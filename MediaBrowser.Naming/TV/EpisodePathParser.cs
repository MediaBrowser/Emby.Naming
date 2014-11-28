using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.TV
{
    public class EpisodePathParser
    {
        private readonly NamingOptions _options;
        private readonly IRegexProvider _iRegexProvider;

        public EpisodePathParser(NamingOptions options, IRegexProvider iRegexProvider)
        {
            _options = options;
            _iRegexProvider = iRegexProvider;
        }

        public EpisodePathParserResult Parse(string path, FileInfoType type)
        {
            var name = Path.GetFileName(path);

            var result = _options.EpisodeExpressions.Select(i => Parse(name, i))
                .FirstOrDefault(i => i.Success);

            if (result != null)
            {
                return result;
            }
            
            return new EpisodePathParserResult
            {

            };
        }

        private EpisodePathParserResult Parse(string name, EpisodeExpression expression)
        {
            var result = new EpisodePathParserResult();

            var match = _iRegexProvider.GetRegex(expression.Expression, RegexOptions.IgnoreCase).Match(name);

            // (Full)(Season)(Episode)(Extension)
            if (match.Success && match.Groups.Count >= 3)
            {
                if (expression.IsByDate)
                {
                    DateTime date;
                    if (expression.DateTimeFormats.Length > 0)
                    {
                        if (DateTime.TryParseExact(match.Groups[0].Value, 
                            expression.DateTimeFormats, 
                            CultureInfo.InvariantCulture, 
                            DateTimeStyles.None, 
                            out date))
                        {
                            result.Year = date.Year;
                            result.Month = date.Month;
                            result.Day = date.Day;
                        }
                    }
                    else
                    {
                        if (DateTime.TryParse(match.Groups[0].Value, out date))
                        {
                            result.Year = date.Year;
                            result.Month = date.Month;
                            result.Day = date.Day;
                        }
                    }
                }
                else
                {
                    int num;
                    if (int.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                    {
                        result.SeasonNumber = num;
                    }

                    if (int.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                    {
                        result.EpsiodeNumber = num;
                    }
                }

                result.IsByDate = expression.IsByDate;
                result.Success = true;
                return result;
            }

            return result;
        }
    }
}
