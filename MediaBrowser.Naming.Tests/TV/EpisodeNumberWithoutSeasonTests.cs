using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Logging;

namespace MediaBrowser.Naming.Tests.TV
{
    [TestClass]
    public class EpisodeNumberWithoutSeasonTests
    {
        [TestMethod]
        public void TestEpisodeNumberWithoutSeason1()
        {
            Assert.AreEqual(8, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons.S25E08.Steal this episode.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason2()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons - 02 - Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason3()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\02.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason4()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\02 - Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason5()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\02-Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason6()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\02.EpName.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason7()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons - 02.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason8()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons - 02 Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason9()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 5 - 02 - Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason10()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 5 - 02 Ep Name.avi"));
        }

        private int? GetEpisodeNumberFromFile(string path)
        {
            var options = new ExtendedNamingOptions();

            var result = new EpisodeResolver(options, new NullLogger(), new RegexProvider())
                .Resolve(path, false);

            return result.EpisodeNumber;
        }

    }
}
