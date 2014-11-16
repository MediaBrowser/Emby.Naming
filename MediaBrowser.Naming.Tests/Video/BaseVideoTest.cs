using MediaBrowser.Naming.Audio;
using MediaBrowser.Naming.Logging;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    public class BaseVideoTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        protected VideoResolver GetParser()
        {
            var options = new ExpandedVideoOptions();

            return new VideoResolver(options, new AudioOptions(), new NullLogger());
        }
    }
}
