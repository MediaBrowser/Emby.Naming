using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.TV
{
    [TestClass]
    public class EpisodeNumberTests
    {
        [TestMethod]
        public void TestEpisodeNumber1()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 02\S02E03 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber40()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2\02x03 - 02x04 - 02x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber41()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\01x02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber42()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\S01x02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber43()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\S01E02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber44()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2\Elementary - 02x03-04-15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber45()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\S01xE02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber46()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\seriesname S01E02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber47()
        {
            Assert.AreEqual(36, GetEpisodeNumberFromFile(@"Season 2\[HorribleSubs] Hunter X Hunter - 136 [720p].mkv"));
        }

        [TestMethod]
        public void TestEpisodeNumber50()
        {
            // This convention is not currently supported, just adding in case we want to look at it in the future
            Assert.AreEqual(1, GetEpisodeNumberFromFile(@"2016\Season s2016e1.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber51()
        {
            // This convention is not currently supported, just adding in case we want to look at it in the future
            Assert.AreEqual(1, GetEpisodeNumberFromFile(@"2016\Season 2016x1.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber52()
        {
            Assert.AreEqual(16, GetEpisodeNumberFromFile(@"Season 2\Episode - 16.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber53()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(16, GetEpisodeNumberFromFile(@"Season 2\Episode 16.avi"));
        }
        [TestMethod]
        public void TestEpisodeNumber54()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(16, GetEpisodeNumberFromFile(@"Season 2\Episode 16 - Some Title.avi"));
        }
        [TestMethod]
        public void TestEpisodeNumber55()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(16, GetEpisodeNumberFromFile(@"Season 2\Season 3 Episode 16.avi"));
        }
        [TestMethod]
        public void TestEpisodeNumber56()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(16, GetEpisodeNumberFromFile(@"Season 2\Season 3 Episode 16 - Some Title.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber57()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(16, GetEpisodeNumberFromFile(@"Season 2\16 Some Title.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber58()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(16, GetEpisodeNumberFromFile(@"Season 2\16 - 12 Some Title.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber59()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(7, GetEpisodeNumberFromFile(@"Season 2\7 - 12 Angry Men.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber60()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(16, GetEpisodeNumberFromFile(@"Season 2\16 12 Some Title.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber61()
        {
            // This is not supported. Expected to fail, although it would be a good one to add support for.
            Assert.AreEqual(7, GetEpisodeNumberFromFile(@"Season 2\7 12 Angry Men.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber30()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2\02x03 - 02x04 - 02x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber31()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\seriesname 01x02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber32()
        {
            Assert.AreEqual(9, GetEpisodeNumberFromFile(@"Season 25\The Simpsons.S25E09.Steal this episode.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber33()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\seriesname S01x02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber34()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2\Elementary - 02x03 - 02x04 - 02x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber35()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\seriesname S01xE02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber36()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 02\02x03 - x04 - x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber37()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 02\Elementary - 02x03 - x04 - x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber38()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 02\02x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber39()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 02\Elementary - 02x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber20()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2\02x03-04-15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber21()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 02\02x03-E15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber22()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 02\Elementary - 02x03-E15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber23()
        {
            Assert.AreEqual(23, GetEpisodeNumberFromFile(@"Season 1\Elementary - S01E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber24()
        {
            Assert.AreEqual(23, GetEpisodeNumberFromFile(@"Season 2009\S2009E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber25()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2009\2009x02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber26()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2009\S2009x02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber27()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2009\S2009E02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber28()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2009\seriesname 2009x02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber29()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber11()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\2009x03x04x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber12()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03-E15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber13()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2009\S2009xE02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber14()
        {
            Assert.AreEqual(23, GetEpisodeNumberFromFile(@"Season 2009\Elementary - S2009E23-E24-E26 - The Woman.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber15()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2009\seriesname S2009xE02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber16()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\2009x03-E15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber17()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2009\seriesname S2009E02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber18()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\2009x03 - 2009x04 - 2009x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber19()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\2009x03 - x04 - x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber2()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2009\seriesname S2009x02 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber3()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03 - 2009x04 - 2009x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber4()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03-04-15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber5()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\2009x03-04-15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber6()
        {
            Assert.AreEqual(03, GetEpisodeNumberFromFile(@"Season 2009\Elementary - 2009x03 - x04 - x15 - Ep Name.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumber7()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\02 - blah-02 a.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber8()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 1\02 - blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber9()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2\02 - blah 14 blah.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber10()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2\02.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber48()
        {
            Assert.AreEqual(02, GetEpisodeNumberFromFile(@"Season 2\2. Infestation.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumber49()
        {
            Assert.AreEqual(7, GetEpisodeNumberFromFile(@"The Wonder Years\The.Wonder.Years.S04.PDTV.x264-JCH\The Wonder Years s04e07 Christmas Party NTSC PDTV.avi"));
        }

        private int? GetEpisodeNumberFromFile(string path)
        {
            var options = new ExtendedNamingOptions();

            var result = new EpisodePathParser(options, new RegexProvider())
                .Parse(path, false, true);

            return result.EpisodeNumber;
        }

    }
}
