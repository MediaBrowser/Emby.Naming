using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.TV
{
    [TestClass]
    public class MultiEpisodeTests
    {
        [TestMethod]
        public void TestGetEndingEpisodeNumberFromFile()
        {
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\4x01 – 20 Hours in America (1).mkv"));

            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\01x02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\S01x02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\S01E02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\S01xE02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\seriesname 01x02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\seriesname S01x02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\seriesname S01E02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\seriesname S01xE02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2\02x03 - 04 Ep Name.mp4"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2\My show name 02x03 - 04 Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2\Elementary - 02x03 - 02x04 - 02x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2\02x03 - 02x04 - 02x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2\02x03-04-15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2\Elementary - 02x03-04-15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 02\02x03-E15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 02\Elementary - 02x03-E15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 02\02x03 - x04 - x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 02\Elementary - 02x03 - x04 - x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 02\02x03x04x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 02\Elementary - 02x03x04x15 - Ep Name.mp4"));
            Assert.AreEqual(26, GetEndingEpisodeNumberFromFile(@"Season 1\Elementary - S01E23-E24-E26 - The Woman.mp4"));
            Assert.AreEqual(26, GetEndingEpisodeNumberFromFile(@"Season 1\S01E23-E24-E26 - The Woman.mp4"));


            //Four Digits seasons
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2009\2009x02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2009\S2009x02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2009\S2009E02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2009\S2009xE02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2009\seriesname 2009x02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2009\seriesname S2009x02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2009\seriesname S2009E02 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2009\seriesname S2009xE02 blah.avi"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03 - 2009x04 - 2009x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\2009x03 - 2009x04 - 2009x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\2009x03-04-15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03-04-15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\2009x03-E15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03-E15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\2009x03 - x04 - x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03 - x04 - x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\2009x03x04x15 - Ep Name.mp4"));
            Assert.AreEqual(15, GetEndingEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03x04x15 - Ep Name.mp4"));
            Assert.AreEqual(26, GetEndingEpisodeNumberFromFile(@"Season 2009\Elementary - S2009E23-E24-E26 - The Woman.mp4"));
            Assert.AreEqual(26, GetEndingEpisodeNumberFromFile(@"Season 2009\S2009E23-E24-E26 - The Woman.mp4"));

            //Without season number
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\02 - blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2\02 - blah 14 blah.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\02 - blah-02 a.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2\02.avi"));

            Assert.AreEqual(3, GetEndingEpisodeNumberFromFile(@"Season 1\02-03 - blah.avi"));
            Assert.AreEqual(4, GetEndingEpisodeNumberFromFile(@"Season 2\02-04 - blah 14 blah.avi"));
            Assert.AreEqual(5, GetEndingEpisodeNumberFromFile(@"Season 1\02-05 - blah-02 a.avi"));
            Assert.AreEqual(4, GetEndingEpisodeNumberFromFile(@"Season 2\02-04.avi"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 2\[HorribleSubs] Hunter X Hunter - 136 [720p].mkv"));

            // With format specification that must not be detected as ending episode number
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\series-s09e14-1080p.mkv"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\series-s09e14-720p.mkv"));
            Assert.AreEqual(null, GetEndingEpisodeNumberFromFile(@"Season 1\series-s09e14-720i.mkv"));
            Assert.AreEqual(4, GetEndingEpisodeNumberFromFile(@"Season 1\MOONLIGHTING_s01e01-e04.mkv"));
        }

        [TestMethod]
        public void TestGetEndingEpisodeNumberFromFolder()
        {
            Assert.AreEqual(4, GetEndingEpisodeNumberFromFolder(@"Season 1\MOONLIGHTING_s01e01-e04"));
        }
        
        private int? GetEndingEpisodeNumberFromFolder(string path)
        {
            var options = new ExtendedNamingOptions();

            var result = new EpisodePathParser(options, new RegexProvider())
                .Parse(path, true, true);

            return result.EndingEpsiodeNumber;
        }

        private int? GetEndingEpisodeNumberFromFile(string path)
        {
            var options = new ExtendedNamingOptions();

            var result = new EpisodePathParser(options, new RegexProvider())
                .Parse(path, false, true);

            return result.EndingEpsiodeNumber;
        }
    }
}
