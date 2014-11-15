
namespace MediaBrowser.Naming.Videos
{
    /// <summary>
    /// Represents a single video file
    /// </summary>
    public class VideoFileInfo
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }
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
        /// Gets or sets a value indicating whether this instance is multi part.
        /// </summary>
        /// <value><c>true</c> if this instance is multi part; otherwise, <c>false</c>.</value>
        public bool IsMultiPart { get; set; }
        /// <summary>
        /// Gets or sets the format3 d.
        /// </summary>
        /// <value>The format3 d.</value>
        public string Format3D { get; set; }
        /// <summary>
        /// Gets or sets the quality.
        /// </summary>
        /// <value>The quality.</value>
        public string Quality { get; set; }
        /// <summary>
        /// Gets or sets the type of the extra, e.g. trailer, theme song, behing the scenes, etc.
        /// </summary>
        /// <value>The type of the extra.</value>
        public string ExtraType { get; set; }
    }
}
