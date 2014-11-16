using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class CleanStringTests : BaseVideoTest
    {
        [TestMethod]
        public void CleanBasicTest()
        {
            Test("Super movie 480p.mp4", "Super movie");
            Test("Super movie 480p 2001.mp4", "Super movie");
            Test("Super movie [480p].mp4", "Super movie");
            Test("480 Super movie [tmdbid=12345].mp4", "480 Super movie");
        }

        private void Test(string input, string expectedName)
        {
            var result = GetParser().CleanString(input);

            Assert.AreEqual(expectedName, result.Name, true, CultureInfo.InvariantCulture);
        }
    }
}
