using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class CleanStringTests : BaseVideoTest
    {
        [TestMethod]
        public void TestCleanString()
        {
            Test("Super movie 480p.mp4", "Super movie");
            Test("Super movie 480p 2001.mp4", "Super movie");
            Test("Super movie [480p].mp4", "Super movie");
            Test("480 Super movie [tmdbid=12345].mp4", "480 Super movie");
        }

        [TestMethod]
        public void TestStringWithoutDate()
        {
            Test(@"American.Psycho.mkv", "American.Psycho.mkv");
            Test(@"American Psycho.mkv", "American Psycho.mkv");
        }

        [TestMethod]
        public void TestNameWithBrackets()
        {
            Test(@"[rec].mkv", "[rec].mkv");
        }

        private void Test(string input, string expectedName)
        {
            var result = GetParser().CleanString(input);

            Assert.AreEqual(expectedName, result.Name, true, CultureInfo.InvariantCulture);
        }
    }
}
