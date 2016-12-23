using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class VideoResolverTests : BaseVideoTest
    {
        [TestMethod]
        public void TestSimpleFile()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\Brave (2007)\\Brave (2006).mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("Brave", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestSimpleFile2()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\Bad Boys (1995)\\Bad Boys (1995).mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(1995, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("Bad Boys", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestSimpleFileWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006).mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestExtra()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\Brave (2007)\\Brave (2006)-trailer.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("trailer", result.ExtraType);
            Assert.AreEqual("Brave (2006)-trailer", result.Name);
        }

        [TestMethod]
        public void TestExtraWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006)-trailer.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300 (2006)-trailer", result.Name);
            Assert.AreEqual("trailer", result.ExtraType);
        }

        [TestMethod]
        public void TestStubFileWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006).bluray.disc");

            Assert.AreEqual("disc", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(true, result.IsStub);
            Assert.AreEqual("bluray", result.StubType);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestStubFile()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\Brave (2007)\\Brave (2006).bluray.disc");

            Assert.AreEqual("disc", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(true, result.IsStub);
            Assert.AreEqual("bluray", result.StubType);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("Brave", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestExtraStubWithNumericNameNotSupported()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006)-trailer.bluray.disc");

            Assert.AreEqual("disc", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(true, result.IsStub);
            Assert.AreEqual("bluray", result.StubType);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestExtraStubNotSupported()
        {
            // Using a stub for an extra is currently not supported
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\brave (2007)\\brave (2006)-trailer.bluray.disc");

            Assert.AreEqual("disc", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(true, result.IsStub);
            Assert.AreEqual("bluray", result.StubType);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("brave", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void Test3DFileWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006).3d.sbs.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(true, result.Is3D);
            Assert.AreEqual("sbs", result.Format3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestBad3DFileWithNumericName()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\300 (2007)\\300 (2006).3d1.sbas.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("300", result.Name);
            Assert.IsNull(result.ExtraType);
            Assert.IsNull(result.Format3D);
        }

        [TestMethod]
        public void Test3DFile()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\brave (2007)\\brave (2006).3d.sbs.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2006, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(true, result.Is3D);
            Assert.AreEqual("sbs", result.Format3D);
            Assert.AreEqual("brave", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestNameWithoutDate()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\American Psycho\\American.Psycho.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(null, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual(null, result.Format3D);
            Assert.AreEqual("American.Psycho", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestCleanDateAndStringsSequence()
        {
            var parser = GetParser();

            // In this test case, running CleanDateTime first produces no date, so it will attempt to run CleanString first and then CleanDateTime again
            var result =
                parser.ResolveFile(@"\\server\\Movies\\3.Days.to.Kill\\3.Days.to.Kill.2014.720p.BluRay.x264.YIFY.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.AreEqual(2014, result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual(null, result.Format3D);
            Assert.AreEqual("3.Days.to.Kill", result.Name);
            Assert.IsNull(result.ExtraType);
        }

        [TestMethod]
        public void TestFolderNameWithExtension()
        {
            var parser = GetParser();

            var result =
                parser.ResolveFile(@"\\server\\Movies\\7 Psychos.mkv\\7 Psychos.mkv");

            Assert.AreEqual("mkv", result.Container);
            Assert.IsNull(result.Year);
            Assert.AreEqual(false, result.IsStub);
            Assert.AreEqual(false, result.Is3D);
            Assert.AreEqual("7 Psychos", result.Name);
            Assert.IsNull(result.ExtraType);
        }
    }
}
