using MediaBrowser.Naming.Audio;
using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.Logging;
using System;
using System.IO;
using System.Linq;

namespace MediaBrowser.Naming.Video
{
    public class ExtraTypeParser
    {
        private readonly ILogger _logger;
        private readonly VideoOptions _videoOptions;
        private readonly AudioOptions _audioOptions;

        public ExtraTypeParser(VideoOptions videoOptions, AudioOptions audioOptions, ILogger logger)
        {
            _videoOptions = videoOptions;
            _audioOptions = audioOptions;
            _logger = logger;
        }

        public string GetExtraType(string path)
        {
            return _videoOptions.ExtraRules
                .Select(i => GetExtraType(path, i))
                .FirstOrDefault(i => !string.IsNullOrWhiteSpace(i));
        }

        private string GetExtraType(string path, ExtraRule rule)
        {
            if (rule.MediaType == MediaType.Audio)
            {
                if (!new AudioFileParser(_audioOptions).IsAudioFile(path))
                {
                    return null;
                }
            }
            else if (rule.MediaType == MediaType.Video)
            {
                if (!new VideoFileParser(_videoOptions, _logger).IsVideoFile(path))
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            var filename = Path.GetFileNameWithoutExtension(path);

            if (rule.RuleType == ExtraRuleType.Filename)
            {
                return string.Equals(filename, rule.Token, StringComparison.OrdinalIgnoreCase) ?
                    rule.ExtraType :
                    null;
            }

            if (rule.RuleType == ExtraRuleType.Suffix)
            {
                return filename.EndsWith(rule.Token, StringComparison.OrdinalIgnoreCase) ?
                    rule.ExtraType :
                    null;
            }

            return null;
        }
    }
}
