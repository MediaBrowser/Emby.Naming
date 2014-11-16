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

        public SubtitleParser(SubtitleOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
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

            var flags = GetFlags(path);

            var info = new SubtitleInfo
            {
                Path = path,
                IsDefault = _options.DefaultFlags.Any(i => flags.Contains(i, StringComparer.OrdinalIgnoreCase)),
                IsForced = _options.ForcedFlags.Any(i => flags.Contains(i, StringComparer.OrdinalIgnoreCase))
            };

            var parts = flags.Where(i => !_options.DefaultFlags.Contains(i, StringComparer.OrdinalIgnoreCase) && !_options.ForcedFlags.Contains(i, StringComparer.OrdinalIgnoreCase))
                .ToList();

            // Should have a name, language and file extension
            if (parts.Count >= 3)
            {
                info.Language = parts[parts.Count - 2];
            }

            return info;
        }

        private string[] GetFlags(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            // Note: the tags need be be surrounded be either a space ( ), hyphen -, dot . or underscore _.

            var file = Path.GetFileName(path);

            return file.Split(_options.FlagDelimiters, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
