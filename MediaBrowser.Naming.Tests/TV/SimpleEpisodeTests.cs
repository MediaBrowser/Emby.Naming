using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using MediaBrowser.Model.Logging;

namespace MediaBrowser.Naming.Tests.TV
{
    [TestClass]
    public class SimpleEpisodeTests
    {
        [TestMethod]
        public void TestSimpleEpisodePath1()
        {
            Test(@"\\server\anything_s01e02.mp4", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath2()
        {
            Test(@"\\server\anything_s1e2.mp4", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath3()
        {
            Test(@"\\server\anything_s01.e02.mp4", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath4()
        {
            Test(@"\\server\anything_s01_e02.mp4", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath5()
        {
            Test(@"\\server\anything_102.mp4", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath6()
        {
            Test(@"\\server\anything_1x02.mp4", "anything", 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath7()
        {
            Test(@"\\server\The Walking Dead 4x01.mp4", "The Walking Dead", 4, 1);
        }

        [TestMethod]
        public void TestSimpleEpisodePath8()
        {
            Test(@"\\server\the_simpsons-s02e01_18536.mp4", "the_simpsons", 2, 1);
        }
        

        [TestMethod]
        public void TestSimpleEpisodePath9()
        {
            Test(@"\\server\Temp\S01E02 foo.mp4", string.Empty, 1, 2);
        }

        [TestMethod]
        public void TestSimpleEpisodePath10()
        {
            Test(@"Series\4-12 - The Woman.mp4", string.Empty, 4, 12);
        }

        [TestMethod]
        public void TestSimpleEpisodePath11()
        {
            Test(@"Series\4x12 - The Woman.mp4", string.Empty, 4, 12);
        }

        private void Test(string path, string seriesName, int? seasonNumber, int? episodeNumber)
        {
            var options = new NamingOptions();

            var result = new EpisodeResolver(options, new NullLogger(), new RegexProvider())
                .Resolve(path, false);

            Assert.AreEqual(seasonNumber, result.SeasonNumber);
            Assert.AreEqual(episodeNumber, result.EpisodeNumber);
            Assert.AreEqual(seriesName, result.SeriesName, true, CultureInfo.InvariantCulture);
        }
    }
}
