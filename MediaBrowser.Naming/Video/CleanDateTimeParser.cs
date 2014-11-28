using MediaBrowser.Naming.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Video
{
    /// <summary>
    /// http://kodi.wiki/view/Advancedsettings.xml#video
    /// </summary>
    public class CleanDateTimeParser
    {
        private readonly NamingOptions _options;
        private readonly IRegexProvider _iRegexProvider;

        public CleanDateTimeParser(NamingOptions options, IRegexProvider iRegexProvider)
        {
            _options = options;
            _iRegexProvider = iRegexProvider;
        }

        public CleanDateTimeResult Clean(string name)
        {
            var originalName = name;

            // Dummy up a file extension because the expressions will fail without one
            if (string.IsNullOrWhiteSpace(Path.GetExtension(name)))
            {
                name += ".mkv";
            }

            var result = _options.CleanDateTimes.Select(i => Clean(name, i))
                .FirstOrDefault(i => i.HasChanged) ??
                new CleanDateTimeResult { Name = originalName };

            if (result.HasChanged)
            {
                return result;
            }

            // Make a second pass, running clean string first
            var cleanStringResult = new CleanStringParser(_iRegexProvider).Clean(name, _options.CleanStrings);

            if (!cleanStringResult.HasChanged)
            {
                return result;
            }

            return _options.CleanDateTimes.Select(i => Clean(cleanStringResult.Name, i))
                .FirstOrDefault(i => i.HasChanged) ??
                result;
        }

        public CleanDateTimeResult Clean(string name, string expression)
        {
            var result = new CleanDateTimeResult();

            var match = _iRegexProvider.GetRegex(expression, RegexOptions.IgnoreCase).Match(name);

            if (match.Success && match.Groups.Count == 4)
            {
                int year;
                if (match.Groups[1].Success && match.Groups[2].Success && int.TryParse(match.Groups[2].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out year))
                {
                    name = match.Groups[1].Value;
                    result.Year = year;
                    result.HasChanged = true;
                }
            }

            result.Name = name;
            return result;
        }
    }
}
