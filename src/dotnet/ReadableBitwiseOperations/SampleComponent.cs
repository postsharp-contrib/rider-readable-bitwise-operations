using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon.CodeFolding;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.SimplerBitwise
{
    [Language(typeof(CSharpLanguage))]
    public class BitwiseComparisonCodeFoldingFactory : ICodeFoldingProcessorFactory
    {
        public ICodeFoldingProcessor CreateProcessor()
        {
            return new BitwiseComparisonCodeFoldingProcessor();
        }
    }

    public class BitwiseComparisonCodeFoldingProcessor : TreeNodeVisitor<FoldingHighlightingConsumer>, ICodeFoldingProcessor
    {
        private const string FoldId = "ReSharper Preprocessor Regions Folding";

        public bool InteriorShouldBeProcessed(ITreeNode element, FoldingHighlightingConsumer context)
        {
            return true;
        }

        public bool IsProcessingFinished(FoldingHighlightingConsumer context)
        {
            return false;
        }

        public void ProcessBeforeInterior(ITreeNode element, FoldingHighlightingConsumer context)
        { 
            if (!(element is ICSharpTreeNode csharpTreeNode))
                return;
            csharpTreeNode.Accept(this, context);

        }

        public void ProcessAfterInterior(ITreeNode element, FoldingHighlightingConsumer context)
        {
        }

        public override void VisitBinaryExpression(IBinaryExpression binaryExpressionParam, FoldingHighlightingConsumer context)
        {
            if (binaryExpressionParam.OperatorSign.GetText() == "!=")
            {
                if (binaryExpressionParam.RightOperand.GetText() == "0")
                {
                    if (binaryExpressionParam.LeftOperand is IParenthesizedExpression parenthesizedExpression)
                    {
                        if (parenthesizedExpression.Expression is IBinaryExpression binaryAddition)
                        {
                            if (binaryAddition.OperatorSign.GetText() == "&")
                            {
                                DocumentRange documentRange = binaryExpressionParam.GetDocumentRange();
                                context.AddDefaultPriorityFolding(FoldId,
                                    documentRange,
                                    binaryAddition.RightOperand.GetText() + " ∈ " +
                                    binaryAddition.LeftOperand.GetText());
                                return;
                            }
                        }
                    }
                }
            }
            
            if (binaryExpressionParam.OperatorSign.GetText() == "==")
            {
                if (binaryExpressionParam.RightOperand.GetText() == "0")
                {
                    if (binaryExpressionParam.LeftOperand is IParenthesizedExpression parenthesizedExpression)
                    {
                        if (parenthesizedExpression.Expression is IBinaryExpression binaryAddition)
                        {
                            if (binaryAddition.OperatorSign.GetText() == "&")
                            {
                                
                                DocumentRange documentRange = binaryExpressionParam.GetDocumentRange();
                                context.AddDefaultPriorityFolding(FoldId,
                                    documentRange,
                                    binaryAddition.RightOperand.GetText() + " ∉ " +
                                    binaryAddition.LeftOperand.GetText());
                                return;
                            }
                        }
                    }
                }
            }

            base.VisitBinaryExpression(binaryExpressionParam, context);
        }
    }
}