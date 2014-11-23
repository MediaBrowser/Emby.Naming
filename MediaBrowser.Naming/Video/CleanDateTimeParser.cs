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
        private readonly VideoOptions _videoOptions;

        public CleanDateTimeParser(VideoOptions videoOptions)
        {
            _videoOptions = videoOptions;
        }

        public CleanDateTimeResult Clean(string name)
        {
            var originalName = name;

            // Dummy up a file extension because the expressions will fail without one
            if (string.IsNullOrWhiteSpace(Path.GetExtension(name)))
            {
                name += ".mkv";
            }

            var result = _videoOptions.CleanDateTimes.Select(i => Clean(name, i))
                .FirstOrDefault(i => i.HasChanged) ??
                new CleanDateTimeResult { Name = originalName };

            if (result.HasChanged)
            {
                return result;
            }

            // Make a second pass, running clean string first
            var cleanStringResult = new CleanStringParser().Clean(name, _videoOptions.CleanStrings);

            if (!cleanStringResult.HasChanged)
            {
                return result;
            }

            return _videoOptions.CleanDateTimes.Select(i => Clean(cleanStringResult.Name, i))
                .FirstOrDefault(i => i.HasChanged) ??
                result;
        }

        public CleanDateTimeResult Clean(string name, string expression)
        {
            var result = new CleanDateTimeResult();

            var match = Regex.Match(name, expression, RegexOptions.IgnoreCase);

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
