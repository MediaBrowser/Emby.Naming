using MediaBrowser.Naming.Common;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MediaBrowser.Naming.TV
{
    public class SeasonPathParser
    {
        private readonly NamingOptions _options;
        private readonly IRegexProvider _iRegexProvider;

        public SeasonPathParser(NamingOptions options, IRegexProvider iRegexProvider)
        {
            _options = options;
            _iRegexProvider = iRegexProvider;
        }

        public SeasonPathParserResult Parse(string path, bool supportSpecialAliases, bool supportNumericSeasonFolders)
        {
            var result = new SeasonPathParserResult();

            result.SeasonNumber = GetSeasonNumberFromPath(path, supportSpecialAliases, supportNumericSeasonFolders);
            result.Success = result.SeasonNumber.HasValue;

            return result;
        }

        /// <summary>
        /// A season folder must contain one of these somewhere in the name
        /// </summary>
        private static readonly string[] SeasonFolderNames =
        {
            "season",
            "sæson",
            "temporada",
            "saison",
            "staffel",
            "series",
            "сезон",
            "stagione"
        };

        /// <summary>
        /// Gets the season number from path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="supportSpecialAliases">if set to <c>true</c> [support special aliases].</param>
        /// <param name="supportNumericSeasonFolders">if set to <c>true</c> [support numeric season folders].</param>
        /// <returns>System.Nullable{System.Int32}.</returns>
        private int? GetSeasonNumberFromPath(string path, bool supportSpecialAliases, bool supportNumericSeasonFolders)
        {
            var filename = Path.GetFileName(path);

            if (supportSpecialAliases)
            {
                if (string.Equals(filename, "specials", StringComparison.OrdinalIgnoreCase))
                {
                    return 0;
                }
                if (string.Equals(filename, "extras", StringComparison.OrdinalIgnoreCase))
                {
                    return 0;
                }
            }

            if (supportNumericSeasonFolders)
            {
                int val;
                if (int.TryParse(filename, NumberStyles.Integer, CultureInfo.InvariantCulture, out val))
                {
                    return val;
                }
            }

            if (filename.StartsWith("s", StringComparison.OrdinalIgnoreCase))
            {
                var testFilename = filename.Substring(1);

                int val;
                if (int.TryParse(testFilename, NumberStyles.Integer, CultureInfo.InvariantCulture, out val))
                {
                    return val;
                }
            }

            // Look for one of the season folder names
            foreach (var name in SeasonFolderNames)
            {
                var index = filename.IndexOf(name, StringComparison.OrdinalIgnoreCase);

                if (index != -1)
                {
                    return GetSeasonNumberFromPathSubstring(filename.Substring(index + name.Length));
                }
            }

            var parts = filename.Split(new[] { '.', '_', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);

            return parts.Select(GetSeasonNumberFromPart).FirstOrDefault(i => i.HasValue);
        }

        private int? GetSeasonNumberFromPart(string part)
        {
            if (part.Length < 2 || !part.StartsWith("s", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            part = part.Substring(1);

            int value;
            if (int.TryParse(part, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Extracts the season number from the second half of the Season folder name (everything after "Season", or "Staffel")
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.Nullable{System.Int32}.</returns>
        private int? GetSeasonNumberFromPathSubstring(string path)
        {
            var numericStart = -1;
            var length = 0;

            // Find out where the numbers start, and then keep going until they end
            for (var i = 0; i < path.Length; i++)
            {
                if (char.IsNumber(path, i))
                {
                    if (numericStart == -1)
                    {
                        numericStart = i;
                    }
                    length++;
                }
                else if (numericStart != -1)
                {
                    break;
                }
            }

            if (numericStart == -1)
            {
                return null;
            }

            return int.Parse(path.Substring(numericStart, length), CultureInfo.InvariantCulture);
        }
    }
}
