﻿using MediaBrowser.Model.Logging;
using Emby.Naming.Common;
using Emby.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emby.Naming.Tests.TV
{
    [TestClass]
    public class EpisodeNumberWithoutSeasonTests
    {
        [TestMethod]
        public void TestEpisodeNumberWithoutSeason1()
        {
            Assert.AreEqual(8, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons.S25E08.Steal this episode.mp4"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason2()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons - 02 - Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason3()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\02.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason4()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\02 - Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason5()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\02-Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason6()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\02.EpName.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason7()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons - 02.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason8()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons - 02 Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason9()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 5 - 02 - Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason10()
        {
            Assert.AreEqual(2, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 5 - 02 Ep Name.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason11()
        {
            Assert.AreEqual(7, GetEpisodeNumberFromFile(@"Seinfeld\Seinfeld 0807 The Checks.avi"));
            Assert.AreEqual(8, GetSeasonNumberFromFile(@"Seinfeld\Seinfeld 0807 The Checks.avi"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason12()
        {
            Assert.AreEqual(7, GetEpisodeNumberFromFile(@"GJ Club (2013)\GJ Club - 07.mkv"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason13()
        {
            // This is not supported anymore after removing the episode number 365+ hack from EpisodePathParser
            Assert.AreEqual(13, GetEpisodeNumberFromFile(@"Case Closed (1996-2007)\Case Closed - 13.mkv"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason14()
        {
            Assert.AreEqual(3, GetSeasonNumberFromFile(@"Case Closed (1996-2007)\Case Closed - 317.mkv"));
            Assert.AreEqual(17, GetEpisodeNumberFromFile(@"Case Closed (1996-2007)\Case Closed - 317.mkv"));
        }

        [TestMethod]
        public void TestEpisodeNumberWithoutSeason15()
        {
            Assert.AreEqual(2017, GetSeasonNumberFromFile(@"Running Man\Running Man S2017E368.mkv"));
        }

        private int? GetEpisodeNumberFromFile(string path)
        {
            var options = new NamingOptions();

            var result = new EpisodeResolver(options)
                .Resolve(path, false);

            return result.EpisodeNumber;
        }

        private int? GetSeasonNumberFromFile(string path)
        {
            var options = new NamingOptions();

            var result = new EpisodeResolver(options)
                .Resolve(path, false);

            return result.SeasonNumber;
        }

    }
}
