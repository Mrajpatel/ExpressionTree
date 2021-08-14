/**
 * Name: Raj Pankajkumar Patel
 * Statement of Authoroship: I, Raj Patel, certify that this material is my original work. No other person's work has been used without due acknowledgement.
 * Date: 6th February 2019
 * 
 * */

using System;
using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ExpressionTree
{
    public class Program
    {
        /// <summary>
        /// Ask user to enter reverse polish expression and evaluate it using expression tree
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // initializing the variables
            bool varified = false;
            string varifiedString = "";

            // asking the user to enter the string
            while (!varified)
            {
                Console.WriteLine("Enter Reverse Polish Notation expression: ");
                string enterString = Console.ReadLine();
                
                // triming the string to remove extra spacing from the ends and validating
                varified = ValidateString(enterString.Trim());

                // Assigning the validated string
                if (varified) varifiedString = enterString.Trim();
            }

            // if the entered string is valid
            if (varified)
            {
                // taking the varified string and putting each character in the stack
                string notation = varifiedString;
                string[] notarionSplit = notation.Split(" ");

                Stack listConstant = new Stack();

                // Declaring the expression tree
                Expression<Func<double, double, double>> expression = (x, y) => x + y;

                foreach (var i in notarionSplit)
                {
                    // converting string into double and pushing it into the stack
                    if (Double.TryParse(i, out double x))
                    {
                        listConstant.Push(x);
                    }
                    else
                    {
                        // Using RewriteExpression class that implements ExpressionVisitor class to rewrite the expression tree and Compile the new expression 
                        // and add it back to the stack
                        switch (i)
                        {
                            case "*":
                                // sending the expression to the RewriteExpression class to rewrite it as per the operator
                                var expressionMultiply = (Expression<Func<double, double, double>>)(new RewriteExpression(ExpressionType.Multiply)).Visit(expression);
                                Func<double, double, double> result = expressionMultiply.Compile();
                                listConstant = AddStack(listConstant, expressionMultiply);
                                break;
                            case "-":
                                var expressionSub = (Expression<Func<double, double, double>>)(new RewriteExpression(ExpressionType.Subtract)).Visit(expression);
                                listConstant = AddStack(listConstant, expressionSub);
                                break;
                            case "+":
                                var expressionAdd = (Expression<Func<double, double, double>>)(new RewriteExpression(ExpressionType.Add)).Visit(expression);
                                listConstant = AddStack(listConstant, expressionAdd);
                                break;
                            case "/":
                                var expressionDivide = (Expression<Func<double, double, double>>)(new RewriteExpression(ExpressionType.Divide)).Visit(expression);
                                listConstant = AddStack(listConstant, expressionDivide);
                                break;
                        }
                    }
                }

                // Printing the final value remaining in the stack
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Value of the entered Reverse Polish Notation: ");
                foreach (var value in listConstant)
                {
                    Console.WriteLine(value);
                }
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Takes first two numbers from stack and validate the expression tree
        /// </summary>
        /// <param name="listConstant"></param>
        /// <param name="otherExpression"></param>
        /// <returns></returns>
        public static Stack AddStack(Stack listConstant, Expression<Func<double, double, double>> otherExpression)
        {
            // compiling the expression
            Func<double, double, double> result = otherExpression.Compile();

            double rhs = (Double)listConstant.Pop();
            double lhs = (Double)listConstant.Pop();
            
            // adding the value back to the stack
            listConstant.Push((result(lhs, rhs)));

            return listConstant;
        }

        /// <summary>
        /// Validate the string and return the boolean variable
        /// </summary>
        /// <param name="enterString"></param>
        /// <returns></returns>
        public static Boolean ValidateString(string enterString)
        {
            bool varified = false;

            //checking if string has any invalid character
            int errorCounter = Regex.Matches(enterString, @"[a-zA-Z]").Count;

            try
            {
                if (errorCounter == 0 && enterString.Trim() != "")
                {
                    String[] splitValues = enterString.Split(' ');

                    //check if first two digits are numbers in string
                    var isNumeric_0 = int.TryParse(splitValues[0], out int n0);
                    var isNumeric_1 = int.TryParse(splitValues[1], out int n1);

                    if (isNumeric_0 && isNumeric_1)
                    {
                        //checking if the last character in the string is operator
                        var isOperator = Regex.Matches(splitValues[splitValues.Length - 1], @"^(\+|-|\*|/)$").Count;

                        //counting numbers and operators in string 
                        if (isOperator != 0)
                        {
                            int countNumbers = 0;
                            int countOperators = 0;

                            foreach (var a in splitValues)
                            {
                                if (int.TryParse(a, out int b))
                                    countNumbers++;
                                else
                                    countOperators++;
                            }

                            // checking if the operators are one less than operands
                            if ((countNumbers - 1) == countOperators)
                            {
                                varified = true;
                                Console.WriteLine("---Enter String is VALID for Reverse Polish Notation---");                                
                            }
                            else
                            {
                                Console.WriteLine("---Enter String is INVALID for Reverse Polish Notation---");
                            }
                        }
                        else
                        {
                            Console.WriteLine("---Enter String is INVALID for Reverse Polish Notation---");
                        }
                    }
                    else
                    {
                        Console.WriteLine("---Enter String is INVALID for Reverse Polish Notation---");
                    }

                }
                else
                {
                    Console.WriteLine("---Enter String is INVALID for Reverse Polish Notation---");
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("---Enter String is INVALID for Reverse Polish Notation---");
            }

            return varified;
        }
    }
}

