using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Logging;

namespace MediaBrowser.Naming.Tests.TV
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

        private void Test(string path, string seriesName, int? year, int? month, int? day)
        {
            var options = new NamingOptions();

            var result = new EpisodeResolver(options, new NullLogger(), new RegexProvider())
                .Resolve(path, FileInfoType.File);

            Assert.IsNull(result.SeasonNumber);
            Assert.IsNull(result.EpisodeNumber);
            Assert.AreEqual(year, result.Year);
            Assert.AreEqual(month, result.Month);
            Assert.AreEqual(day, result.Day);
            //Assert.AreEqual(seriesName, result.SeriesName, true, CultureInfo.InvariantCulture);
        }
    }
}
