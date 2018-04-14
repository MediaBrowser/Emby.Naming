using MediaBrowser.Model.Logging;
using Emby.Naming.Common;
using Emby.Naming.TV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Emby.Naming.Tests.TV
{
    [TestClass]
    public class AbsoluteEpisodeNumberTests
    {
        [TestMethod]
        public void TestAbsoluteEpisodeNumber1()
        {
            Assert.AreEqual(12, GetEpisodeNumberFromFile(@"The Simpsons\12.avi"));
        }

        [TestMethod]
        public void TestAbsoluteEpisodeNumber2()
        {
            Assert.AreEqual(12, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 12.avi"));
        }

        [TestMethod]
        public void TestAbsoluteEpisodeNumber3()
        {
            Assert.AreEqual(82, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 82.avi"));
        }

        [TestMethod]
        public void TestAbsoluteEpisodeNumber4()
        {
            Assert.AreEqual(112, GetEpisodeNumberFromFile(@"The Simpsons\The Simpsons 112.avi"));
        }

        private int? GetEpisodeNumberFromFile(string path)
        {
            var options = new NamingOptions();

            options.EpisodeExpressions = options.EpisodeExpressions
                .Where(i => i.SupportsAbsoluteEpisodeNumbers)
                .ToArray();

            var result = new EpisodeResolver(options)
                .Resolve(path, false);

            return result.EpisodeNumber;
        }
    }
}
