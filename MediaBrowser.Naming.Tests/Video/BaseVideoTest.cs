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

        protected VideoFileParser GetParser()
        {
            var options = new VideoOptions();

            return new VideoFileParser(options, new NullLogger());
        }
    }
}
