
namespace MediaBrowser.Naming.Subtitles
{
    public class SubtitleOptions
    {
        public string[] FileExtensions { get; set; }
        public char[] FlagDelimiters { get; set; }

        public string[] ForcedFlags { get; set; }
        public string[] DefaultFlags { get; set; }

        public SubtitleOptions()
        {
            FileExtensions = new[]
            {
                ".srt", 
                ".ssa", 
                ".ass", 
                ".sub"
            };

            FlagDelimiters = new[]
            {
                '.'
            };

            ForcedFlags = new[]
            {
                "foreign",
                "forced"
            };

            DefaultFlags = new[]
            {
                "default"
            };
        }
    }
}
