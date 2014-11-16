using MediaBrowser.Naming.Logging;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class Format3DTests : BaseVideoTest
    {
        [TestMethod]
        public void TestKodiFormat3D()
        {
            var options = new VideoOptions();

            Test("Super movie.3d.mp4", false, null, options);
            Test("Super movie.3d.hsbs.mp4", true, "hsbs", options);
            Test("Super movie.3d.sbs.mp4", true, "sbs", options);
            Test("Super movie.3d.htab.mp4", true, "htab", options);
            Test("Super movie.3d.tab.mp4", true, "tab", options);

            Test("Super movie.hsbs.mp4", false, null, options);
            Test("Super movie.sbs.mp4", false, null, options);
            Test("Super movie.htab.mp4", false, null, options);
            Test("Super movie.tab.mp4", false, null, options);
            Test("Super movie.sbs3d.mp4", false, null, options);
        }

        [TestMethod]
        public void TestExpandedFormat3D()
        {
            // These were introduced for Media Browser 3 
            // Kodi conventions are preferred but these still need to be supported
            var options = new ExpandedVideoOptions();

            Test("Super movie.3d.mp4", false, null, options);
            Test("Super movie.3d.hsbs.mp4", true, "hsbs", options);
            Test("Super movie.3d.sbs.mp4", true, "sbs", options);
            Test("Super movie.3d.htab.mp4", true, "htab", options);
            Test("Super movie.3d.tab.mp4", true, "tab", options);

            Test("Super movie.hsbs.mp4", true, "hsbs", options);
            Test("Super movie.sbs.mp4", true, "sbs", options);
            Test("Super movie.htab.mp4", true, "htab", options);
            Test("Super movie.tab.mp4", true, "tab", options);
            Test("Super movie.sbs3d.mp4", true, "sbs3d", options);
            
            Test("Super movie [3d].mp4", false, null, options);
            Test("Super movie [hsbs].mp4", true, "hsbs", options);
            Test("Super movie [fsbs].mp4", true, "fsbs", options);
            Test("Super movie [ftab].mp4", true, "ftab", options);
            Test("Super movie [htab].mp4", true, "htab", options);
            Test("Super movie [sbs3d].mp4", true, "sbs3d", options);
        }

        private void Test(string input, bool is3D, string format3D, VideoOptions options)
        {
            var parser = new Format3DParser(options, new NullLogger());

            var result = parser.Parse(input);

            Assert.AreEqual(is3D, result.Is3D);

            if (format3D == null)
            {
                Assert.IsNull(result.Format3D);
            }
            else
            {
                Assert.AreEqual(format3D, result.Format3D, true, CultureInfo.InvariantCulture);
            }
        }
    }
}
