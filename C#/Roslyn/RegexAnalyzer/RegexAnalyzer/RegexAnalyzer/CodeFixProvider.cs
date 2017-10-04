using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace RegexAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(CheekyRegexAnalyzerCodeFixProvider)), Shared]
    public class CheekyRegexAnalyzerCodeFixProvider : CodeFixProvider
    {
        private const string Title = "Fix Regex";

        public sealed override ImmutableArray<string> FixableDiagnosticIds =>
            ImmutableArray.Create(RegexAnalyzerAnalyzer.DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.
            var invocationExpr = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf()
                .OfType<InvocationExpressionSyntax>().First();

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(Title, c => FixRegexCheekilyAsync(
                    context.Document, invocationExpr, c), equivalenceKey: Title), diagnostic);
        }

        private async Task<Document> FixRegexCheekilyAsync(Document document, InvocationExpressionSyntax invocationExpr,
            CancellationToken cancellationToken)
        {
            // find the node to replace
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);

            var memberAccessExpr = invocationExpr.Expression as MemberAccessExpressionSyntax;
            var memberSymbol = semanticModel.GetSymbolInfo(memberAccessExpr).Symbol as IMethodSymbol;

            var argumentList = invocationExpr.ArgumentList;
            var regexLiteral = argumentList.Arguments[1].Expression as LiteralExpressionSyntax;

            var regexOpt = semanticModel.GetConstantValue(regexLiteral);
            var regex = regexOpt.Value as string;

            // generate the replacement node
            var newLiteral = SyntaxFactory
                .ParseExpression("\"Maybe you shouldn't be using Regex to solve this particular problem?\"")
                .WithLeadingTrivia(regexLiteral.GetLeadingTrivia())
                .WithTrailingTrivia(regexLiteral.GetTrailingTrivia())
                .WithAdditionalAnnotations(Formatter.Annotation);

            // swap the new node into the syntax tree
            var root = await document.GetSyntaxRootAsync();
            var newRoot = root.ReplaceNode(regexLiteral, newLiteral);
            return document.WithSyntaxRoot(newRoot);
        }
    }
}