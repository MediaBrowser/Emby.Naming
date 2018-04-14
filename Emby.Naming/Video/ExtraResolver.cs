﻿using Emby.Naming.Audio;
using Emby.Naming.Common;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Emby.Naming.Video
{
    public class ExtraResolver
    {
        private readonly NamingOptions _options;

        public ExtraResolver(NamingOptions options)
        {
            _options = options;
        }

        public ExtraResult GetExtraInfo(string path)
        {
            return _options.VideoExtraRules
                .Select(i => GetExtraInfo(path, i))
                .FirstOrDefault(i => !string.IsNullOrEmpty(i.ExtraType)) ?? new ExtraResult();
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
                if (!new VideoResolver(_options).IsVideoFile(path))
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

                var regex = new Regex(rule.Token, RegexOptions.IgnoreCase);

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
