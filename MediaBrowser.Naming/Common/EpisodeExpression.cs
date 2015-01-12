
namespace MediaBrowser.Naming.Common
{
    public class EpisodeExpression
    {
        public string Expression { get; set; }
        public bool IsByDate { get; set; }
        public bool IsOptimistic { get; set; }
        public bool IsNamed { get; set; }

        public string[] DateTimeFormats { get; set; }

        public EpisodeExpression(string expression, bool byDate)
        {
            Expression = expression;
            IsByDate = byDate;
            DateTimeFormats = new string[] { };
        }

        public EpisodeExpression(string expression)
            : this(expression, false)
        {
        }

        public EpisodeExpression()
            : this(null)
        {
        }
    }
}
