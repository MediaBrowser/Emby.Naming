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
        private readonly IRegexProvider _iRegexProvider;

        public VideoListResolver(NamingOptions options, ILogger logger)
            : this(options, logger, new RegexProvider())
        {
        }

        public VideoListResolver(NamingOptions options, ILogger logger, IRegexProvider iRegexProvider)
        {
            _options = options;
            _logger = logger;
            _iRegexProvider = iRegexProvider;
        }

        public IEnumerable<VideoInfo> Resolve(List<PortableFileInfo> files)
        {
            var videoResolver = new VideoResolver(_options, _logger, _iRegexProvider);

            var stackResult = new StackResolver(_options, _logger, _iRegexProvider)
                .Resolve(files);

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

                var extras = remainingFiles
                    .Where(i => !string.IsNullOrWhiteSpace(i.ExtraType))
                    .Where(i => i.FileNameWithoutExtension.StartsWith(stack.Name, StringComparison.OrdinalIgnoreCase))
                    .ToList();

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

                var extras = remainingFiles
                    .Where(i => !string.IsNullOrWhiteSpace(i.ExtraType))
                    .Where(i => i.FileNameWithoutExtension.StartsWith(media.FileNameWithoutExtension, StringComparison.OrdinalIgnoreCase))
                    .ToList();

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
    }
}
