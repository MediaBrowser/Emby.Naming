using MediaBrowser.Naming.Videos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Videos
{
    [TestClass]
    public class MultiPartTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private VideoParser GetParser()
        {
            var options = new VideoOptions();

            return new VideoParser(options);
        }

        [TestMethod]
        public void TestMultiPartFiles()
        {
            var parser = GetParser();

            Assert.IsFalse(parser.IsMultiPartFile(@"Braveheart.mkv"));
            Assert.IsFalse(parser.IsMultiPartFile(@"Braveheart - 480p.mkv"));
            Assert.IsFalse(parser.IsMultiPartFile(@"Braveheart - 720p.mkv"));

            Assert.IsFalse(parser.IsMultiPartFile(@"blah blah.mkv"));

            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - cd1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - disc1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - disk1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - pt1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - part1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - dvd1.mkv"));

            // Add a space
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - cd 1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - disc 1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - disk 1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - pt 1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - part 1.mkv"));
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - dvd 1.mkv"));

            // Not case sensitive
            Assert.IsTrue(parser.IsMultiPartFile(@"blah blah - Disc1.mkv"));
        }

        [TestMethod]
        public void TestMultiPartFolders()
        {
            var parser = GetParser();
            
            Assert.IsFalse(parser.IsMultiPartFolder(@"blah blah"));
            Assert.IsFalse(parser.IsMultiPartFolder(@"d:\\music\weezer\\03 Pinkerton"));
            Assert.IsFalse(parser.IsMultiPartFolder(@"d:\\music\\michael jackson\\Bad (2012 Remaster)"));

            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - cd1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - disc1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - disk1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - pt1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - part1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - dvd1"));

            // Add a space
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - cd 1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - disc 1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - disk 1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - pt 1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - part 1"));
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - dvd 1"));

            // Not case sensitive
            Assert.IsTrue(parser.IsMultiPartFolder(@"blah blah - Disc1"));
        }

    }
}
