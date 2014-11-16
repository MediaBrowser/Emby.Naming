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
        public void CleanBasicTest()
        {
            Test("Super movie.3d.hsbs.mp4", true, "hsbs");
            Test("Super movie.3d.mp4", true, null);
            Test("Super movie.hsbs.mp4", false, null);
        }

        private void Test(string input, bool is3D, string format3D)
        {
            var options = new VideoOptions();
            var parser = new Format3D(options, new NullLogger());

            var result = parser.Parse(input);

            Assert.AreEqual(is3D, result.Is3D);
            Assert.AreEqual(format3D, result.Format3D, true, CultureInfo.InvariantCulture);
        }
    }
}
