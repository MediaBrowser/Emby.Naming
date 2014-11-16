using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBrowser.Naming.Audio
{
    public class AudioOptions
    {
        public List<string> FileExtensions { get; set; }

        public AudioOptions()
        {
            FileExtensions = new List<string>()
            {
                ".mp3",
                ".flac",
                ".wma",
                ".aac",
                ".acc",
                ".m4a",
                ".m4b",
                ".wav",
                ".ape",
                ".ogg",
                ".oga"
            };
        }
    }
}
