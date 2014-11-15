using MediaBrowser.Naming.Videos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Videos
{
    /// <summary>
    /// Summary description for ExtraTests
    /// </summary>
    [TestClass]
    public class ExtraTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private VideoParser GetParser()
        {
            var options = new VideoOptions();

            return new VideoParser(options);
        }

        // Requirements
        // movie-trailer = ExtraType trailer
        // trailer.ext = ExtraType trailer
        // movie-behindthescenes = ExtraType behindthescenes
        // movie-interview = ExtraType interview
        // movie-deleted = ExtraType deletedscene
        // movie-clip = ExtraType clip
        // movie-scene = ExtraType scene
        // movie-sample = ExtraType sample (there are other rules that would match sample as well)
        // theme.mp3 (or other audio ext) = ExtraType themesong

        // All of the above rules should be configurable through the options objects (ideally, even the ExtraTypes)
    }
}
