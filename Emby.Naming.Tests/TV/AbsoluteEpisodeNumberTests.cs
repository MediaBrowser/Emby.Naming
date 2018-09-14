using MediaBrowser.Model.Logging;
using Emby.Naming.Common;
using Emby.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Emby.Naming.Tests.TV
{
    [TestClass]
    public class AbsoluteEpisodeNumberTests
    {
        [TestMethod]
        public void TestAbsoluteEpisodeNumber1()
        {
            Assert.AreEqual(12, GetEpisodeNumberFromFile(@"The Simpsons\12.avi"));
        }

        [TestMethod]
        public void TestAbsoluteEpisodeNumber2()
        {
            Assert.AreEqual(12, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 12.avi"));
        }

        [TestMethod]
        public void TestAbsoluteEpisodeNumber3()
        {
            Assert.AreEqual(82, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 82.avi"));
        }

        [TestMethod]
        public void TestAbsoluteEpisodeNumber4()
        {
            Assert.AreEqual(112, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 112.avi"));
        }

        [TestMethod]
        public void TestAbsoluteEpisodeNumber5()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\Foo_ep_02.avi"));
        }

        private int? GetEpisodeNumberFromFile(string path)
        {
            var options = new NamingOptions();

            var result = new EpisodeResolver(options)
                .Resolve(path, false, null, null, true);

            return result.EpisodeNumber;
        }
    }
}
