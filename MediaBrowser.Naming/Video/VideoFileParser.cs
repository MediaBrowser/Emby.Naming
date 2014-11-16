using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using System;
using System.IO;
using System.Linq;

namespace MediaBrowser.Naming.Video
{
    public class VideoFileParser
    {
        private readonly ILogger _logger;
        private readonly VideoOptions _options;

        public VideoFileParser(VideoOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// Parses the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>VideoFileInfo.</returns>
        public VideoFileInfo ParseDirectory(string path)
        {
            return ParsePath(path, FileInfoType.Directory);
        }
        
        /// <summary>
        /// Parses a video file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>VideoFileInfo.</returns>
        public VideoFileInfo ParseFile(string path)
        {
            return ParsePath(path, FileInfoType.File);
        }

        /// <summary>
        /// Parses the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="type">The type.</param>
        /// <returns>VideoFileInfo.</returns>
        /// <exception cref="System.ArgumentNullException">path</exception>
        public VideoFileInfo ParsePath(string path, FileInfoType type)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            var isStub = false;
            string container = null;

            if (type == FileInfoType.File)
            {
                var extension = Path.GetExtension(path) ?? string.Empty;
                // Check supported extensions
                if (!_options.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    var stubResult = new StubParser(_options, _logger).ParseFile(path);

                    isStub = stubResult.IsStub;

                    // It's not supported. Check stub extensions
                    if (!isStub)
                    {
                        return null;
                    }
                }

                container = extension.TrimStart('.');
            }

            var multiPartResult = GetMultiPartParserResult(path, type);

            var name = multiPartResult.IsMultiPart ?
                multiPartResult.Name :
                (type == FileInfoType.File ?
                Path.GetFileNameWithoutExtension(path) :
                Path.GetFileName(path));

            var cleanDateTimeResult = CleanDateTime(name);

            name = cleanDateTimeResult.Name;
            name = CleanString(name).Name;

            var flags = GetFlags(path);

            var info = new VideoFileInfo
            {
                Path = path,
                Flags = flags,
                Container = container,
                IsStub = isStub,
                Name = name,
                Year = cleanDateTimeResult.Year
            };

            var format3DResult = GetFormat3DInfo(flags);

            info.Is3D = format3DResult.Is3D;
            info.Format3D = format3DResult.Format3D;

            info.IsMultiPart = multiPartResult.IsMultiPart;
            info.Part = multiPartResult.Part;

            return info;
        }

        public bool IsVideoFile(string path)
        {
            var extension = Path.GetExtension(path) ?? string.Empty;
            return _options.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }

        public string[] GetFlags(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            // Note: the tags need be be surrounded be either a space ( ), hyphen -, dot . or underscore _.

            var file = Path.GetFileName(path);

            return file.Split(_options.FlagDelimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        public CleanStringResult CleanString(string name)
        {
            return new CleanStringParser().Clean(name, _options.CleanStrings);
        }

        public CleanDateTimeResult CleanDateTime(string name)
        {
            return new CleanDateTimeParser().Clean(name, _options.CleanDateTimes);
        }

        private Format3DResult GetFormat3DInfo(string[] flags)
        {
            return new Format3DParser(_options, _logger).Parse(flags);
        }

        private MultiPartResult GetMultiPartParserResult(string path, FileInfoType type)
        {
            return new MultiPartParser(_options, _logger).Parse(path, type);
        }
    }
}
