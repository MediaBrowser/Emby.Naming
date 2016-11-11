using MediaBrowser.Model.Logging;
using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.TV
{
    [TestClass]
    public class SeasonNumberTests
    {
        private int? GetSeasonNumberFromEpisodeFile(string path)
        {
            var options = new ExtendedNamingOptions();

            var result = new EpisodeResolver(options, new NullLogger(), new RegexProvider())
                .Resolve(path, false);

            return result.SeasonNumber;
        }

        [TestMethod]
        public void TestSeasonNumber1()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"\Show\Season 02\S02E03 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber2()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\seriesname S01x02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber3()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\S01x02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber4()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\seriesname S01xE02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber5()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\01x02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber6()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\S01E02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber7()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\S01xE02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber8()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\seriesname 01x02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber9()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\seriesname S01x02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber10()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\seriesname S01E02 blah.avi"));
        }

        [TestMethod]
        public void TestSeasonNumber11()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 2\Elementary - 02x03 - 02x04 - 02x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber12()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 2\02x03 - 02x04 - 02x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber13()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 2\02x03-04-15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber14()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 2\Elementary - 02x03-04-15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber15()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 02\02x03-E15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber16()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 02\Elementary - 02x03-E15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber17()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 02\02x03 - x04 - x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber18()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 02\Elementary - 02x03 - x04 - x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber19()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 02\02x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber20()
        {
            Assert.AreEqual(2, GetSeasonNumberFromEpisodeFile(@"Season 02\Elementary - 02x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber21()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\Elementary - S01E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber22()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Season 1\S01E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber23()
        {
            Assert.AreEqual(25, GetSeasonNumberFromEpisodeFile(@"Season 25\The Simpsons.S25E09.Steal this episode.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber24()
        {
            Assert.AreEqual(25, GetSeasonNumberFromEpisodeFile(@"The Simpsons\The Simpsons.S25E09.Steal this episode.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber25()
        {
            // This convention is not currently supported, just adding in case we want to look at it in the future
            Assert.AreEqual(2016, GetSeasonNumberFromEpisodeFile(@"2016\Season s2016e1.mp4"));
        }

        [TestMethod]
        public void TestSeasonNumber26()
        {
            // This convention is not currently supported, just adding in case we want to look at it in the future
            Assert.AreEqual(2016, GetSeasonNumberFromEpisodeFile(@"2016\Season 2016x1.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber1()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\2009x02 blah.avi"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber2()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\S2009x02 blah.avi"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber3()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\S2009E02 blah.avi"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber4()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\S2009xE02 blah.avi"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber5()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\seriesname 2009x02 blah.avi"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber6()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\seriesname S2009x02 blah.avi"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber7()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\seriesname S2009E02 blah.avi"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber8()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\Elementary - 2009x03 - 2009x04 - 2009x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber9()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\2009x03 - 2009x04 - 2009x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber10()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\2009x03-04-15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber11()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\Elementary - 2009x03 - x04 - x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber12()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\2009x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber13()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\Elementary - 2009x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber14()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\Elementary - S2009E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber15()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\S2009E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber16()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\Elementary - 2009x03 - x04 - x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber17()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\2009x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber18()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\Elementary - 2009x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber19()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\Elementary - S2009E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestFourDigitSeasonNumber20()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromEpisodeFile(@"Season 2009\S2009E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestNoSeriesFolder()
        {
            Assert.AreEqual(1, GetSeasonNumberFromEpisodeFile(@"Series\1-12 - The Woman.mp4"));
        }
    }
}
