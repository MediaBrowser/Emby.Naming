using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.IO;
using MediaBrowser.Naming.Logging;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class VideoListResolverTests
    {
        [TestMethod]
        public void TestStackAndExtras()
        {
            // No stacking here because there is no part/disc/etc
            var files = new[]
            {
                "Harry Potter and the Deathly Hallows-trailer.mkv",
                "Harry Potter and the Deathly Hallows.trailer.mkv",
                "Harry Potter and the Deathly Hallows part1.mkv",
                "Harry Potter and the Deathly Hallows part2.mkv",
                "Harry Potter and the Deathly Hallows part3.mkv",
                "Harry Potter and the Deathly Hallows part4.mkv",
                "Batman-deleted.mkv",
                "Batman-sample.mkv",
                "Batman-trailer.mkv",
                "Batman part1.mkv",
                "Batman part2.mkv",
                "Batman part3.mkv",
                "Avengers.mkv",
                "Avengers-trailer.mkv",

                // Despite having a keyword in the name that will return an ExtraType, there's no original video to match it to
                // So this is just a standalone video
                "trailer.mkv",

                // Same as above
                "WillyWonka-trailer.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(5, result.Count);

            Assert.AreEqual(3, result[1].Files.Count);
            Assert.AreEqual(3, result[1].Extras.Count);
            Assert.AreEqual("Batman", result[1].Name);
            
            Assert.AreEqual(4, result[2].Files.Count);
            Assert.AreEqual(2, result[2].Extras.Count);
            Assert.AreEqual("Harry Potter and the Deathly Hallows", result[2].Name);
        }

        [TestMethod]
        public void TestWithMetadata()
        {
            var files = new[]
            {
                "300.mkv",
                "300.nfo"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestWithExtra()
        {
            var files = new[]
            {
                "300.mkv",
                "300 trailer.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestVariationWithFolderName()
        {
            var files = new[]
            {
                "X-Men Days of Future Past - 1080p.mkv",
                "X-Men Days of Future Past-trailer.mp4"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestMultiEdition()
        {
            var files = new[]
            {
                "X-Men Days of Future Past - 1080p.mkv",
                "X-Men Days of Future Past-trailer.mp4",
                "X-Men Days of Future Past - [hsbs].mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestTrailer2()
        {
            var files = new[]
            {
                "X-Men Days of Future Past - 1080p.mkv",
                "X-Men Days of Future Past-trailer.mp4",
                "X-Men Days of Future Past-trailer2.mp4"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestDifferentNames()
        {
            var files = new[]
            {
                "Looper (2012)-trailer.mkv",
                "Looper.2012.bluray.720p.x264.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestSeparateFiles()
        {
            // These should be considered separate, unrelated videos
            var files = new[]
            {
                "My video 1.mkv",
                "My video 2.mkv",
                "My video 3.mkv",
                "My video 4.mkv",
                "My video 5.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void TestMultiDisc()
        {
            var files = new[]
            {
                @"M:\Movies (DVD)\Movies (Musical)\Sound of Music (1965)\Sound of Music Disc 1",
                @"M:\Movies (DVD)\Movies (Musical)\Sound of Music (1965)\Sound of Music Disc 2"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.Directory,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestPoundSign()
        {
            // These should be considered separate, unrelated videos
            var files = new[]
            {
                @"My movie #1.mp4",
                @"My movie #2.mp4"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.Directory,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestStackedWithTrailer()
        {
            var files = new[]
            {
                @"No (2012) part1.mp4",
                @"No (2012) part2.mp4",
                @"No (2012) part1-trailer.mp4"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestStackedWithTrailer2()
        {
            var files = new[]
            {
                @"No (2012) part1.mp4",
                @"No (2012) part2.mp4",
                @"No (2012)-trailer.mp4"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new PortableFileInfo
            {
                Type = FileInfoType.File,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
        }

        private VideoListResolver GetResolver()
        {
            var options = new ExtendedNamingOptions();
            return new VideoListResolver(options, new NullLogger());
        }
    }
}
