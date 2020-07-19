using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NameAnalyzer.Internal
{
    internal static class SettingsHelper
    {
        public const string FileName = ".nameanalyzer";

        public static ImmutableArray<NamingRule> GetNamingRules(this AnalyzerOptions options)
        {
            foreach (AdditionalText additionalFile in options.AdditionalFiles)
            {
                if (Path.GetFileName(additionalFile.Path) == FileName)
                {
                    return ImmutableArray.Create(new SettingsFileReader(additionalFile.Path).Parse());
                }
            }

            return ImmutableArray<NamingRule>.Empty;
        }
    }
}
