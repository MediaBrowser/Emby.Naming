using System.Collections.Generic;

namespace MediaBrowser.Naming.Video
{
    public class ExtraResult
    {
        /// <summary>
        /// Gets or sets the type of the extra.
        /// </summary>
        /// <value>The type of the extra.</value>
        public string ExtraType { get; set; }
        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        public List<string> Tokens { get; set; }

        public ExtraResult()
        {
            Tokens = new List<string>();
        }
    }
}
