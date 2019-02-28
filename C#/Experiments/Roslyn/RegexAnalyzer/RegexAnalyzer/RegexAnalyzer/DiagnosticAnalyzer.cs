using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RegexAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RegexAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "Regex";
        internal const string Title = "Regex error parsing string argument";
        internal const string MessageFormat = "Regex error {0}";
        internal const string Description = "Regex patterns should be syntactically valid.";
        internal const string Category = "Syntax";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat,
            Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            // get target node
            var invocationExpr = (InvocationExpressionSyntax) context.Node;

            // find match identifier in syntax tree
            var memberAccessExpr =
                invocationExpr.Expression as MemberAccessExpressionSyntax;

            // ensure 'Match' method is getting called
            if (memberAccessExpr?.Name.ToString() != "Match") return;

            // get symbol info for expression to ensure call is correct
            var memberSymbol =
                context.SemanticModel.GetSymbolInfo(memberAccessExpr).Symbol as IMethodSymbol;

            // compare to fully qualified name, bail if otherwise
            if (!memberSymbol?.ToString().StartsWith("System.Text.RegularExpressions.Regex.Match") ?? true) return;

            // ensure argument list has at least two args
            var argumentList = invocationExpr.ArgumentList;
            if ((argumentList?.Arguments.Count ?? 0) < 2) return;

            // ensure second arg is string literal
            var regexLiteral =
                argumentList.Arguments[1].Expression as LiteralExpressionSyntax;
            if (regexLiteral == null) return;

            // get constant compile-time value
            var regexOpt = context.SemanticModel.GetConstantValue(regexLiteral);
            if (!regexOpt.HasValue) return;
            var regex = regexOpt.Value as string;
            if (regex == null) return;

            // validate
            try
            {
                System.Text.RegularExpressions.Regex.Match("", regex);
            }
            catch (ArgumentException e)
            {
                // report a diagnostic
                var diagnostic =
                    Diagnostic.Create(Rule,
                        regexLiteral.GetLocation(), e.Message);

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}