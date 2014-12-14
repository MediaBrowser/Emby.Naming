using System.IO;
using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaBrowser.Naming.Video
{
    public class VideoListResolver
    {
        private readonly ILogger _logger;
        private readonly NamingOptions _options;
        private readonly IRegexProvider _regexProvider;

        public VideoListResolver(NamingOptions options, ILogger logger)
            : this(options, logger, new RegexProvider())
        {
        }

        public VideoListResolver(NamingOptions options, ILogger logger, IRegexProvider regexProvider)
        {
            _options = options;
            _logger = logger;
            _regexProvider = regexProvider;
        }

        public IEnumerable<VideoInfo> Resolve(List<PortableFileInfo> files)
        {
            var videoResolver = new VideoResolver(_options, _logger, _regexProvider);
            var extraResolver = new ExtraResolver(_options, _logger, _regexProvider);

            // Filter out all extras, otherwise they could cause stacks to not be resolved
            // See the unit test TestStackedWithTrailer
            var nonExtras = files
                .Where(i => extraResolver.GetExtraInfo(i.FullName).ExtraType == null)
                .ToList();

            var stackResult = new StackResolver(_options, _logger, _regexProvider)
                .Resolve(nonExtras);

            var remainingFiles = files
                .Where(i => !stackResult.Stacks.Any(s => s.ContainsFile(i.FullName, i.Type)))
                .Select(i => videoResolver.Resolve(i.FullName, i.Type))
                .Where(i => i != null)
                .ToList();

            var list = new List<VideoInfo>();

            foreach (var stack in stackResult.Stacks)
            {
                var info = new VideoInfo
                {
                    Files = stack.Files.Select(i => videoResolver.Resolve(i, stack.Type)).ToList(),
                    Name = stack.Name
                };

                info.Year = info.Files.First().Year;

                var extraBaseNames = new List<string> 
                {
                    stack.Name, 
                    Path.GetFileNameWithoutExtension(stack.Files[0])
                };

                var extras = GetExtras(remainingFiles, extraBaseNames);

                if (extras.Count > 0)
                {
                    remainingFiles = remainingFiles
                        .Except(extras)
                        .ToList();

                    info.Extras = extras;
                }

                list.Add(info);
            }

            var standaloneMedia = remainingFiles
                .Where(i => string.IsNullOrWhiteSpace(i.ExtraType))
                .ToList();

            foreach (var media in standaloneMedia)
            {
                var info = new VideoInfo
                {
                    Files = new List<VideoFileInfo> { media },
                    Name = media.Name
                };

                info.Year = info.Files.First().Year;

                var extras = GetExtras(remainingFiles, new List<string> { media.FileNameWithoutExtension, media.Name });

                remainingFiles = remainingFiles
                    .Except(extras.Concat(new[] { media }))
                    .ToList();

                info.Extras = extras;

                list.Add(info);
            }

            // Whatever files are left, just add them

            list.AddRange(remainingFiles.Select(i => new VideoInfo
            {
                Files = new List<VideoFileInfo> { i },
                Name = i.Name,
                Year = i.Year
            }));

            return list.OrderBy(i => i.Name);
        }

        private List<VideoFileInfo> GetExtras(IEnumerable<VideoFileInfo> remainingFiles, List<string> baseNames)
        {
            foreach (var name in baseNames.ToList())
            {
                var trimmedName = name.TrimEnd().TrimEnd(_options.VideoFlagDelimiters).TrimEnd();
                baseNames.Add(trimmedName);
            }

            return remainingFiles
                .Where(i => !string.IsNullOrWhiteSpace(i.ExtraType))
                .Where(i => baseNames.Any(b => i.FileNameWithoutExtension.StartsWith(b, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }
}
