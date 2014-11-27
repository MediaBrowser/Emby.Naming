
namespace MediaBrowser.Naming.TV
{
    public class EpisodePathParserResult
    {
        public int? SeasonNumber { get; set; }
        public int? EpsiodeNumber { get; set; }
        public int? EndingEpsiodeNumber { get; set; }
        public string SeriesName { get; set; }
        public bool Success { get; set; }
    }
}
