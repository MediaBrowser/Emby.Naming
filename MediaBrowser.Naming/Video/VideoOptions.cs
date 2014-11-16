using System.Collections.Generic;

namespace MediaBrowser.Naming.Video
{
    public class VideoOptions
    {
        public List<string> FileStackingExpressions { get; set; }
        public List<string> FileExtensions { get; set; }
        public List<string> StubFileExtensions { get; set; }
        public List<string> IgnoreExpressions { get; set; }
        public List<string> CleanDateTimes { get; set; }
        public List<string> CleanStrings { get; set; }
        public char[] FlagDelimiters { get; set; }

        public string Format3DPrefix { get; set; }
        public List<string> Format3DFlags { get; set; }

        public VideoOptions()
        {
            FileStackingExpressions = new List<string>
            {
                @"(.*?)([ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck]|d)[ _.-]*[0-9]+)(.*?)(\.[^.]+)$",
                @"(.*?)([ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck]|d)[ _.-]*[a-d])(.*?)(\.[^.]+)$",
                @"(.*?)([ ._-]*[a-d])(.*?)(\.[^.]+)$"
            };

            FileExtensions = new List<string>()
            {
                ".m4v",
                ".3gp",
                ".nsv", 
                ".ts", 
                ".ty", 
                ".strm", 
                ".rm", 
                ".rmvb", 
                ".m3u", 
                ".ifo", 
                ".mov", 
                ".qt", 
                ".divx", 
                ".xvid", 
                ".bivx", 
                ".vob", 
                ".nrg", 
                ".img",
                ".iso", 
                ".pva", 
                ".wmv", 
                ".asf", 
                ".asx", 
                ".ogm", 
                ".m2v", 
                ".avi", 
                ".bin", 
                ".dat", 
                ".dvr-ms", 
                ".mpg", 
                ".mpeg", 
                ".mp4", 
                ".mkv", 
                ".avc", 
                ".vp3", 
                ".svq3", 
                ".nuv", 
                ".viv", 
                ".dv", 
                ".fli", 
                ".flv", 
                ".rar", 
                ".001", 
                ".wpl", 
                ".zip",
                ".wtv",
                ".ogv",
                ".m2t",
                ".m2ts",
                ".mk3d",
                ".ts",
                ".rmvb",
                ".mov",
                ".avi",
                ".webm",
                ".mts",
                ".rec"

            };

            StubFileExtensions = new List<string>
            {
                ".disc"
            };

            IgnoreExpressions = new List<string>
            {

            };

            FlagDelimiters = new[]
            {
                '(', 
                ')', 
                '-', 
                '.', 
                '_'
            };

            CleanDateTimes = new List<string>
            {
                @"(.+[^ _\,\.\(\)\[\]\-])[ _\.\(\)\[\]\-]+(19[0-9][0-9]|20[0-1][0-9])([ _\,\.\(\)\[\]\-][^0-9]|$)"
            };

            CleanStrings = new List<string>
            {
                @"[ _\,\.\(\)\[\]\-](ac3|dts|custom|dc|divx|divx5|dsr|dsrip|dutch|dvd|dvdrip|dvdscr|dvdscreener|screener|dvdivx|cam|fragment|fs|hdtv|hdrip|hdtvrip|internal|limited|multisubs|ntsc|ogg|ogm|pal|pdtv|proper|repack|rerip|retail|cd[1-9]|r3|r5|bd5|se|svcd|swedish|german|read.nfo|nfofix|unrated|ws|telesync|ts|telecine|tc|brrip|bdrip|480p|480i|576p|576i|720p|720i|1080p|1080i|hrhd|hrhdtv|hddvd|bluray|x264|h264|xvid|xvidvd|xxx|www.www|\[.*\])([ _\,\.\(\)\[\]\-]|$)",
                @"(\[.*\])"
            };

            Format3DPrefix = "3d";
            Format3DFlags = new List<string>
            {
                "hsbs",
                "sbs",
                "htab",
                "tab"
            };
        }
    }
}
