using MediaBrowser.Naming.Audio;
using MediaBrowser.Naming.Extensions;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Video
{
    public class MultiPartParser
    {
        private readonly VideoOptions _options;
        private readonly ILogger _logger;
        private readonly AudioOptions _audioOptions;

        public MultiPartParser(VideoOptions options, AudioOptions audioOptions, ILogger logger)
        {
            _options = options;
            _audioOptions = audioOptions;
            _logger = logger;
        }

        public MultiPartResult Parse(string path, FileInfoType type)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            var format3DResult = new Format3DParser(_options, _logger).Parse(path);
            var stubResult = new StubResolver(_options, _logger).ResolveFile(path);
            var extraResult = new ExtraTypeParser(_options, _audioOptions, _logger).GetExtraInfo(path);

            return Parse(path, stubResult, extraResult, format3DResult, type);
        }

        public MultiPartResult Parse(string path, StubResult stubResult, ExtraResult extraResult, Format3DResult format3DResult, FileInfoType type)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            path = Path.GetFileName(path);
            string originalName;

            if (type == FileInfoType.Directory)
            {
                originalName = path;

                // Dummy this up since the stacking expressions currently expect an extension
                path += ".mkv";
            }
            else
            {
                originalName = Path.GetFileNameWithoutExtension(path);
            }

            // The presence of 3d tokens, .e.g. movie.3d, can create a false positive (part 3)
            path = StripTokens(path, format3DResult.Tokens);

            // The presence of stub tokens, .e.g. movie.bluray, can create a false positive (part b)
            path = StripTokens(path, stubResult.Tokens);

            // The presence of extra tokens, .e.g. movie-trailer, can create a false positive (part t)
            foreach (var token in extraResult.Tokens)
            {
                var newPath = path.Replace(token, string.Empty, StringComparison.OrdinalIgnoreCase);

                if (!string.Equals(path, newPath, StringComparison.OrdinalIgnoreCase))
                {
                    path = newPath;
                    break;
                }
            }
            var list = new List<string>();
            list.Add(_options.FileStackingExpressions.First());
            list.Add(_options.FileStackingExpressions.Last());
            foreach (var exp in _options.FileStackingExpressions)
            {
                var result = Parse(path, exp);

                if (result.IsMultiPart)
                {
                    return result;
                }
            }
            
            return new MultiPartResult { Name = originalName };
        }

        private string StripTokens(string path, IEnumerable<string> tokens)
        {
            foreach (var token in tokens)
            {
                foreach (var character in _options.FlagDelimiters)
                {
                    var newPath = path.Replace(character + token, string.Empty, StringComparison.OrdinalIgnoreCase);

                    if (!string.Equals(path, newPath, StringComparison.OrdinalIgnoreCase))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            return path;
        }

        private MultiPartResult Parse(string file, string expression)
        {
            var match = Regex.Match(file, expression, RegexOptions.IgnoreCase);

            var result = new MultiPartResult();

            // (Title)(Volume)(Ignore)(Extension)
            if (match.Success && match.Groups.Count >= 5)
            {
                var name = match.Groups[1].Value;
                var part = match.Groups[2].Value;

                // See if the matched part represents the whole filename
                if (string.IsNullOrWhiteSpace(name))
                {
                    if (string.Equals(part, Path.GetFileNameWithoutExtension(file), StringComparison.OrdinalIgnoreCase))
                    {
                        //name = part;
                    }
                }

                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(part))
                {
                    result.IsMultiPart = true;
                    result.Name = name;
                    result.Part = part;
                }
            }

            return result;
        }
    }
}
