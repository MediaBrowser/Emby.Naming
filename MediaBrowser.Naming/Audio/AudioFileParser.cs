using System;
using System.IO;
using System.Linq;

namespace MediaBrowser.Naming.Audio
{
    public class AudioFileParser
    {
        private readonly AudioOptions _options;

        public AudioFileParser(AudioOptions options)
        {
            _options = options;
        }

        public bool IsAudioFile(string path)
        {
            var extension = Path.GetExtension(path) ?? string.Empty;
            return _options.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
