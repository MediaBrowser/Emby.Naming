using MediaBrowser.Naming.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public EpisodePathParserResult Parse(string path, bool IsDirectory, bool fillExtendedInfo = true)
        {
            // Added to be able to use regex patterns which require a file extension.
            // There were no failed tests without this block, but to be safe, we can keep it until
            // the regex which require file extensions are modified so that they don't need them.
            if (IsDirectory)
                path += ".mp4";

            var query = from expression in _options.EpisodeExpressions
                        select Parse(path, expression);
            EpisodePathParserResult result = query.FirstOrDefault(r => r.Success);

            if (result != null && fillExtendedInfo)
            {
                FillAdditional(path, result);

                if (!string.IsNullOrWhiteSpace(result.SeriesName))
                {
                    result.SeriesName = result.SeriesName
                        .Trim()
                        .Trim(new[] { '_', '.', '-' })
                        .Trim();
                }
            }

            return result ?? new EpisodePathParserResult();
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
                    result.Success = true;
                }
                else if (expression.IsNamed)
                {
                    int num;
                    if (int.TryParse(match.Groups["seasonnumber"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                    {
                        result.SeasonNumber = num;
                    }

                    if (int.TryParse(match.Groups["epnumber"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                    {
                        result.EpisodeNumber = num;
                    }

                    Group endingNumberGroup = match.Groups["endingepnumber"];
                    if (endingNumberGroup.Success)
                    {
                        // Will only set EndingEpsiodeNumber if the captured number is not followed by additional numbers
                        // or a 'p' or 'i' as what you would get with a pixel resolution specification.
                        // It avoids erroneous parsing of something like "series-s09e14-1080p.mkv" as a multi-episode from E14 to E108
                        int nextIndex = endingNumberGroup.Index + endingNumberGroup.Length;
                        if (nextIndex >= name.Length || "0123456789iIpP".IndexOf(name[nextIndex]) == -1)
                        {
                            if (int.TryParse(endingNumberGroup.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                            {
                                result.EndingEpsiodeNumber = num;
                            }
                        }
                    }

                    result.SeriesName = match.Groups["seriesname"].Value;
                    result.Success = result.EpisodeNumber.HasValue;
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
                        result.EpisodeNumber = num;
                    }

                    result.Success = result.EpisodeNumber.HasValue;
                }

                // Invalidate match when the season is 200 through 1927 or above 2500
                // because it is an error unless the TV show is intentionally using false season numbers.
                // It avoids erroneous parsing of something like "Series Special (1920x1080).mkv" as being season 1920 episode 1080.
                if (result.SeasonNumber >= 200 && result.SeasonNumber < 1928 || result.SeasonNumber > 2500)
                    result.Success = false;

                // Invalidate match when the season is greater than 1 and the episode is greater than 365
                // because it is an error unless the TV show is intentionally using false episode numbers.
                // It avoids erroneous parsing of something like "Series (2001-2002)\Episode 31.mp4" as being season 2001 episode 2002.
                if (result.SeasonNumber > 1 && result.EpisodeNumber > 365)
                    result.Success = false;

                result.IsByDate = expression.IsByDate;
            }

            return result;
        }

        private void FillAdditional(string path, EpisodePathParserResult info)
        {
            var expressions = new List<EpisodeExpression>();

            expressions.InsertRange(0, _multipleEpisodeExpressions.Select(i => new EpisodeExpression
            {
                Expression = i,
                IsNamed = true
            }));

            if (string.IsNullOrWhiteSpace(info.SeriesName))
            {
                expressions.InsertRange(0, _options.EpisodeExpressions.Where(i => i.IsNamed));
            }

            FillAdditional(path, info, expressions);
        }

        private void FillAdditional(string path, EpisodePathParserResult info, IEnumerable<EpisodeExpression> expressions)
        {
            var results = expressions
                .Where(i => i.IsNamed)
                .Select(i => Parse(path, i))
                .Where(i => i.Success);

            foreach (var result in results)
            {
                if (string.IsNullOrWhiteSpace(info.SeriesName))
                {
                    info.SeriesName = result.SeriesName;
                }

                if (!info.EndingEpsiodeNumber.HasValue && info.EpisodeNumber.HasValue)
                {
                    info.EndingEpsiodeNumber = result.EndingEpsiodeNumber;
                }

                if (!string.IsNullOrWhiteSpace(info.SeriesName))
                {
                    if (!info.EpisodeNumber.HasValue || info.EndingEpsiodeNumber.HasValue)
                    {
                        break;
                    }
                }
            }
        }

        private readonly string[] _multipleEpisodeExpressions =
        {
            @".*(\\|\/)[sS]?(?<seasonnumber>\d{1,4})[xX](?<epnumber>\d{1,3})((-| - )\d{1,4}[eExX](?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)[sS]?(?<seasonnumber>\d{1,4})[xX](?<epnumber>\d{1,3})((-| - )\d{1,4}[xX][eE](?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)[sS]?(?<seasonnumber>\d{1,4})[xX](?<epnumber>\d{1,3})((-| - )?[xXeE](?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)[sS]?(?<seasonnumber>\d{1,4})[xX](?<epnumber>\d{1,3})(-[xE]?[eE]?(?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)(?<seriesname>((?![sS]?\d{1,4}[xX]\d{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>\d{1,4})[xX](?<epnumber>\d{1,3}))((-| - )\d{1,4}[xXeE](?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)(?<seriesname>((?![sS]?\d{1,4}[xX]\d{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>\d{1,4})[xX](?<epnumber>\d{1,3}))((-| - )\d{1,4}[xX][eE](?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)(?<seriesname>((?![sS]?\d{1,4}[xX]\d{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>\d{1,4})[xX](?<epnumber>\d{1,3}))((-| - )?[xXeE](?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)(?<seriesname>((?![sS]?\d{1,4}[xX]\d{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>\d{1,4})[xX](?<epnumber>\d{1,3}))(-[xX]?[eE]?(?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)(?<seriesname>[^\\\/]*)[sS](?<seasonnumber>\d{1,4})[xX\.]?[eE](?<epnumber>\d{1,3})((-| - )?[xXeE](?<endingepnumber>\d{1,3}))+[^\\\/]*$",
            @".*(\\|\/)(?<seriesname>[^\\\/]*)[sS](?<seasonnumber>\d{1,4})[xX\.]?[eE](?<epnumber>\d{1,3})(-[xX]?[eE]?(?<endingepnumber>\d{1,3}))+[^\\\/]*$"
        };
    }
}
