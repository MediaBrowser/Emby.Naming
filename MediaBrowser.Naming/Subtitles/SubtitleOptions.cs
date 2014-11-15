using System.Collections.Generic;

namespace MediaBrowser.Naming.Subtitles
{
    public class SubtitleOptions
    {
        public List<string> FileExtensions { get; set; }

        public SubtitleOptions()
        {
            FileExtensions = new List<string>()
            {
                ".srt", 
                ".ssa", 
                ".ass", 
                ".sub"
            };
        }
    }
}
