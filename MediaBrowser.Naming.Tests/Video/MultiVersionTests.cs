using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Logging;

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

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
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

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Extras.Count);
            Assert.AreEqual(1, result[0].AlternateVersions.Count);
        }

        [TestMethod]
        public void TestLetterFolders()
        {
            var files = new[]
            {
                @"\\movies\M\Movie 1.mkv",
                @"\\movies\M\Movie 2.mkv",
                @"\\movies\M\Movie 3.mkv",
                @"\\movies\M\Movie 4.mkv",
                @"\\movies\M\Movie 5.mkv",
                @"\\movies\M\Movie 6.mkv",
                @"\\movies\M\Movie 7.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(7, result.Count);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(0, result[0].AlternateVersions.Count);
        }

        [TestMethod]
        public void TestMultiVersionLimit()
        {
            var files = new[]
            {
                @"\\movies\Movie\Movie.mkv",
                @"\\movies\Movie\Movie-2.mkv",
                @"\\movies\Movie\Movie-3.mkv",
                @"\\movies\Movie\Movie-4.mkv",
                @"\\movies\Movie\Movie-5.mkv",
                @"\\movies\Movie\Movie-6.mkv",
                @"\\movies\Movie\Movie-7.mkv",
                @"\\movies\Movie\Movie-8.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(7, result[0].AlternateVersions.Count);
        }

        [TestMethod]
        public void TestMultiVersionLimit2()
        {
            var files = new[]
            {
                @"\\movies\Mo\Movie 1.mkv",
                @"\\movies\Mo\Movie 2.mkv",
                @"\\movies\Mo\Movie 3.mkv",
                @"\\movies\Mo\Movie 4.mkv",
                @"\\movies\Mo\Movie 5.mkv",
                @"\\movies\Mo\Movie 6.mkv",
                @"\\movies\Mo\Movie 7.mkv",
                @"\\movies\Mo\Movie 8.mkv",
                @"\\movies\Mo\Movie 9.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(9, result.Count);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(0, result[0].AlternateVersions.Count);
        }

        [TestMethod]
        public void TestMultiVersion3()
        {
            var files = new[]
            {
                @"\\movies\Movie\Movie 1.mkv",
                @"\\movies\Movie\Movie 2.mkv",
                @"\\movies\Movie\Movie 3.mkv",
                @"\\movies\Movie\Movie 4.mkv",
                @"\\movies\Movie\Movie 5.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(0, result[0].AlternateVersions.Count);
        }

        [TestMethod]
        public void TestMultiVersion4()
        {
            var files = new[]
            {
                @"\\movies\Iron Man\Iron Man.mkv",
                @"\\movies\Iron Man\Iron Man (2008).mkv",
                @"\\movies\Iron Man\Iron Man (2009).mkv",
                @"\\movies\Iron Man\Iron Man (2010).mkv",
                @"\\movies\Iron Man\Iron Man (2011).mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(0, result[0].AlternateVersions.Count);
        }

        [TestMethod]
        public void TestMultiVersion5()
        {
            var files = new[]
            {
                @"\\movies\Iron Man\Iron Man.mkv",
                @"\\movies\Iron Man\Iron Man-720p.mkv",
                @"\\movies\Iron Man\Iron Man-test.mkv",
                @"\\movies\Iron Man\Iron Man-bluray.mkv",
                @"\\movies\Iron Man\Iron Man-3d.mkv",
                @"\\movies\Iron Man\Iron Man-3d-hsbs.mkv",
                @"\\movies\Iron Man\Iron Man-3d.hsbs.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(6, result[0].AlternateVersions.Count);
            Assert.AreEqual(false, result[0].AlternateVersions[2].Is3D);
            Assert.AreEqual(true, result[0].AlternateVersions[3].Is3D);
            Assert.AreEqual(true, result[0].AlternateVersions[4].Is3D);
        }

        [TestMethod]
        public void TestMultiVersion6()
        {
            var files = new[]
            {
                @"\\movies\Iron Man\Iron Man.mkv",
                @"\\movies\Iron Man\Iron Man - 720p.mkv",
                @"\\movies\Iron Man\Iron Man - test.mkv",
                @"\\movies\Iron Man\Iron Man - bluray.mkv",
                @"\\movies\Iron Man\Iron Man - 3d.mkv",
                @"\\movies\Iron Man\Iron Man - 3d-hsbs.mkv",
                @"\\movies\Iron Man\Iron Man - 3d.hsbs.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(6, result[0].AlternateVersions.Count);
            Assert.AreEqual(false, result[0].AlternateVersions[2].Is3D);
            Assert.AreEqual(true, result[0].AlternateVersions[3].Is3D);
            Assert.AreEqual(true, result[0].AlternateVersions[4].Is3D);
        }

        [TestMethod]
        public void TestMultiVersion7()
        {
            var files = new[]
            {
                @"\\movies\Iron Man\Iron Man - B (2006).mkv",
                @"\\movies\Iron Man\Iron Man - C (2007).mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestMultiVersion8()
        {
            // This is not actually supported yet

            var files = new[]
            {
                @"\\movies\Iron Man\Iron Man.mkv",
                @"\\movies\Iron Man\Iron Man_720p.mkv",
                @"\\movies\Iron Man\Iron Man_test.mkv",
                @"\\movies\Iron Man\Iron Man_bluray.mkv",
                @"\\movies\Iron Man\Iron Man_3d.mkv",
                @"\\movies\Iron Man\Iron Man_3d-hsbs.mkv",
                @"\\movies\Iron Man\Iron Man_3d.hsbs.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(6, result[0].AlternateVersions.Count);
            Assert.AreEqual(false, result[0].AlternateVersions[2].Is3D);
            Assert.AreEqual(true, result[0].AlternateVersions[3].Is3D);
            Assert.AreEqual(true, result[0].AlternateVersions[4].Is3D);
        }

        private VideoListResolver GetResolver()
        {
            var options = new ExtendedNamingOptions();
            return new VideoListResolver(options, new NullLogger());
        }
    }
}
