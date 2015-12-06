using MediaBrowser.Naming.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Video
{
    /// <summary>
    /// http://kodi.wiki/view/Advancedsettings.xml#video
    /// </summary>
    public class CleanStringParser
    {
        private readonly IRegexProvider _iRegexProvider;

        public CleanStringParser(IRegexProvider iRegexProvider)
        {
            _iRegexProvider = iRegexProvider;
        }

        public CleanStringResult Clean(string name, IEnumerable<string> expressions)
        {
            var hasChanged = false;

            foreach (var exp in expressions)
            {
                var result = Clean(name, exp);

                if (!string.IsNullOrWhiteSpace(result.Name))
                {
                    name = result.Name;
                    hasChanged = hasChanged || result.HasChanged;
                }
            }

            return new CleanStringResult
            {
                Name = name,
                HasChanged = hasChanged
            };
        }

        private CleanStringResult Clean(string name, string expression)
        {
            var result = new CleanStringResult();

            var match = _iRegexProvider.GetRegex(expression, RegexOptions.IgnoreCase).Match(name);

            if (match.Success)
            {
                result.HasChanged = true;
                name = name.Substring(0, match.Index);
            }

            result.Name = name;
            return result;
        }
    }
}
