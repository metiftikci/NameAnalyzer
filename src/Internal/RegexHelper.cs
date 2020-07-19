using System.Text.RegularExpressions;

namespace NameAnalyzer.Internal
{
    internal static class RegexHelper
    {
        public static bool IsPatternValid(string pattern)
        {
            try
            {
                new Regex(pattern);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
