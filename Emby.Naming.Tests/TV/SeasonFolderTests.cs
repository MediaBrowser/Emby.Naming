using Emby.Naming.Common;
using Emby.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emby.Naming.Tests.TV
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

        [TestMethod]
        public void TestGetSeasonNumberFromPath14()
        {
            Assert.IsNull(GetSeasonNumberFromPath(@"\Drive\Season (8)"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath13()
        {
            Assert.AreEqual(3, GetSeasonNumberFromPath(@"\Drive\3.Staffel"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath15()
        {
            Assert.IsNull(GetSeasonNumberFromPath(@"\Drive\s06e05"));
        }

        [TestMethod]
        public void TestGetSeasonNumberFromPath16()
        {
            Assert.IsNull(GetSeasonNumberFromPath(@"\Drive\The.Legend.of.Condor.Heroes.2017.V2.web-dl.1080p.h264.aac-hdctv"));
        }

        private int? GetSeasonNumberFromPath(string path)
        {
            var options = new NamingOptions();

            var result = new SeasonPathParser(options)
                .Parse(path, true, true);

            return result.SeasonNumber;
        }
    }
}
