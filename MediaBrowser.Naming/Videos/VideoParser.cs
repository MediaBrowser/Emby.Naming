using System.Text.RegularExpressions;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaBrowser.Naming.Videos
{
    public class VideoParser
    {
        private ILogger _logger;
        private readonly VideoOptions _options;

        public VideoParser(VideoOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Parses a video file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>VideoFileInfo.</returns>
        public VideoFileInfo ParseFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            var extension = Path.GetExtension(path);
            if (!_options.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                return null;
            }

            var info = new VideoFileInfo
            {
                Path = path
            };

            return info;
        }

        /// <summary>
        /// Parses a list of files into videos
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns>List&lt;VideoInfo&gt;.</returns>
        public List<VideoInfo> ParseFiles(IEnumerable<PortableFileInfo> files)
        {
            return new List<VideoInfo>();
        }

        /// <summary>
        /// Gets the regex.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Regex.</returns>
        private Regex GetRegex(string expression)
        {
            return new Regex(expression, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Determines whether [is multi part file] [the specified path].
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if [is multi part file] [the specified path]; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">path</exception>
        public bool IsMultiPartFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            path = Path.GetFileName(path);

            return _options.FileStackingExpressions.Any(i => GetRegex(i).Match(path).Success);
        }

        /// <summary>
        /// Determines whether [is multi part folder] [the specified path].
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if [is multi part folder] [the specified path]; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">path</exception>
        public bool IsMultiPartFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            path = Path.GetFileName(path);

            return _options.FolderStackingExpressions.Any(i => GetRegex(i).Match(path).Success);
        }
    }
}
