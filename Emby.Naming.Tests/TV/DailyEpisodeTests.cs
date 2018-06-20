using MediaBrowser.Model.Logging;
using Emby.Naming.Common;
using Emby.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emby.Naming.Tests.TV
{
    [TestClass]
    public class DailyEpisodeTests
    {
        [TestMethod]
        public void TestDailyEpisode1()
        {
            Test(@"\\server\anything_1996.11.14.mp4", "anything", 1996, 11, 14);
        }

        [TestMethod]
        public void TestDailyEpisode2()
        {
            Test(@"\\server\anything_1996-11-14.mp4", "anything", 1996, 11, 14);
        }

        [TestMethod]
        public void TestDailyEpisode3()
        {
            Test(@"\\server\anything_14.11.1996.mp4", "anything", 1996, 11, 14);
        }

        [TestMethod]
        public void TestDailyEpisode4()
        {
            Test(@"\\server\A Daily Show - (2015-01-15) - Episode Name - [720p].mkv", "A Daily Show", 2015, 01, 15);
        }

        [TestMethod]
        public void TestDailyEpisode5()
        {
            Test(@"\\server\james.corden.2017.04.20.anne.hathaway.720p.hdtv.x264-crooks.mkv", "james.corden", 2017, 04, 20);
        }

        [TestMethod]
        public void TestDailyEpisode6()
        {
            Test(@"\\server\ABC News 2018_03_24_19_00_00.mkv", "james.corden", 2018, 03, 24);
        }

        [TestMethod]
        public void TestDailyEpisode7()
        {
            Test(@"\\server\Last Man Standing_KTLADT_2018_05_25_01_28_00.wtv", "Last Man Standing", 2018, 05, 25);
        }

        private void Test(string path, string seriesName, int? year, int? month, int? day)
        {
            var options = new NamingOptions();

            var result = new EpisodeResolver(options)
                .Resolve(path, false);

            Assert.IsNull(result.SeasonNumber);
            Assert.IsNull(result.EpisodeNumber);
            Assert.AreEqual(year, result.Year);
            Assert.AreEqual(month, result.Month);
            Assert.AreEqual(day, result.Day);
            //Assert.AreEqual(seriesName, result.SeriesName, true, CultureInfo.InvariantCulture);
        }
    }
}
