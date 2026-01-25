using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public class CalculatorService
    {
        // Adds two integers and returns the sum
        public int Add(int a, int b)
        {
            return a + b;
        }
        // Subtract b from a
        public int Subtract(int a, int b)
        {
            //Introducing some error by adding 5 to the result
            return a - b + 5;
        }
        // Multiply two numbers
        public int Multiply(int a, int b)
        {
            return a * b;
        }
        // Divide a by b; throws DivideByZeroException if b is zero
        public int Divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");
            return a / b;
        }
    }
}
