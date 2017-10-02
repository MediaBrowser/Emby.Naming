using System;
using System.Collections.Generic;
using System.Text;
using Emby.Naming.AudioBook;
using Emby.Naming.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emby.Naming.Tests.AudioBook
{
    [TestClass]
    public class AudioBookResolverTests
    {
        [TestMethod]
        public void TestFileName1()
        {
            var path = @"\\audiobooks\A Clash of Kings\A Clash of Kings 01.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(null, result.ChapterNumber);
            Assert.AreEqual(1, result.PartNumber);
        }
        [TestMethod]
        public void TestFileName2()
        {
            var path = @"\\audiobooks\A Clash of Kings\00035_011.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(35, result.ChapterNumber);
            Assert.AreEqual(11, result.PartNumber);
        }
        [TestMethod]
        public void TestFileName3()
        {
            var path = @"\\audiobooks\A Clash of Kings\CH-01 Chapter One Title pt-02.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(1, result.ChapterNumber);
            Assert.AreEqual(2, result.PartNumber);
        }
        [TestMethod]
        public void TestFileName4()
        {
            var path = @"\\audiobooks\A Clash of Kings\Chapter 16 Some chapter title part 5.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(16, result.ChapterNumber);
            Assert.AreEqual(5, result.PartNumber);
        }
        [TestMethod]
        public void TestFileName5()
        {
            var path = @"\\audiobooks\A Clash of Kings\A Clash of Kings ch4pt15.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(4, result.ChapterNumber);
            Assert.AreEqual(15, result.PartNumber);
        }
        [TestMethod]
        public void TestFileName6()
        {
            var path = @"\\audiobooks\A Clash of Kings\12 A Clash of Kings 4.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(12, result.ChapterNumber);
            Assert.AreEqual(4, result.PartNumber);
        }
        [TestMethod]
        public void TestFileName7()
        {
            var path = @"\\audiobooks\A Clash of Kings\ch 10 some title.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(10, result.ChapterNumber);
            Assert.AreEqual(null, result.PartNumber);
        }
        [TestMethod]
        public void TestFileName8()
        {
            var path = @"\\audiobooks\A Clash of Kings\Warcraft 3 An interesting chapter title 24.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(null, result.ChapterNumber);
            Assert.AreEqual(24, result.PartNumber);
        }
        [TestMethod]
        public void TestFileName9()
        {
            var path = @"\\audiobooks\A Clash of Kings\Warcraft 3 ch_4 An interesting chapter title pt_24.mp3";
            var resolver = GetResolver();
            var result = resolver.Resolve(path);

            Assert.AreEqual(4, result.ChapterNumber);
            Assert.AreEqual(24, result.PartNumber);
        }

        private AudioBookResolver GetResolver()
        {
            var options = new ExtendedNamingOptions();
            return new AudioBookResolver(options);
        }
    }
}
