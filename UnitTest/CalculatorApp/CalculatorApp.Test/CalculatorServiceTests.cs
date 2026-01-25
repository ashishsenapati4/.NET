using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Test
{
    public class CalculatorServiceTests
    {
        private readonly CalculatorService _calculator;
        public CalculatorServiceTests()
        {
            _calculator = new CalculatorService();
        }

        [Fact]
        public void Add_WhenCalledWith2and3_Returns5()
        {
            var result = _calculator.Add(2, 3);
            Assert.Equal(5, result);
        }

        [Fact]
        public void Subtract_WhenCalledwith7and4_Retuns3()
        {
            var result = _calculator.Subtract(7, 4);
            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData(2,3,6)]
        [InlineData(9,6,54)]
        [InlineData(-1,-7,7)]
        [InlineData(3,-88,-264)]
        public void Multiply_WhenCalled_ReturnsExpectedResult(int a, int b, int expected)
        {
            var result = _calculator.Multiply(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_WhenCalledWith78and2_Returns39()
        {
            var result = _calculator.Divide(78, 2);
            Assert.Equal(39, result);
        }

        [Theory]
        [InlineData(8,2,4)]
        [InlineData(90,45,2)]
        [InlineData(999,-3,-333)]
        public void Divide_WhenCalled_ReturnsExpectedResult(int a, int b, int expected)
        {
            var result = _calculator.Divide(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_WhenDividesByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(13 , 0));
        }

    }
}
