using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionTree
{
    public class RewriteExpression : ExpressionVisitor
    {
        public RewriteExpression() { }

        /// <summary>
        /// Setting the value for the constructor
        /// </summary>
        /// <param name="stackOperator"></param>
        public RewriteExpression(ExpressionType stackOperator)
        {
            this.Operator = stackOperator;
        }
        
        public ExpressionType Operator { get; set; }

        /// <summary>
        /// Checking if the Expression type is binary or constant 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override Expression Visit(Expression node)
        {
            //Console.WriteLine(node.NodeType); 
            // apply a switch case to the node of the expression 
            switch (node.NodeType)
            {
                // if the expression is +,-,* or / we want to visit the binary expression 
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                    return this.VisitBinary((BinaryExpression)node);
                // if the expression is constant visit the constant expression 
                case ExpressionType.Constant:
                    return this.VisitConstant((ConstantExpression)node);
                default:
                    return base.Visit(node);
            }
        }

        /// <summary>
        /// Rewrite the expression as per the Operator
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            // apply a switch case to the node type of the binary expression 
            switch (this.Operator)
            {
                case ExpressionType.Multiply:
                    return Expression.MakeBinary(this.Operator, this.Visit(node.Left), this.Visit(node.Right));
                case ExpressionType.Subtract:
                    return Expression.MakeBinary(this.Operator, this.Visit(node.Left), this.Visit(node.Right));
                case ExpressionType.Add:
                    return Expression.MakeBinary(this.Operator, this.Visit(node.Left), this.Visit(node.Right));
                case ExpressionType.Divide:
                    return Expression.MakeBinary(this.Operator, this.Visit(node.Left), this.Visit(node.Right));
            }

            // return all other types of expressions normally as we do not want to change any other type of expression in our expression tree 
            return base.VisitBinary(node);
        }

        /// <summary>
        /// Return the base node if it is constant
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            return base.VisitConstant(node);
        }
    }
}
