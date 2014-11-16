using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Video
{
    /// <summary>
    /// http://kodi.wiki/view/Advancedsettings.xml#video
    /// </summary>
    public class CleanDateTime
    {
        public CleanDateTimeResult Clean(string name, IEnumerable<string> expressions)
        {
            return expressions.Select(i => Clean(name, i))
                .FirstOrDefault(i => i.HasChanged);
        }

        public CleanDateTimeResult Clean(string name, string expression)
        {
            var regex = GetRegex(expression);

            var result = new CleanDateTimeResult();

            var match = regex.Match(name);

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

        private Regex GetRegex(string expression)
        {
            return new Regex(expression, RegexOptions.IgnoreCase);
        }
    }
}
