using Emby.Naming.Common;
using Emby.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using MediaBrowser.Model.Logging;
using System;

namespace Emby.Naming.Tests.Video
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
                GetParser().ResolveFile(@"C:\\Users\\media\\Desktop\\Video Test\\Movies\\Oblivion\\Oblivion.dvd.disc".AsSpan());

            Assert.AreEqual("Oblivion", result.Name);
        }

        private void Test(string path, bool isStub, string stubType)
        {
            var options = new NamingOptions();
            var parser = new StubResolver(options);

            var resultStubType = parser.ResolveFile(path.AsSpan());

            Assert.AreEqual(isStub, !string.IsNullOrEmpty(resultStubType));

            if (stubType == null)
            {
                Assert.IsNull(resultStubType);
            }
            else
            {
                Assert.AreEqual(stubType, resultStubType, true, CultureInfo.InvariantCulture);
            }
        }
    }
}
