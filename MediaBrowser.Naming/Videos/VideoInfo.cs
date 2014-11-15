using MediaBrowser.Naming.Subtitles;
using System.Collections.Generic;

namespace MediaBrowser.Naming.Videos
{
    /// <summary>
    /// Represents a complete video, including all parts and subtitles
    /// </summary>
    public class VideoInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int? Year { get; set; }
        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public List<VideoFileInfo> Files { get; set; }
        /// <summary>
        /// Gets or sets the subtitles.
        /// </summary>
        /// <value>The subtitles.</value>
        public List<SubtitleInfo> Subtitles { get; set; }
        /// <summary>
        /// Gets or sets the extras.
        /// </summary>
        /// <value>The extras.</value>
        public List<VideoFileInfo> Extras { get; set; }

        public VideoInfo()
        {
            Files = new List<VideoFileInfo>();
            Subtitles = new List<SubtitleInfo>();
            Extras = new List<VideoFileInfo>();
        }
    }
}
