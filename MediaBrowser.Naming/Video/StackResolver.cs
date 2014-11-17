using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediaBrowser.Naming.Logging;

namespace MediaBrowser.Naming.Video
{
    public class StackResolver
    {
        private readonly VideoOptions _options;
        private readonly ILogger _logger;

        public StackResolver(VideoOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        public StackResult StackFiles(IEnumerable<string> files)
        {
            var result = new StackResult();

            var list = files
                .OrderBy(i => i)
                .ToList();

            string currentExpression = null;
            var stack = new FileStack();
            var extraFiles = new List<string>();

            foreach (var file in list)
            {
                Match match = null;

                if (currentExpression == null)
                {
                    foreach (var exp in _options.FileStackingExpressions)
                    {
                        // (Title)(Volume)(Ignore)(Extension)
                        match = Regex.Match(file, exp, RegexOptions.IgnoreCase);

                        if (match.Success)
                        {
                            currentExpression = exp;
                            break;
                        }
                    }
                }
                else
                {
                    match = Regex.Match(file, currentExpression, RegexOptions.IgnoreCase);
                }

                if (match != null && match.Success)
                {
                    var newStackName = match.Groups[1].Value + match.Groups[3].Value;

                    if (stack.Files.Count == 0)
                    {
                        stack.Name = newStackName;
                        stack.Files.Add(file);
                        continue;
                    }

                    if (string.Equals(stack.Name, newStackName, StringComparison.OrdinalIgnoreCase))
                    {
                        stack.Files.Add(file);
                        continue;
                    }
                }
                
                if (stack.Files.Count > 1)
                {
                    result.Stacks.Add(stack);
                    stack = new FileStack();
                    currentExpression = null;
                }
                else if (stack.Files.Count > 0)
                {
                    extraFiles.Add(stack.Files[0]);
                    stack = new FileStack();
                    currentExpression = null;
                }
                else
                {
                    currentExpression = null;
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
