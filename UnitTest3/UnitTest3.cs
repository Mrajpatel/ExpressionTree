using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionTree;

namespace UnitTest3
{
    /// <summary>
    /// Represents tests for the <see cref="Program"/> class.
    /// </summary>
    [TestClass]
    public class ProgramClassTest
    {
        /// <summary>
        /// (Test to Pass)
        /// Test for checking the correct value return from 'ValidateString()' method
        /// </summary>
        [TestMethod]
        public void ValidateStringTest()
        {
            // test string for ValidateString method
            string testString = "15 7 1 1 + - / 3 * 2 1 1 + + -";

            bool varifiedString = Program.ValidateString(testString);
            
            // Checking if the actual value and expected value are equal
            Assert.AreEqual(true, varifiedString);
        }
    }
}
