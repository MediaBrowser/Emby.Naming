using System.Collections.Generic;

namespace MediaBrowser.Naming.Audio
{
    public class AudioOptions
    {
        public List<string> FileExtensions { get; set; }
        public List<string> AlbumStackingPrefixes { get; set; }

        public AudioOptions()
        {
            AlbumStackingPrefixes = new List<string>
            {
                "disc", 
                "cd", 
                "disk", 
                "vol", 
                "volume"
            };

            FileExtensions = new List<string>()
            {
                ".nsv",
                ".m4a", 
                ".flac", 
                ".aac", 
                ".strm", 
                ".pls", 
                ".rm", 
                ".mpa", 
                ".wav", 
                ".wma", 
                ".ogg", 
                ".mp3", 
                ".mp2", 
                ".m3u", 
                ".mod", 
                ".amf", 
                ".669", 
                ".dmf", 
                ".dsm", 
                ".far", 
                ".gdm", 
                ".imf", 
                ".it", 
                ".m15", 
                ".med", 
                ".okt", 
                ".s3m", 
                ".stm", 
                ".sfx", 
                ".ult", 
                ".uni", 
                ".xm", 
                ".sid", 
                ".ac3", 
                ".dts", 
                ".cue", 
                ".aif", 
                ".aiff", 
                ".wpl", 
                ".ape", 
                ".mac", 
                ".mpc", 
                ".mp+", 
                ".mpp", 
                ".shn", 
                ".zip", 
                ".rar", 
                ".wv", 
                ".nsf", 
                ".spc", 
                ".gym", 
                ".adplug", 
                ".adx", 
                ".dsp", 
                ".adp", 
                ".ymf", 
                ".ast", 
                ".afc", 
                ".hps", 
                ".xsp",
                ".acc",
                ".m4b",
                ".oga"
            };
        }
    }
}
