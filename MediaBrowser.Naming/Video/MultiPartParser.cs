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

        public MultiPartParserResult Parse(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            path = Path.GetFileName(path);

            return _options.FileStackingExpressions.Select(i => Parse(path, i))
                .FirstOrDefault(i => i.IsMultiPart) ??
                new MultiPartParserResult { Path = path };
        }

        private MultiPartParserResult Parse(string file, string expression)
        {
            var match = GetRegex(expression).Match(file);

            var result = new MultiPartParserResult();

            if (match.Success && match.Groups.Count >= 3)
            {
                var name = match.Groups[1].Value;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    result.IsMultiPart = true;
                    result.Name = name;
                    result.Part = match.Groups[2].Value;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the regex.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Regex.</returns>
        private Regex GetRegex(string expression)
        {
            return new Regex(expression, RegexOptions.IgnoreCase);
        }
    }
}
