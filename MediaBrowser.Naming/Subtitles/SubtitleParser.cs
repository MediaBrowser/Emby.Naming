using MediaBrowser.Naming.Logging;
using System;
using System.IO;
using System.Linq;

namespace MediaBrowser.Naming.Subtitles
{
    public class SubtitleParser
    {
        private ILogger _logger;
        private readonly SubtitleOptions _options;

        public SubtitleParser(SubtitleOptions options)
        {
            _options = options;
        }

        public SubtitleInfo ParseFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            var extension = Path.GetExtension(path);
            if (!_options.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                return null;
            }

            var info = new SubtitleInfo
            {
                Path = path
            };

            return info;
        }
    }
}
