using MediaBrowser.Naming.Common;
using MediaBrowser.Naming.Subtitles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using MediaBrowser.Model.Logging;

namespace MediaBrowser.Naming.Tests.Subtitles
{
    /// <summary>
    /// Summary description for SubtitleParserTests
    /// </summary>
    [TestClass]
    public class SubtitleParserTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private SubtitleParser GetParser()
        {
            var options = new NamingOptions();

            return new SubtitleParser(options, new NullLogger());
        }

        [TestMethod]
        public void TestSubtitles()
        {
            Test("The Skin I Live In (2011).srt", null, false, false);
            Test("The Skin I Live In (2011).eng.srt", "eng", false, false);
            Test("The Skin I Live In (2011).eng.default.srt", "eng", true, false);
            Test("The Skin I Live In (2011).eng.forced.srt", "eng", false, true);
            Test("The Skin I Live In (2011).eng.foreign.srt", "eng", false, true);
            Test("The Skin I Live In (2011).eng.default.foreign.srt", "eng", true, true);

            Test("The Skin I Live In (2011).default.foreign.eng.srt", "eng", true, true);
        }

        private void Test(string input, string language, bool isDefault, bool isForced)
        {
            var parser = GetParser();

            var result = parser.ParseFile(input);

            Assert.AreEqual(language, result.Language, true, CultureInfo.InvariantCulture);
            Assert.AreEqual(isDefault, result.IsDefault);
            Assert.AreEqual(isForced, result.IsForced);
        }
    }
}
