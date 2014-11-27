using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.TV
{
    public class EpisodePathParser
    {
        private readonly NamingOptions _options;

        public EpisodePathParser(NamingOptions options)
        {
            _options = options;
        }

        public EpisodePathParserResult Parse(string path, FileInfoType type)
        {
            var name = Path.GetFileName(path);

            var result = _options.EpisodeExpressions.Select(i => Parse(name, i))
                .FirstOrDefault();

            if (result != null)
            {
                return result;
            }
            
            return new EpisodePathParserResult
            {

            };
        }

        private EpisodePathParserResult Parse(string name, string expression)
        {
            var result = new EpisodePathParserResult();

            var match = Regex.Match(name, expression, RegexOptions.IgnoreCase);

            // (Season)(Episode)(Extension)
            if (match.Success && match.Groups.Count >= 3)
            {
                int num;
                if (int.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                {
                    result.SeasonNumber = num;
                    result.Success = true;
                }

                if (int.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                {
                    result.EpsiodeNumber = num;
                    result.Success = true;
                }

                if (result.Success)
                {
                    return result;
                }
            }

            return result;
        }
    }
}
