using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using MediaBrowser.Model.Logging;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class StubTests : BaseVideoTest
    {
        [TestMethod]
        public void TestStubs()
        {
            Test("video.mkv", false, null);
            Test("video.disc", true, null);
            Test("video.dvd.disc", true, "dvd");
            Test("video.hddvd.disc", true, "hddvd");
            Test("video.bluray.disc", true, "bluray");
            Test("video.brrip.disc", true, "bluray");
            Test("video.bd25.disc", true, "bluray");
            Test("video.bd50.disc", true, "bluray");
            Test("video.vhs.disc", true, "vhs");
            Test("video.hdtv.disc", true, "tv");
            Test("video.pdtv.disc", true, "tv");
            Test("video.dsr.disc", true, "tv");
        }

        [TestMethod]
        public void TestStubName()
        {
            var result =
                GetParser().ResolveFile(@"C:\\Users\\media\\Desktop\\Video Test\\Movies\\Oblivion\\Oblivion.dvd.disc");

            Assert.AreEqual("Oblivion", result.Name);
        }

        private void Test(string path, bool isStub, string stubType)
        {
            var options = new NamingOptions();
            var parser = new StubResolver(options, new NullLogger());

            var result = parser.ResolveFile(path);

            Assert.AreEqual(isStub, result.IsStub);

            if (stubType == null)
            {
                Assert.IsNull(result.StubType);
            }
            else
            {
                Assert.AreEqual(stubType, result.StubType, true, CultureInfo.InvariantCulture);
            }
        }
    }
}
