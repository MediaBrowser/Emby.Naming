using MediaBrowser.Naming.Audio;
using MediaBrowser.Naming.Common;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MediaBrowser.Model.Logging;

namespace MediaBrowser.Naming.Video
{
    public class ExtraResolver
    {
        private readonly ILogger _logger;
        private readonly NamingOptions _options;
        private readonly IRegexProvider _regexProvider;

        public ExtraResolver(NamingOptions options, ILogger logger, IRegexProvider regexProvider)
        {
            _options = options;
            _logger = logger;
            _regexProvider = regexProvider;
        }

        public ExtraResult GetExtraInfo(string path)
        {
            return _options.VideoExtraRules
                .Select(i => GetExtraInfo(path, i))
                .FirstOrDefault(i => !string.IsNullOrWhiteSpace(i.ExtraType)) ?? new ExtraResult();
        }

        private ExtraResult GetExtraInfo(string path, ExtraRule rule)
        {
            var result = new ExtraResult();

            if (rule.MediaType == MediaType.Audio)
            {
                if (!new AudioFileParser(_options).IsAudioFile(path))
                {
                    return result;
                }
            }
            else if (rule.MediaType == MediaType.Video)
            {
                if (!new VideoResolver(_options, _logger).IsVideoFile(path))
                {
                    return result;
                }
            }
            else
            {
                return result;
            }

            if (rule.RuleType == ExtraRuleType.Filename)
            {
                var filename = Path.GetFileNameWithoutExtension(path);

                if (string.Equals(filename, rule.Token, StringComparison.OrdinalIgnoreCase))
                {
                    result.ExtraType = rule.ExtraType;
                    result.Rule = rule;
                }
            }

            else if (rule.RuleType == ExtraRuleType.Suffix)
            {
                var filename = Path.GetFileNameWithoutExtension(path);

                if (filename.IndexOf(rule.Token, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    result.ExtraType = rule.ExtraType;
                    result.Rule = rule;
                }
            }

            else if (rule.RuleType == ExtraRuleType.Regex)
            {
                var filename = Path.GetFileName(path);

                var regex = _regexProvider.GetRegex(rule.Token, RegexOptions.IgnoreCase);

                if (regex.IsMatch(filename))
                {
                    result.ExtraType = rule.ExtraType;
                    result.Rule = rule;
                }
            }

            return result;
        }
    }
}
