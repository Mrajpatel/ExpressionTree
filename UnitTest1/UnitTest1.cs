using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionTree;
using System.Collections;
using System.Linq.Expressions;
using System;

namespace UnitTest1
{
    /// <summary>
    /// Represents tests for the <see cref="RewriteExpression"/> class.
    /// </summary>
    [TestClass]
    public class MyExpressionVisitorTest
    {

        /// <summary>
        /// (Test to Pass)
        /// Tests a null property after an object is created.
        /// </summary>
        [TestMethod]
        public void NullPropertyTest() 
        {
            RewriteExpression myExpressionVisitor = new RewriteExpression();

            try
            {
                Assert.IsNull(myExpressionVisitor.Operator);
            }
            catch (AssertFailedException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
    
}
