using System.Collections.Generic;

namespace MediaBrowser.Naming.Videos
{
    public class VideoOptions
    {
        public List<string> FileStackingExpressions { get; set; }
        public List<string> FolderStackingExpressions { get; set; }
        public List<string> FileExtensions { get; set; }
        public List<string> IgnoreExpressions { get; set; }

        public VideoOptions()
        {
            // TODO: Replace with Kodi expressions?
            FileStackingExpressions = new List<string>
            {
                @"(.*?)([ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck]|d)[ _.-]*[0-9]+)(.*?)(\.[^.]+)$"
            };

            // TODO: Replace with Kodi expressions?
            FolderStackingExpressions = new List<string>()
            {
                @"(.*?)([ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck]|d)[ _.-]*[0-9]+)$"
            };

            // TODO: These are from Kodi. Add extensions from Media Browser, if any are missing
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
                ".zip"
            };

            IgnoreExpressions = new List<string>
            {

            };
        }
    }
}
