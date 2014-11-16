using MediaBrowser.Naming.Audio;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class MultiPartTests : BaseVideoTest
    {
        [TestMethod]
        public void TestMultiPartFiles()
        {
            TestFile(@"Braveheart.mkv", false, "Braveheart");
            TestFile(@"Braveheart-trailer.mkv", false, "Braveheart-trailer");
            TestFile(@"Braveheart - 480p.mkv", false, "Braveheart - 480p");
            TestFile(@"Braveheart - 720p.mkv", false, "Braveheart - 720p");

            TestFile(@"Braveheart.3d.sbs.mkv", false, "Braveheart.3d.sbs");
            TestFile(@"300.3d.sbs.mkv", false, "300.3d.sbs");
     
            TestFile(@"blah blah.mkv", false, "blah blah");

            TestFile(@"blah blah - cd1.mkv", true, "blah blah");
            TestFile(@"blah blah - disc1.mkv", true, "blah blah");
            TestFile(@"blah blah - disk1.mkv", true, "blah blah");
            TestFile(@"blah blah - pt1.mkv", true, "blah blah");
            TestFile(@"blah blah - part1.mkv", true, "blah blah");
            TestFile(@"blah blah - dvd1.mkv", true, "blah blah");

            // Add a space
            TestFile(@"blah blah - cd 1.mkv", true, "blah blah");
            TestFile(@"blah blah - disc 1.mkv", true, "blah blah");
            TestFile(@"blah blah - disk 1.mkv", true, "blah blah");
            TestFile(@"blah blah - pt 1.mkv", true, "blah blah");
            TestFile(@"blah blah - part 1.mkv", true, "blah blah");
            TestFile(@"blah blah - dvd 1.mkv", true, "blah blah");

            // Not case sensitive
            TestFile(@"blah blah - Disc1.mkv", true, "blah blah");

            // See if the -2 fools it into matching part-X
            TestFile(@"\\server\\Movies\\Braveheart (2007)\\Braveheart (2006)-1.mkv", false, "Braveheart (2006)-1");
            TestFile(@"\\server\\Movies\\300 (2007)\\300 (2006)-2.mkv", false, "300 (2006)-2");

            // See if the -trailer fools it into matching part-AZ
            TestFile(@"\\server\\Movies\\Braveheart (2007)\\Braveheart (2006)-trailer.mkv", false, "Braveheart (2006)-trailer");
            TestFile(@"\\server\\Movies\\300 (2007)\\300 (2006)-trailer.mkv", false, "300 (2006)-trailer");
        }

        [TestMethod]
        public void TestMultiPartFolders()
        {
            TestDirectory(@"blah blah", false, @"blah blah");
            TestDirectory(@"d:\\music\weezer\\03 Pinkerton", false, "03 Pinkerton");
            TestDirectory(@"d:\\music\\michael jackson\\Bad (2012 Remaster)", false, "Bad (2012 Remaster)");

            TestDirectory(@"blah blah - cd1", true, "blah blah");
            TestDirectory(@"blah blah - disc1", true, "blah blah");
            TestDirectory(@"blah blah - disk1", true, "blah blah");
            TestDirectory(@"blah blah - pt1", true, "blah blah");
            TestDirectory(@"blah blah - part1", true, "blah blah");
            TestDirectory(@"blah blah - dvd1", true, "blah blah");

            // Add a space
            TestDirectory(@"blah blah - cd 1", true, "blah blah");
            TestDirectory(@"blah blah - disc 1", true, "blah blah");
            TestDirectory(@"blah blah - disk 1", true, "blah blah");
            TestDirectory(@"blah blah - pt 1", true, "blah blah");
            TestDirectory(@"blah blah - part 1", true, "blah blah");
            TestDirectory(@"blah blah - dvd 1", true, "blah blah");

            // Not case sensitive
            TestDirectory(@"blah blah - Disc1", true, "blah blah");
        }

        private void TestFile(string path, bool isMultiPart, string name)
        {
            var options = new VideoOptions();
            var parser = new MultiPartParser(options, new AudioOptions(), new NullLogger());

            var result = parser.Parse(path, FileInfoType.File);

            Assert.AreEqual(isMultiPart, result.IsMultiPart);

            if (name == null)
            {
                Assert.IsNull(result.Name);
            }
            else
            {
                Assert.AreEqual(name, result.Name, true, CultureInfo.InvariantCulture);
            }
        }
        
        private void TestDirectory(string path, bool isMultiPart, string name)
        {
            var options = new VideoOptions();
            var parser = new MultiPartParser(options, new AudioOptions(), new NullLogger());

            var result = parser.Parse(path, FileInfoType.Directory);

            Assert.AreEqual(isMultiPart, result.IsMultiPart);

            if (name == null)
            {
                Assert.IsNull(result.Name);
            }
            else
            {
                Assert.AreEqual(name, result.Name, true, CultureInfo.InvariantCulture);
            }
        }
    }
}
