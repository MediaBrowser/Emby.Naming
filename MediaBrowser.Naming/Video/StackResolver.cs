using System.IO;
using MediaBrowser.Naming.Audio;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Video
{
    public class StackResolver
    {
        private readonly VideoOptions _options;
        private readonly AudioOptions _audioOptions;
        private readonly ILogger _logger;

        public StackResolver(VideoOptions options, AudioOptions audioOptions, ILogger logger)
        {
            _options = options;
            _audioOptions = audioOptions;
            _logger = logger;
        }

        public StackResult ResolveDirectories(IEnumerable<string> files)
        {
            return Resolve(files.Select(i => new PortableFileInfo
            {
                FullName = i,
                Type = FileInfoType.Directory
            }));
        }

        public StackResult ResolveFiles(IEnumerable<string> files)
        {
            return Resolve(files.Select(i => new PortableFileInfo
            {
                FullName = i,
                Type = FileInfoType.File
            }));
        }

        public StackResult Resolve(IEnumerable<PortableFileInfo> files)
        {
            var result = new StackResult();

            var resolver = new VideoResolver(_options, _audioOptions, _logger);

            var list = files
                .Where(i => i.Type == FileInfoType.Directory || (resolver.IsVideoFile(i.FullName) || resolver.IsStubFile(i.FullName)))
                .OrderBy(i => i.FullName)
                .ToList();

            var stack = new FileStack();
            var extraFiles = new List<string>();

            foreach (var file in list)
            {
                Match match = null;
                string expression = null;

                // For directories, dummy up an extension otherwise the expressions will fail
                var regexInput = file.Type == FileInfoType.File
                    ? file.FullName
                    : file.FullName + ".mkv";

                regexInput = Path.GetFileName(regexInput);

                foreach (var exp in _options.FileStackingExpressions)
                {
                    // (Title)(Volume)(Ignore)(Extension)
                    match = Regex.Match(regexInput, exp, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        expression = exp;
                        break;
                    }
                }

                if (match != null && match.Success)
                {
                    var newStackName = match.Groups[1].Value + match.Groups[3].Value;

                    if (stack.Files.Count == 0)
                    {
                        stack.Name = newStackName;
                        stack.Files.Add(file.FullName);
                        stack.Type = file.Type;
                        stack.Expression = expression;
                        continue;
                    }

                    if (string.Equals(stack.Name, newStackName, StringComparison.OrdinalIgnoreCase) &&
                        stack.Type == file.Type)
                    {
                        stack.Files.Add(file.FullName);
                        continue;
                    }
                }

                if (stack.Files.Count > 1)
                {
                    result.Stacks.Add(stack);
                }
                else if (stack.Files.Count > 0)
                {
                    extraFiles.Add(stack.Files[0]);
                }

                stack = new FileStack();

                if (match != null && match.Success)
                {
                    var newStackName = match.Groups[1].Value + match.Groups[3].Value;

                    stack.Name = newStackName;
                    stack.Files.Add(file.FullName);
                    stack.Type = file.Type;
                    stack.Expression = expression;
                }
            }

            if (stack.Files.Count > 1)
            {
                result.Stacks.Add(stack);
            }

            return result;
        }
    }
}
