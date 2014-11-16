using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Video
{
    public class MultiPartParser
    {
        private readonly VideoOptions _options;
        private readonly ILogger _logger;

        public MultiPartParser(VideoOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        public MultiPartResult Parse(string path, FileInfoType type)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            path = Path.GetFileName(path);

            if (type == FileInfoType.Directory)
            {
                // Dummy this up since the stacking expressions currently expect an extension
                path += ".mkv";
            }

            return _options.FileStackingExpressions.Select(i => Parse(path, i))
                .FirstOrDefault(i => i.IsMultiPart) ??
                new MultiPartResult();
        }

        private MultiPartResult Parse(string file, string expression)
        {
            var match = Regex.Match(file, expression, RegexOptions.IgnoreCase);

            var result = new MultiPartResult();

            if (match.Success && match.Groups.Count >= 3)
            {
                var name = match.Groups[1].Value;
                var part = match.Groups[2].Value;

                // See if the matched part represents the whole filename
                if (string.IsNullOrWhiteSpace(name))
                {
                    if (string.Equals(part, Path.GetFileNameWithoutExtension(file), StringComparison.OrdinalIgnoreCase))
                    {
                        name = part;
                    }
                }

                if (!string.IsNullOrWhiteSpace(name))
                {
                    result.IsMultiPart = true;
                    result.Name = name;
                    result.Part = part;
                }
            }

            return result;
        }
    }
}
