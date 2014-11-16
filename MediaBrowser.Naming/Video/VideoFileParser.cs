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
        /// Parses a video file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>VideoFileInfo.</returns>
        public VideoFileInfo ParseFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            var isStub = false;

            var extension = Path.GetExtension(path);
            // Check supported extensions
            if (!_options.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                // It's not supported. Check stub extensions
                if (_options.StubFileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    isStub = true;
                }
                else
                {
                    return null;
                }
            }

            var multiPartResult = GetMultiPartParserResult(path);

            var name = multiPartResult.IsMultiPart ?
                multiPartResult.Name :
                Path.GetFileNameWithoutExtension(path);

            var cleanDateTimeResult = CleanDateTime(name);

            name = cleanDateTimeResult.Name;
            name = CleanString(name).Name;

            var flags = GetFlags(path);

            var info = new VideoFileInfo
            {
                Path = path,
                Flags = flags,
                Container = (extension ?? string.Empty).TrimStart('.'),
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
            return new CleanString().Clean(name, _options.CleanStrings);
        }

        public CleanDateTimeResult CleanDateTime(string name)
        {
            return new CleanDateTime().Clean(name, _options.CleanDateTimes);
        }

        private Format3DResult GetFormat3DInfo(string[] flags)
        {
            return new Format3D(_options, _logger).Parse(flags);
        }

        /// <summary>
        /// Determines whether [is multi part file] [the specified path].
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.ArgumentNullException">path</exception>
        public MultiPartParserResult GetMultiPartParserResult(string path)
        {
            return new MultiPartParser(_options, _logger).Parse(path);
        }
    }
}
