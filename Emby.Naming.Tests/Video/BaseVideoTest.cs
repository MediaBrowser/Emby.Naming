using MediaBrowser.Model.Logging;
using Emby.Naming.Common;
using Emby.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emby.Naming.Tests.Video
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
            var options = new NamingOptions();

            return new VideoResolver(options);
        }
    }
}
