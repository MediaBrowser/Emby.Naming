using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    [TestClass]
    public class CleanDateTimeTests : BaseVideoTest
    {
        [TestMethod]
        public void TestCleanDateTime()
        {
            Test(@"The Wolf of Wall Street (2013).mkv", "The Wolf of Wall Street", 2013);
            Test(@"The Wolf of Wall Street 2 (2013).mkv", "The Wolf of Wall Street 2", 2013);
            Test(@"The Wolf of Wall Street - 2 (2013).mkv", "The Wolf of Wall Street - 2", 2013);
            Test(@"The Wolf of Wall Street 2001 (2013).mkv", "The Wolf of Wall Street 2001", 2013);

            Test(@"300 (2006).mkv", "300", 2006);
            Test(@"d:\\movies\\300 (2006).mkv", "300", 2006);
            Test(@"300 2 (2006).mkv", "300 2", 2006);
            Test(@"300 - 2 (2006).mkv", "300 - 2", 2006);
            Test(@"300 2001 (2006).mkv", "300 2001", 2006);

            Test(@"curse.of.chucky.2013.stv.unrated.multi.1080p.bluray.x264-rough", "curse.of.chucky", 2013);

            Test(@"\\server\\Movies\\300 (2007)\\300 (2006).bluray.disc", "300", 2006);
        }

        [TestMethod]
        public void TestCleanDateTimeWithoutFileExtension()
        {
            Test(@"The Wolf of Wall Street (2013)", "The Wolf of Wall Street", 2013);
            Test(@"The Wolf of Wall Street 2 (2013)", "The Wolf of Wall Street 2", 2013);
            Test(@"The Wolf of Wall Street - 2 (2013)", "The Wolf of Wall Street - 2", 2013);
            Test(@"The Wolf of Wall Street 2001 (2013)", "The Wolf of Wall Street 2001", 2013);

            Test(@"300 (2006)", "300", 2006);
            Test(@"d:\\movies\\300 (2006)", "300", 2006);
            Test(@"300 2 (2006)", "300 2", 2006);
            Test(@"300 - 2 (2006)", "300 - 2", 2006);
            Test(@"300 2001 (2006)", "300 2001", 2006);

            Test(@"\\server\\Movies\\300 (2007)\\300 (2006)", "300", 2006);
        }
        
        private void Test(string input, string expectedName, int? expectedYear)
        {
            input = Path.GetFileName(input);

            var result = GetParser().CleanDateTime(input);

            Assert.AreEqual(expectedName, result.Name, true, CultureInfo.InvariantCulture);
            Assert.AreEqual(expectedYear, result.Year);
        }
    }
}
