using MediaBrowser.Naming.Logging;
using MediaBrowser.Naming.Video;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class StackTests : BaseVideoTest
    {
        [TestMethod]
        public void TestSimpleStack()
        {
            var files = new[]
            {
                "Bad Boys (2006) part1.mkv",
                "Bad Boys (2006) part2.mkv",
                "Bad Boys (2006) part3.mkv",
                "Bad Boys (2006) part4.mkv",
                "Bad Boys (2006)-trailer.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.StackFiles(files);

            Assert.AreEqual(1, result.Stacks.Count);
            TestStackInfo(result.Stacks[0], "Bad Boys (2006)", 4);
        }

        [TestMethod]
        public void TestDirtyNames()
        {
            var files = new[]
            {
                "Bad Boys (2006).part1.stv.unrated.multi.1080p.bluray.x264-rough.mkv",
                "Bad Boys (2006).part2.stv.unrated.multi.1080p.bluray.x264-rough.mkv",
                "Bad Boys (2006).part3.stv.unrated.multi.1080p.bluray.x264-rough.mkv",
                "Bad Boys (2006).part4.stv.unrated.multi.1080p.bluray.x264-rough.mkv",
                "Bad Boys (2006)-trailer.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.StackFiles(files);

            Assert.AreEqual(1, result.Stacks.Count);
            TestStackInfo(result.Stacks[0], "Bad Boys (2006).stv.unrated.multi.1080p.bluray.x264-rough", 4);
        }

        [TestMethod]
        public void TestNumberedFiles()
        {
            var files = new[]
            {
                "Bad Boys (2006).mkv",
                "Bad Boys (2006) 1.mkv",
                "Bad Boys (2006) 2.mkv",
                "Bad Boys (2006) 3.mkv",
                "Bad Boys (2006)-trailer.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.StackFiles(files);

            Assert.AreEqual(0, result.Stacks.Count);
        }

        [TestMethod]
        public void TestSimpleStackWithNumericName()
        {
            var files = new[]
            {
                "300 (2006) part1.mkv",
                "300 (2006) part2.mkv",
                "300 (2006) part3.mkv",
                "300 (2006) part4.mkv",
                "300 (2006)-trailer.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.StackFiles(files);

            Assert.AreEqual(1, result.Stacks.Count);
            TestStackInfo(result.Stacks[0], "300 (2006)", 4);
        }

        [TestMethod]
        public void TestMixedExpressionsNotAllowed()
        {
            var files = new[]
            {
                "Bad Boys (2006) part1.mkv",
                "Bad Boys (2006) part2.mkv",
                "Bad Boys (2006) part3.mkv",
                "Bad Boys (2006) parta.mkv",
                "Bad Boys (2006)-trailer.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.StackFiles(files);

            Assert.AreEqual(1, result.Stacks.Count);
            TestStackInfo(result.Stacks[0], "Bad Boys (2006)", 3);
        }

        [TestMethod]
        public void TestDualStacks()
        {
            var files = new[]
            {
                "Bad Boys (2006) part1.mkv",
                "Bad Boys (2006) part2.mkv",
                "Bad Boys (2006) part3.mkv",
                "Bad Boys (2006) part4.mkv",
                "Bad Boys (2006)-trailer.mkv",
                "300 (2006) part1.mkv",
                "300 (2006) part2.mkv",
                "300 (2006) part3.mkv",
                "300 (2006)-trailer.mkv"
            };

            var resolver = GetResolver();

            var result = resolver.StackFiles(files);

            Assert.AreEqual(2, result.Stacks.Count);
            TestStackInfo(result.Stacks[1], "Bad Boys (2006)", 4);
            TestStackInfo(result.Stacks[0], "300 (2006)", 3);
        }

        private void TestStackInfo(FileStack stack, string name, int fileCount)
        {
            Assert.AreEqual(fileCount, stack.Files.Count);
            Assert.AreEqual(name, stack.Name);
        }

        private StackResolver GetResolver()
        {
            return new StackResolver(new VideoOptions(), new NullLogger());
        }
    }
}
