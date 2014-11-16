using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaBrowser.Naming.Tests.Video
{
    /// <summary>
    /// Summary description for ExtraTests
    /// </summary>
    [TestClass]
    public class ExtraTests : BaseVideoTest
    {
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
