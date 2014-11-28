using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.TV
{
    [TestClass]
    public class EpisodeWithoutSeasonTests
    {
        [TestMethod]
        public void TestWithoutSeason1()
        {
            Test(@"\\server\anything_ep02.ext", "anything", null, 2);
        }

        [TestMethod]
        public void TestWithoutSeason2()
        {
            Test(@"\\server\anything_ep_02.ext", "anything", null, 2);
        }

        [TestMethod]
        public void TestWithoutSeason3()
        {
            Test(@"\\server\anything_part.II.ext", "anything", null, 2);
        }

        [TestMethod]
        public void TestWithoutSeason4()
        {
            Test(@"\\server\anything_pt.II.ext", "anything", null, 2);
        }

        [TestMethod]
        public void TestWithoutSeason5()
        {
            Test(@"\\server\anything_pt_II.ext", "anything", null, 2);
        }

        private void Test(string path, string seriesName, int? seasonNumber, int? episodeNumber)
        {
            var options = new NamingOptions();

            var result = new EpisodePathParser(options, new RegexProvider())
                .Parse(path, FileInfoType.File);

            Assert.AreEqual(seasonNumber, result.SeasonNumber);
            Assert.AreEqual(episodeNumber, result.EpsiodeNumber);
            //Assert.AreEqual(seriesName, result.SeriesName, true, CultureInfo.InvariantCulture);
        }
    }
}
