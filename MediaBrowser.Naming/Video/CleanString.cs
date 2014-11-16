using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Video
{
    /// <summary>
    /// http://kodi.wiki/view/Advancedsettings.xml#video
    /// </summary>
    public class CleanString
    {
        public CleanStringResult Clean(string name, IEnumerable<string> expressions)
        {
            var hasChanged = false;

            foreach (var exp in expressions)
            {
                var result = Clean(name, exp);

                name = result.Name;
                hasChanged = hasChanged || result.HasChanged;
            }

            return new CleanStringResult
            {
                Name = name,
                HasChanged = hasChanged
            };
        }

        public CleanStringResult Clean(string name, string expression)
        {
            var regex = GetRegex(expression);

            var result = new CleanStringResult();

            var match = regex.Match(name);

            if (match.Success)
            {
                result.HasChanged = true;
                name = name.Substring(0, match.Index);
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
