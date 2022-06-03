using Evalutor;

namespace Evaluator.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;
        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Theory]
        [InlineData("2 + 4", 6)]
        [InlineData("2.5 + 4.7", 7.2)]
        [InlineData("2.5 - 4.7", -2.2)]
        [InlineData("2 * 4 - 5", 3)]
        [InlineData("2 * 4 / 2 - 5", -1)]
        [InlineData("-5", -5)]
        [InlineData("0-5", -5)]
        [InlineData("0 - 5", -5)]
        [InlineData("0 - -5", 5)]
        [InlineData("2.5 - -4.7", 7.2)]
        [InlineData("2.5 * 2 / 2.5", 2)]
        // original task tests
        [InlineData("1-1", 0)]
        [InlineData("1 -1", 0)]
        [InlineData("1- 1", 0)]
        [InlineData("1 - 1", 0)]
        [InlineData("1- -1", 2)]
        [InlineData("1 - -1", 2)]
        [InlineData("1--1", 2)]
        [InlineData("6 + -(4)", 2)]
        [InlineData("6 + -( -4)", 10)]
        [InlineData("1 - - 1", null, true)]
        [InlineData("1- - 1", null, true)]
        [InlineData("6 + - (4)", null, true)]
        [InlineData("6 + -(- 4)", null, true)]
        [InlineData("(2 / (2 + 3.33) * 4) - -6", 7.50094)]
        public void Eval_Correct_Results(string expression, decimal value, bool checkForException = false)
        {
            if (checkForException)
            {
                Assert.Throws<Exception>(() => _calculator.Eval(expression));
            }
            else
            {
                Assert.Equal(value, Math.Round(_calculator.Eval(expression), 5));
            }
        }
    }
}