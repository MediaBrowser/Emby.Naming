using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
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

        public EpisodePathParserResult Parse(string path, FileInfoType type, bool enableOptimisticExpressions, bool fillExtendedInfo = true)
        {
            var name = path;

            var result = _options.EpisodeExpressions
                .Where(i => enableOptimisticExpressions || !i.IsOptimistic)
                .Select(i => Parse(name, i))
                .FirstOrDefault(i => i.Success);

            if (result != null && fillExtendedInfo)
            {
                FillAdditional(path, result);
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

                    var endingNumberGroup = match.Groups["endingepnumber"];
                    if (endingNumberGroup != null)
                    {
                        if (int.TryParse(endingNumberGroup.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                        {
                            result.EndingEpsiodeNumber = num;
                        }
                    }

                    var seriesGroup = match.Groups["seriesname"];
                    result.SeriesName = seriesGroup == null ? null : seriesGroup.Value;
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
