using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using NameAnalyzer.Internal;

namespace NameAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "NA001";
        private const string Title = "The type names format is not allowed.";
        private const string MessageFormat = "The type names format '{0}' is not allowed, must match {1}.";
        private const string Category = "Naming";

        private readonly static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            ImmutableArray<NamingRule> rules = context.Options.GetNamingRules();

            if (rules.Length == 0) return;

            ITypeSymbol symbol = (ITypeSymbol)context.Symbol;

            List<NamingRule> matchingRules = new List<NamingRule>();

            foreach (NamingRule rule in rules)
            {
                if (Regex.IsMatch(symbol.ContainingNamespace.ToDisplayString(), rule.LocationFormat))
                {
                    if (Regex.IsMatch(symbol.Name, rule.NameFormat))
                    {
                        return;
                    }

                    matchingRules.Add(rule);
                }
            }

            if (matchingRules.Count == 0) return;

            string msg = matchingRules.Count == 1 ? string.Empty : "at least one of ";
            msg += string.Join(", ", matchingRules.Select(x => $"`{x.NameFormat}`"));

            context.ReportDiagnostic(Diagnostic.Create(Rule, symbol.Locations[0], symbol.Name, msg));
        }
    }
}
