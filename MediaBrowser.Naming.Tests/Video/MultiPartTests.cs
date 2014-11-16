using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class MultiPartTests : BaseVideoTest
    {
        [TestMethod]
        public void TestMultiPartFiles()
        {
            Assert.IsFalse(IsMultiPartFile(@"d:\\Braveheart.mkv"));
            Assert.IsFalse(IsMultiPartFile(@"Braveheart - 480p.mkv"));
            Assert.IsFalse(IsMultiPartFile(@"Braveheart - 720p.mkv"));

            Assert.IsFalse(IsMultiPartFile(@"blah blah.mkv"));

            Assert.IsTrue(IsMultiPartFile(@"blah blah - cd1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - disc1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - disk1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - pt1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - part1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - dvd1.mkv"));

            // Add a space
            Assert.IsTrue(IsMultiPartFile(@"blah blah - cd 1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - disc 1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - disk 1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - pt 1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - part 1.mkv"));
            Assert.IsTrue(IsMultiPartFile(@"blah blah - dvd 1.mkv"));

            // Not case sensitive
            Assert.IsTrue(IsMultiPartFile(@"blah blah - Disc1.mkv"));
        }

        [TestMethod]
        public void TestMultiPartFolders()
        {
            Assert.IsFalse(IsMultiPartDirectory(@"blah blah"));
            Assert.IsFalse(IsMultiPartDirectory(@"d:\\music\weezer\\03 Pinkerton"));
            Assert.IsFalse(IsMultiPartDirectory(@"d:\\music\\michael jackson\\Bad (2012 Remaster)"));

            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - cd1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - disc1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - disk1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - pt1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - part1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - dvd1"));

            // Add a space
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - cd 1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - disc 1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - disk 1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - pt 1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - part 1"));
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - dvd 1"));

            // Not case sensitive
            Assert.IsTrue(IsMultiPartDirectory(@"blah blah - Disc1"));
        }

        private bool IsMultiPartFile(string path)
        {
            var options = new VideoOptions();
            var parser = new MultiPartParser(options, new NullLogger());

            return parser.Parse(path, FileInfoType.File).IsMultiPart;
        }   

        private bool IsMultiPartDirectory(string path)
        {
            var options = new VideoOptions();
            var parser = new MultiPartParser(options, new NullLogger());

            return parser.Parse(path, FileInfoType.Directory).IsMultiPart;
        }
    }
}
