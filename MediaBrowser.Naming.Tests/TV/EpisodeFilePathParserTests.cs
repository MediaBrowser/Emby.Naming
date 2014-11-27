using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace MediaBrowser.Naming.Tests.TV
{
    [TestClass]
    public class EpisodeFilePathParserTests
    {
        [TestMethod]
        public void TestSimpleEpisodePath()
        {
            Test(@"\\server\anything_s01e02.ext", "anything", 1, 2);
        }
        [TestMethod]
        public void TestSimpleEpisodePath2()
        {
            Test(@"\\server\anything_s1e2.ext", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath3()
        {
            Test(@"\\server\anything_s01.e02.ext", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath4()
        {
            Test(@"\\server\anything_s01_e02.ext", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath5()
        {
            Test(@"\\server\anything_102.ext", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath6()
        {
            Test(@"\\server\anything_1x02.ext", "anything", 1, 2);
        }

        private void Test(string path, string seriesName, int? seasonNumber, int? episodeNumber)
        {
            var options = new NamingOptions();

            var result = new EpisodePathParser(options).Parse(path, FileInfoType.File);

            Assert.AreEqual(seasonNumber, result.SeasonNumber);
            Assert.AreEqual(episodeNumber, result.EpsiodeNumber);
            //Assert.AreEqual(seriesName, result.SeriesName, true, CultureInfo.InvariantCulture);
        }
    }
}
