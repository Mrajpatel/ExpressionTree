using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionTree;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace UnitTest2
{

    /// <summary>
    /// Represents tests for the <see cref="Program"/> class.
    /// </summary>
    [TestClass]
    public class ProgramClassTest
    {
        /// <summary>
        /// (Test to Pass)
        /// Test for checking the correct value return from 'AddStack()' method
        /// </summary>
        [TestMethod]
        public void AddStackTest()
        {
            Stack listContent = new Stack();
            listContent.Push((double)2);
            listContent.Push((double)16);

            Expression<Func<double, double, double>> expression = (x, y) => x * y;

            listContent = ExpressionTree.Program.AddStack(listContent, expression);
            double actualResult = 0;

            foreach (var i in listContent)
                actualResult = (double)i;

            // Comparing the expected value and returned value
            Assert.AreEqual((double)32, actualResult);
            
        }
    }
}
