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
    public class NoVar : SymbolicRewriter
    {
        public const string ID = "<no var>";

        public override string Name()
        {
            return ID;
        }

        public override SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            if (node.Type.ToString() != "var") return base.VisitVariableDeclaration(node);
            
            //var vt = node.Variables.First().Initializer;
            var s = _semaModel.GetSymbolInfo(node.Type);
            //SyntaxFactory.TypeDeclaration(SyntaxKind.ty)
            //return SyntaxFactory.VariableDeclaration(type: SyntaxFactory.t(s.Symbol.ToDisplayString()), node.Variables);

            return SyntaxFactory.VariableDeclaration(
        type: SyntaxFactory.IdentifierName(s.Symbol.ToDisplayString()),
        variables: node.Variables);
         //   Syntax.VariableDeclarator(
         //       identifier: Syntax.Identifier(name)))))
            //return base.VisitVariableDeclaration(node);
        }

        

    }
}
