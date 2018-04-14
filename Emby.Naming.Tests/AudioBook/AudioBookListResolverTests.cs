using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emby.Naming.AudioBook;
using Emby.Naming.Common;
using MediaBrowser.Model.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emby.Naming.Tests.AudioBook
{
    [TestClass]
    public class AudioBookListResolverTests
    {
        [TestMethod]
        // Detect part number from beginning of file name
        public void TestListResolver1()
        {
            var files = new[]
            {
                @"\\audiobooks\Harry Potter and the Philosopher's Stone\01-The boy who lived.mp3",
                @"\\audiobooks\Harry Potter and the Philosopher's Stone\02-The vanishing glass.mp3",
                @"\\audiobooks\Harry Potter and the Philosopher's Stone\03-The letters from no-one.mp3"
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(1, result[0].Files[0].ChapterNumber);
            Assert.AreEqual(null, result[0].Files[0].PartNumber);
            Assert.AreEqual(2, result[0].Files[1].ChapterNumber);
            Assert.AreEqual(null, result[0].Files[1].PartNumber);
            Assert.AreEqual(3, result[0].Files[2].ChapterNumber);
            Assert.AreEqual(null, result[0].Files[2].PartNumber);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(0, result[0].AlternateVersions.Count);
            Assert.AreEqual("Harry Potter and the Philosopher's Stone", result[0].Name);
        }

        [TestMethod]
        // Detect part number from end of file name
        // Ignore deeply nested folders, and get name only from first parent.
        public void TestListResolver2()
        {
            var files = new[]
            {

                @"\\audiobooks\A Song of Ice and Fire\A Clash of Kings\A Clash of Kings 01.mp3",
                @"\\audiobooks\A Song of Ice and Fire\A Clash of Kings\A Clash of Kings 02.mp3",
                @"\\audiobooks\A Song of Ice and Fire\A Clash of Kings\A Clash of Kings 03.mp3",
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(1, result[0].Files[0].PartNumber);
            Assert.AreEqual(null, result[0].Files[0].ChapterNumber);
            Assert.AreEqual(2, result[0].Files[1].PartNumber);
            Assert.AreEqual(null, result[0].Files[1].ChapterNumber);
            Assert.AreEqual(3, result[0].Files[2].PartNumber);
            Assert.AreEqual(null, result[0].Files[2].ChapterNumber);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(0, result[0].AlternateVersions.Count);
            Assert.AreEqual("A Clash of Kings", result[0].Name);
        }

        [TestMethod]
        // Combine test 1 and 2, but with all the files sent to resolver.
        public void TestListResolver3()
        {
            var files = new[]
            {
                @"\\audiobooks\Harry Potter and the Philosopher's Stone\01-The boy who lived.mp3",
                @"\\audiobooks\Harry Potter and the Philosopher's Stone\02-The vanishing glass.mp3",
                @"\\audiobooks\Harry Potter and the Philosopher's Stone\03-The letters from no-one.mp3",
                @"\\audiobooks\A Song of Ice and Fire\A Clash of Kings\A Clash of Kings 01.mp3",
                @"\\audiobooks\A Song of Ice and Fire\A Clash of Kings\A Clash of Kings 02.mp3",
                @"\\audiobooks\A Song of Ice and Fire\A Clash of Kings\A Clash of Kings 03.mp3",
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            // Stacks are sorted by name, so A Clash of Kings comes before Harry Potter in output.
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, result[1].Files.Count);
            Assert.AreEqual(1, result[1].Files[0].ChapterNumber);
            Assert.AreEqual(null, result[1].Files[0].PartNumber);
            Assert.AreEqual(2, result[1].Files[1].ChapterNumber);
            Assert.AreEqual(null, result[1].Files[1].PartNumber);
            Assert.AreEqual(3, result[1].Files[2].ChapterNumber);
            Assert.AreEqual(null, result[1].Files[2].PartNumber);
            Assert.AreEqual(0, result[1].Extras.Count);
            Assert.AreEqual(0, result[1].AlternateVersions.Count);
            Assert.AreEqual("Harry Potter and the Philosopher's Stone", result[1].Name);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(1, result[0].Files[0].PartNumber);
            Assert.AreEqual(null, result[0].Files[0].ChapterNumber);
            Assert.AreEqual(2, result[0].Files[1].PartNumber);
            Assert.AreEqual(null, result[0].Files[1].ChapterNumber);
            Assert.AreEqual(3, result[0].Files[2].PartNumber);
            Assert.AreEqual(null, result[0].Files[2].ChapterNumber);
            Assert.AreEqual(0, result[0].Extras.Count);
            Assert.AreEqual(0, result[0].AlternateVersions.Count);
            Assert.AreEqual("A Clash of Kings", result[0].Name);
        }

        [TestMethod]
        // A single audio file, without any numbers. Ignore any extra files (some may be included as extras later).
        public void TestListResolver4()
        {
            var files = new[]
            {
                @"\\audiobooks\The World of Ice and Fire - The Untold History of Westeros and the Game of Thrones\The World of Ice and Fire - The Untold History of Westeros and the Game of Thrones.m4b",
                @"\\audiobooks\The World of Ice and Fire - The Untold History of Westeros and the Game of Thrones\isbn.txt"
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Files.Count);
            Assert.AreEqual(null, result[0].Files[0].PartNumber);
            Assert.AreEqual(null, result[0].Files[0].ChapterNumber);
            Assert.AreEqual("The World of Ice and Fire - The Untold History of Westeros and the Game of Thrones", result[0].Name);
        }

        [TestMethod]
        // Both chapters and parts, in random order. (output files are sorted by chapter -> part -> name)
        public void TestListResolver5()
        {
            var files = new[]
            {
                @"\\audiobooks\The Last Guardian\00001_the_last_guardian_03.mp3",
                @"\\audiobooks\The Last Guardian\00002_the_last_guardian_02.mp3",
                @"\\audiobooks\The Last Guardian\00001_the_last_guardian_01.mp3",
                @"\\audiobooks\The Last Guardian\00001_the_last_guardian_02.mp3",
                @"\\audiobooks\The Last Guardian\00002_the_last_guardian_01.mp3",
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(5, result[0].Files.Count);
            Assert.AreEqual(1, result[0].Files[0].PartNumber);
            Assert.AreEqual(1, result[0].Files[0].ChapterNumber);
            Assert.AreEqual(3, result[0].Files[2].PartNumber);
            Assert.AreEqual(1, result[0].Files[2].ChapterNumber);
            Assert.AreEqual(2, result[0].Files[4].PartNumber);
            Assert.AreEqual(2, result[0].Files[4].ChapterNumber);
            Assert.AreEqual("The Last Guardian", result[0].Name);
        }

        [TestMethod]
        // Numbers in chapter title
        public void TestListResolver6()
        {
            var files = new[]
            {
                @"\\audiobooks\Tides of Darkness\01 Chapter title one.mp3",
                @"\\audiobooks\Tides of Darkness\02 The 3 little piggies.mp3",
                @"\\audiobooks\Tides of Darkness\03 Chapter title three.mp3",
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(null, result[0].Files[0].PartNumber);
            Assert.AreEqual(1, result[0].Files[0].ChapterNumber);
            // Part number is null, because the number 3 is not last in file name.
            Assert.AreEqual(null, result[0].Files[1].PartNumber);
            Assert.AreEqual(2, result[0].Files[1].ChapterNumber);
            Assert.AreEqual(null, result[0].Files[2].PartNumber);
            Assert.AreEqual(3, result[0].Files[2].ChapterNumber);
            Assert.AreEqual("Tides of Darkness", result[0].Name);
        }

        [TestMethod]
        // Numbers in chapter title that has part number as well
        public void TestListResolver7()
        {
            var files = new[]
            {
                @"\\audiobooks\Tides of Darkness\01 The_3_Little_Piggies pt-1.mp3",
                @"\\audiobooks\Tides of Darkness\01 The_3_Little_Piggies pt-2.mp3",
                @"\\audiobooks\Tides of Darkness\02 Some_Other_Title pt-3.mp3",
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(1, result[0].Files[0].PartNumber);
            Assert.AreEqual(1, result[0].Files[0].ChapterNumber);
            // Part is corretly set to 1, and chapter is 2, even though the title contains the number 3.
            Assert.AreEqual(2, result[0].Files[1].PartNumber);
            Assert.AreEqual(1, result[0].Files[1].ChapterNumber);

            Assert.AreEqual(3, result[0].Files[2].PartNumber);
            Assert.AreEqual(2, result[0].Files[2].ChapterNumber);
            Assert.AreEqual("Tides of Darkness", result[0].Name);
        }

        [TestMethod]
        // Number in title, without chapter first.
        public void TestListResolver8()
        {
            var files = new[]
            {
                @"\\audiobooks\Harry Potter and the Half-Blood Prince\HP6 - Part 01.mp3",
                @"\\audiobooks\Harry Potter and the Half-Blood Prince\HP6 - Part 02.mp3",
                @"\\audiobooks\Harry Potter and the Half-Blood Prince\HP6 - Part 03.mp3",
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            // Chapters are null, because they are not in beginning of file name
            Assert.AreEqual(1, result[0].Files[0].PartNumber);
            Assert.AreEqual(null, result[0].Files[0].ChapterNumber);
            Assert.AreEqual(2, result[0].Files[1].PartNumber);
            Assert.AreEqual(null, result[0].Files[1].ChapterNumber);
            Assert.AreEqual(3, result[0].Files[2].PartNumber);
            Assert.AreEqual(null, result[0].Files[2].ChapterNumber);
            Assert.AreEqual("Harry Potter and the Half-Blood Prince", result[0].Name);
        }

        [TestMethod]
        // Title with number that comes after both chapter and part
        public void TestListResolver9()
        {
            var files = new[]
            {
                @"\\audiobooks\Tides of Darkness\0001_001 The_3_Little_Piggies.mp3",
                @"\\audiobooks\Tides of Darkness\0001_002 Prologue.mp3",
                @"\\audiobooks\Tides of Darkness\0001_003 Some_Other_Title.mp3",
            };
            var resolver = GetResolver();
            var result = resolver.Resolve(files.Select(i => new FileSystemMetadata
            {
                IsDirectory = false,
                FullName = i

            }).ToList()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].Files.Count);
            Assert.AreEqual(1, result[0].Files[0].PartNumber);
            Assert.AreEqual(1, result[0].Files[0].ChapterNumber);
            Assert.AreEqual("Tides of Darkness", result[0].Name);
        }

        private AudioBookListResolver GetResolver()
        {
            var options = new NamingOptions();
            return new AudioBookListResolver(options);
        }
    }
}
