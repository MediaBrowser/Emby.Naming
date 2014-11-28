using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Common
{
    public class RegexProvider : IRegexProvider
    {
        private static readonly Dictionary<string, Regex> RegexCache =
            new Dictionary<string, Regex>(StringComparer.OrdinalIgnoreCase);
        
        public Regex GetRegex(string expression, RegexOptions options)
        {
            lock (RegexCache)
            {
                Regex regex;
                if (RegexCache.TryGetValue(expression, out regex))
                {
                    return regex;
                }

                regex = new Regex(expression, options);
                RegexCache[expression] = regex;
                return regex;
            }
        }

    }
}
