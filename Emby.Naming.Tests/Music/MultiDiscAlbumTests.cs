﻿using MediaBrowser.Model.Logging;
using Emby.Naming.Audio;
using Emby.Naming.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emby.Naming.Tests.Music
{
    [TestClass]
    public class MultiDiscAlbumTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMultiDiscAlbums()
        {
            Assert.IsFalse(IsMultiDiscAlbumFolder(@"blah blah"));
            Assert.IsFalse(IsMultiDiscAlbumFolder(@"d:\\music\weezer\\03 Pinkerton"));
            Assert.IsFalse(IsMultiDiscAlbumFolder(@"d:\\music\\michael jackson\\Bad (2012 Remaster)"));

            Assert.IsTrue(IsMultiDiscAlbumFolder(@"cd1"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"disc1"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"disk1"));

            // Add a space
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"cd 1"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"disc 1"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"disk 1"));

            Assert.IsTrue(IsMultiDiscAlbumFolder(@"cd  - 1"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"disc- 1"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"disk - 1"));

            Assert.IsTrue(IsMultiDiscAlbumFolder(@"Disc 01 (Hugo Wolf · 24 Lieder)"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"Disc 04 (Encores and Folk Songs)"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"Disc04 (Encores and Folk Songs)"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"Disc 04(Encores and Folk Songs)"));
            Assert.IsTrue(IsMultiDiscAlbumFolder(@"Disc04(Encores and Folk Songs)"));

            Assert.IsTrue(IsMultiDiscAlbumFolder(@"D:\Video\MBTestLibrary\VideoTest\music\.38 special\anth\Disc 2"));
        }

        [TestMethod]
        public void TestMultiDiscAlbums1()
        {
            Assert.IsFalse(IsMultiDiscAlbumFolder(@"[1985] Oppurtunities (Let's make lots of money) (1985)"));
        }

        [TestMethod]
        public void TestMultiDiscAlbums2()
        {
            Assert.IsFalse(IsMultiDiscAlbumFolder(@"Blah 04(Encores and Folk Songs)"));
        }

        private bool IsMultiDiscAlbumFolder(string path)
        {
            var parser = new AlbumParser(new NamingOptions());

            return parser.ParseMultiPart(path).IsMultiPart;
        }
    }
}
