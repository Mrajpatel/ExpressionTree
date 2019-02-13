using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionTree;
using System;
using System.Linq.Expressions;
using System.Collections;

namespace UnitTest4
{
    /// <summary>
    /// Represents tests for the <see cref="RewriteExpression"/> class.
    /// </summary>
    [TestClass]
    public class RewriteExpressionTest
    {
        /// <summary>
        /// (Test to Pass)
        /// Tests expected lambda expression body conversion from Subtract to Divide
        /// </summary>
        [TestMethod]
        public void VisitTest()
        {
            Expression<Func<double, double, double>> expression = (x, y) => x - y;
            
            var newExpression = (Expression<Func<double, double, double>>)(new RewriteExpression(ExpressionType.Divide)).Visit(expression);
            string newExpressionBody = ((LambdaExpression)newExpression).Body.ToString();

            string expectedExpressionBody = "(x / y)";
                
            // checking if the expected and actual expression are equal
            Assert.AreEqual(expectedExpressionBody, newExpressionBody);
        }
    }
}
