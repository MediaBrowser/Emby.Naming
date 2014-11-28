using System.Text.RegularExpressions;

namespace MediaBrowser.Naming.Common
{
    public interface IRegexProvider
    {
        /// <summary>
        /// Gets the regex.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="options">The options.</param>
        /// <returns>Regex.</returns>
        Regex GetRegex(string expression, RegexOptions options);
    }
}
