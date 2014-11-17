using MediaBrowser.Naming.Audio;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using System;
using System.IO;
using System.Linq;

namespace MediaBrowser.Naming.Video
{
    public class VideoResolver
    {
        private readonly ILogger _logger;
        private readonly VideoOptions _options;
        private readonly AudioOptions _audioOptions;

        public VideoResolver(VideoOptions options, AudioOptions audioOptions, ILogger logger)
        {
            _options = options;
            _audioOptions = audioOptions;
            _logger = logger;
        }

        /// <summary>
        /// Resolves the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>VideoFileInfo.</returns>
        public VideoFileInfo ResolveDirectory(string path)
        {
            return Resolve(path, FileInfoType.Directory);
        }

        /// <summary>
        /// Resolves the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>VideoFileInfo.</returns>
        public VideoFileInfo ResolveFile(string path)
        {
            return Resolve(path, FileInfoType.File);
        }

        /// <summary>
        /// Resolves the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="type">The type.</param>
        /// <returns>VideoFileInfo.</returns>
        /// <exception cref="System.ArgumentNullException">path</exception>
        public VideoFileInfo Resolve(string path, FileInfoType type)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            var isStub = false;
            string container = null;
            string stubType = null;
            StubResult stubResult = null;

            if (type == FileInfoType.File)
            {
                var extension = Path.GetExtension(path) ?? string.Empty;
                // Check supported extensions
                if (!_options.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    stubResult = new StubResolver(_options, _logger).ResolveFile(path);

                    isStub = stubResult.IsStub;

                    // It's not supported. Check stub extensions
                    if (!isStub)
                    {
                        return null;
                    }

                    stubType = stubResult.StubType;
                }

                container = extension.TrimStart('.');
            }

            var flags = new FlagParser(_options).GetFlags(path);
            var format3DResult = GetFormat3DInfo(flags);

            var extraResult = new ExtraTypeParser(_options, _audioOptions, _logger).GetExtraInfo(path);

            var name = Path.GetFileName(path);

            var cleanDateTimeResult = CleanDateTime(name);

            name = cleanDateTimeResult.Name;
            name = CleanString(name).Name;

            var info = new VideoFileInfo
            {
                Path = path,
                Container = container,
                IsStub = isStub,
                Name = name,
                Year = cleanDateTimeResult.Year,
                StubType = stubType,
                Is3D = format3DResult.Is3D,
                Format3D = format3DResult.Format3D,
                ExtraType = extraResult.ExtraType
            };

            return info;
        }

        public bool IsVideoFile(string path)
        {
            var extension = Path.GetExtension(path) ?? string.Empty;
            return _options.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }

        public CleanStringResult CleanString(string name)
        {
            return new CleanStringParser().Clean(name, _options.CleanStrings);
        }

        public CleanDateTimeResult CleanDateTime(string name)
        {
            return new CleanDateTimeParser().Clean(name, _options.CleanDateTimes);
        }

        private Format3DResult GetFormat3DInfo(string[] flags)
        {
            return new Format3DParser(_options, _logger).Parse(flags);
        }
    }
}
