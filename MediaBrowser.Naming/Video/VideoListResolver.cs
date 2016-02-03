using MediaBrowser.Naming.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interfaces.IO;
using Patterns.Logging;

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

        public IEnumerable<VideoInfo> Resolve(List<FileMetadata> files, bool supportMultiVersion = true)
        {
            var videoResolver = new VideoResolver(_options, _logger, _regexProvider);

            var videoInfos = files
                .Select(i => videoResolver.Resolve(i.Id, i.IsFolder))
                .Where(i => i != null)
                .ToList();

            // Filter out all extras, otherwise they could cause stacks to not be resolved
            // See the unit test TestStackedWithTrailer
            var nonExtras = videoInfos
                .Where(i => string.IsNullOrWhiteSpace(i.ExtraType))
                .Select(i => new FileMetadata
                {
                    Id = i.Path,
                    IsFolder = i.IsFolder
                });

            var stackResult = new StackResolver(_options, _logger, _regexProvider)
                .Resolve(nonExtras);

            var remainingFiles = videoInfos
                .Where(i => !stackResult.Stacks.Any(s => s.ContainsFile(i.Path, i.IsFolder)))
                .ToList();

            var list = new List<VideoInfo>();

            foreach (var stack in stackResult.Stacks)
            {
                var info = new VideoInfo
                {
                    Files = stack.Files.Select(i => videoResolver.Resolve(i, stack.IsFolderStack)).ToList(),
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

            if (supportMultiVersion)
            {
                list = GetVideosGroupedByVersion(list)
                    .ToList();
            }

            // If there's only one resolved video, use the folder name as well to find extras
            if (list.Count == 1)
            {
                var info = list[0];
                var videoPath = list[0].Files[0].Path;
                var parentPath = Path.GetDirectoryName(videoPath);

                if (!string.IsNullOrWhiteSpace(parentPath))
                {
                    var folderName = Path.GetFileName(Path.GetDirectoryName(videoPath));
                    if (!string.IsNullOrWhiteSpace(folderName))
                    {
                        var extras = GetExtras(remainingFiles, new List<string> { folderName });

                        remainingFiles = remainingFiles
                            .Except(extras)
                            .ToList();

                        info.Extras.AddRange(extras);
                    }
                }

                // Add the extras that are just based on file name as well
                var extrasByFileName = remainingFiles
                    .Where(i => i.ExtraRule != null && i.ExtraRule.RuleType == ExtraRuleType.Filename)
                    .ToList();

                remainingFiles = remainingFiles
                    .Except(extrasByFileName)
                    .ToList();

                info.Extras.AddRange(extrasByFileName);
            }

            // If there's only one video, accept all trailers
            // Be lenient because people use all kinds of mish mash conventions with trailers
            if (list.Count == 1)
            {
                var trailers = remainingFiles
                    .Where(i => string.Equals(i.ExtraType, "trailer", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                list[0].Extras.AddRange(trailers);

                remainingFiles = remainingFiles
                    .Except(trailers)
                    .ToList();
            }

            // Whatever files are left, just add them
            list.AddRange(remainingFiles.Select(i => new VideoInfo
            {
                Files = new List<VideoFileInfo> { i },
                Name = i.Name,
                Year = i.Year
            }));

            var orderedList = list.OrderBy(i => i.Name);

            return orderedList;
        }

        private IEnumerable<VideoInfo> GetVideosGroupedByVersion(List<VideoInfo> videos)
        {
            if (videos.Count == 0)
            {
                return videos;
            }

            var list = new List<VideoInfo>();

            var filenamePrefix = Path.GetFileName(Path.GetDirectoryName(videos[0].Files[0].Path));

            if (!string.IsNullOrWhiteSpace(filenamePrefix) && filenamePrefix.Length > 1)
            {
                if (videos.All(i => i.Files.Count == 1 && (Path.GetFileNameWithoutExtension(i.Files[0].Path).StartsWith(filenamePrefix, StringComparison.OrdinalIgnoreCase))))
                {
                    // Enforce the multi-version limit
                    if (videos.Count <= 8)
                    {
                        var ordered = videos.OrderBy(i => i.Name).ToList();

                        list.Add(ordered[0]);

                        list[0].AlternateVersions = ordered.Skip(1).Select(i => i.Files[0]).ToList();
                        list[0].Name = filenamePrefix;
                        list[0].Extras.AddRange(ordered.Skip(1).SelectMany(i => i.Extras));

                        return list;
                    }
                }
            }

            return videos;
            //foreach (var video in videos.OrderBy(i => i.Name))
            //{
            //    var match = list
            //        .FirstOrDefault(i => string.Equals(i.Name, video.Name, StringComparison.OrdinalIgnoreCase));

            //    if (match != null && video.Files.Count == 1 && match.Files.Count == 1)
            //    {
            //        match.AlternateVersions.Add(video.Files[0]);
            //        match.Extras.AddRange(video.Extras);
            //    }
            //    else
            //    {
            //        list.Add(video);
            //    }
            //}

            //return list;
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
