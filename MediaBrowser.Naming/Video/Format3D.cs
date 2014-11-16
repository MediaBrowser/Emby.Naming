using MediaBrowser.Naming.Logging;
using System;
using System.Linq;

namespace MediaBrowser.Naming.Video
{
    public class Format3D
    {
        private readonly VideoOptions _options;
        private readonly ILogger _logger;

        public Format3D(VideoOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        public Format3DResult Parse(string path)
        {
            return Parse(new VideoFileParser(_options, _logger).GetFlags(path));
        }
        
        public Format3DResult Parse(string[] videoFlags)
        {
            var result = new Format3DResult();

            if (string.IsNullOrWhiteSpace(_options.Format3DPrefix))
            {
                result.Format3D = _options.Format3DFlags.FirstOrDefault(i => videoFlags.Contains(i, StringComparer.OrdinalIgnoreCase));
                result.Is3D = !string.IsNullOrWhiteSpace(result.Format3D);
            }
            else
            {
                var foundPrefix = false;
                string format = null;

                foreach (var flag in videoFlags)
                {
                    if (foundPrefix)
                    {
                        if (_options.Format3DFlags.Contains(flag, StringComparer.OrdinalIgnoreCase))
                        {
                            format = flag;
                        }
                        break;
                    }
                    foundPrefix = string.Equals(flag, _options.Format3DPrefix, StringComparison.OrdinalIgnoreCase);
                }

                result.Is3D = foundPrefix;
                result.Format3D = format;
            }

            return result;
        }
    }
}
