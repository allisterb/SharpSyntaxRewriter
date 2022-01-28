using System;
using System.Diagnostics;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

using SharpSyntaxRewriter.Adapters;
using SharpSyntaxRewriter.Rewriters.Types;
using SharpSyntaxRewriter.Utilities;

namespace SharpSyntaxRewriter.Rewriters
{
    public class NoNameof : SymbolicRewriter
    {
        public const string ID = "<no nameof>";

        public override string Name()
        {
            return ID;
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (!node.ToFullString().StartsWith("nameof(", StringComparison.InvariantCulture)) return base.VisitInvocationExpression(node);
            var arg = node
                .DescendantNodes()
                .OfType<ArgumentSyntax>()
                .First().DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .First();
            return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(arg.ToFullString()));
        }
    }
}
