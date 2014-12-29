using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class MultiVersionTests
    {
        [TestMethod]
        public void TestMultiEdition1()
        {
            var files = new[]
            {
                @"\\movies\X-Men Days of Future Past\X-Men Days of Future Past - 1080p.mkv",
                @"\\movies\X-Men Days of Future Past\X-Men Days of Future Past-trailer.mp4",
                @"\\movies\X-Men Days of Future Past\X-Men Days of Future Past - [hsbs].mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Extras.Count);
        }

        [TestMethod]
        public void TestMultiEdition2()
        {
            var files = new[]
            {
                @"\\movies\X-Men Days of Future Past\X-Men Days of Future Past - apple.mkv",
                @"\\movies\X-Men Days of Future Past\X-Men Days of Future Past-trailer.mp4",
                @"\\movies\X-Men Days of Future Past\X-Men Days of Future Past - banana.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Extras.Count);
            Assert.AreEqual(1, result[0].AlternateVersions.Count);
        }

        private VideoListResolver GetResolver()
        {
            var options = new ExtendedNamingOptions();
            return new VideoListResolver(options, new NullLogger());
        }
    }
}
