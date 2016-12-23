using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.TV
{
    [TestClass]
    public class SeasonFolderTests
    {
        [TestMethod]
        public void TestGetSeasonNumberFromPath1()
        {
            Assert.AreEqual(1, GetSeasonNumberFromPath(@"\Drive\Season 1"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath2()
        {
            Assert.AreEqual(2, GetSeasonNumberFromPath(@"\Drive\Season 2"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath3()
        {
            Assert.AreEqual(2, GetSeasonNumberFromPath(@"\Drive\Season 02"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath4()
        {
            Assert.AreEqual(1, GetSeasonNumberFromPath(@"\Drive\Season 1"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath5()
        {
            Assert.AreEqual(2, GetSeasonNumberFromPath(@"\Drive\Seinfeld\S02"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath6()
        {
            Assert.AreEqual(2, GetSeasonNumberFromPath(@"\Drive\Seinfeld\2"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath7()
        {
            Assert.AreEqual(2009, GetSeasonNumberFromPath(@"\Drive\Season 2009"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath8()
        {
            Assert.AreEqual(1, GetSeasonNumberFromPath(@"\Drive\Season1"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath9()
        {
            Assert.AreEqual(4, GetSeasonNumberFromPath(@"The Wonder Years\The.Wonder.Years.S04.PDTV.x264-JCH"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath10()
        {
            Assert.AreEqual(7, GetSeasonNumberFromPath(@"\Drive\Season 7 (2016)"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath11()
        {
            Assert.AreEqual(7, GetSeasonNumberFromPath(@"\Drive\Staffel 7 (2016)"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath12()
        {
            Assert.AreEqual(7, GetSeasonNumberFromPath(@"\Drive\Stagione 7 (2016)"));
        }

        private int? GetSeasonNumberFromPath(string path)
        {
            var options = new ExtendedNamingOptions();

            var result = new SeasonPathParser(options, new RegexProvider())
                .Parse(path, true, true);

            return result.SeasonNumber;
        }
    }
}
