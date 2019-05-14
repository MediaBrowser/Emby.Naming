﻿using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Emby.Naming.Tests.Video
{
    [TestClass]
    public class CleanDateTimeTests : BaseVideoTest
    {
        [TestMethod]
        public void TestCleanDateTime()
        {
            Test(@"The Wolf of Wall Street (2013).mkv", "The Wolf of Wall Street", 2013);
            Test(@"The Wolf of Wall Street 2 (2013).mkv", "The Wolf of Wall Street 2", 2013);
            Test(@"The Wolf of Wall Street - 2 (2013).mkv", "The Wolf of Wall Street - 2", 2013);
            Test(@"The Wolf of Wall Street 2001 (2013).mkv", "The Wolf of Wall Street 2001", 2013);

            Test(@"300 (2006).mkv", "300", 2006);
            Test(@"d:\\movies\\300 (2006).mkv", "300", 2006);
            Test(@"300 2 (2006).mkv", "300 2", 2006);
            Test(@"300 - 2 (2006).mkv", "300 - 2", 2006);
            Test(@"300 2001 (2006).mkv", "300 2001", 2006);

            Test(@"curse.of.chucky.2013.stv.unrated.multi.1080p.bluray.x264-rough", "curse.of.chucky", 2013);
            Test(@"curse.of.chucky.2013.stv.unrated.multi.2160p.bluray.x264-rough", "curse.of.chucky", 2013);

            Test(@"\\server\\Movies\\300 (2007)\\300 (2006).bluray.disc", "300", 2006);
        }

        [TestMethod]
        public void TestCleanDateTime1()
        {
            Test(@"Arrival.2016.2160p.Blu-Ray.HEVC.mkv", "Arrival", 2016);
        }

        [TestMethod]
        public void TestCleanDateTimeWithoutFileExtension()
        {
            Test(@"The Wolf of Wall Street (2013)", "The Wolf of Wall Street", 2013);
            Test(@"The Wolf of Wall Street 2 (2013)", "The Wolf of Wall Street 2", 2013);
            Test(@"The Wolf of Wall Street - 2 (2013)", "The Wolf of Wall Street - 2", 2013);
            Test(@"The Wolf of Wall Street 2001 (2013)", "The Wolf of Wall Street 2001", 2013);

            Test(@"300 (2006)", "300", 2006);
            Test(@"d:\\movies\\300 (2006)", "300", 2006);
            Test(@"300 2 (2006)", "300 2", 2006);
            Test(@"300 - 2 (2006)", "300 - 2", 2006);
            Test(@"300 2001 (2006)", "300 2001", 2006);

            Test(@"\\server\\Movies\\300 (2007)\\300 (2006)", "300", 2006);
            Test(@"\\server\\Movies\\300 (2007)\\300 (2006).mkv", "300", 2006);
        }

        [TestMethod]
        public void TestCleanDateTimeWithoutDate()
        {
            Test(@"American.Psycho.mkv", "American.Psycho.mkv", null);
            Test(@"American Psycho.mkv", "American Psycho.mkv", null);
        }

        [TestMethod]
        public void TestCleanDateTimeWithBracketedName()
        {
            Test(@"[rec].mkv", "[rec].mkv", null);
        }

        [TestMethod]
        public void TestCleanDateTimeWithoutExtension()
        {
            Test(@"St. Vincent (2014)", "St. Vincent", 2014);
        }

        [TestMethod]
        public void TestCleanDateTimeWithoutDate1()
        {
            Test("Super movie(2009).mp4", "Super movie", 2009);
        }

        [TestMethod]
        public void TestCleanDateTimeWithoutParenthesis()
        {
            Test("Drug War 2013.mp4", "Drug War", 2013);
        }

        [TestMethod]
        public void TestCleanDateTimeWithMultipleYears()
        {
            Test("My Movie (1997) - GreatestReleaseGroup 2019.mp4", "My Movie", 1997);
        }

        [TestMethod]
        public void TestCleanDateTimeWithYearAndResolution()
        {
            Test("First Man 2018 1080p.mkv", "First Man", 2018);
        }

        [TestMethod]
        public void TestCleanDateTimeWithYearAndResolution1()
        {
            Test("First Man (2018) 1080p.mkv", "First Man", 2018);
        }

        [TestMethod]
        public void TestCleanDateTimeWithSceneRelease()
        {
            Test("Maximum Ride - 2016 - WEBDL-1080p - x264 AC3.mkv", "Maximum Ride", 2016);
        }

        [TestMethod]
        public void TestYearInBrackets()
        {
            Test("Robin Hood [Multi-Subs] [2018].mkv", "Robin Hood", 2018);
        }

        private void Test(string input, string expectedName, int? expectedYear)
        {
            input = Path.GetFileName(input);

            var result = GetParser().CleanDateTime(input.AsSpan());

            Assert.AreEqual(expectedName, result.Name, true, CultureInfo.InvariantCulture);
            Assert.AreEqual(expectedYear, result.Year);
        }

        [TestMethod]
        public void TestCleanDateAndStringsSequence()
        {
            // In this test case, running CleanDateTime first produces no date, so it will attempt to run CleanString first and then CleanDateTime again

            Test(@"3.Days.to.Kill.2014.720p.BluRay.x264.YIFY.mkv", "3.Days.to.Kill", 2014);
        }
    }
}
