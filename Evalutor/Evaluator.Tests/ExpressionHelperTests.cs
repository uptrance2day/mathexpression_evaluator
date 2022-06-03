using Evalutor;

namespace Evaluator.Tests
{
    public class ExpressionHelperTests
    {
        [Fact]
        public void FindSubExpression_Error_On_Null()
        {
            string? expression = null;
            Assert.Throws<NullReferenceException>(() => expression.FindMostDeepSubExpression("(", ")"));
            Assert.Throws<ArgumentNullException>(() => string.Empty.FindMostDeepSubExpression(null, null));
        }

        [Fact]
        public void FindSubExpression_NoError_On_Empty()
        {
            Assert.Equal(string.Empty, string.Empty.FindMostDeepSubExpression(string.Empty, string.Empty));
            Assert.Equal(string.Empty, "empty".FindMostDeepSubExpression("(", ")"));
        }

        [Fact]
        public void FindSubExpression_Error_On_Incorrect()
        {
            Assert.Throws<Exception>(() => "(".FindMostDeepSubExpression("(", ")"));
        }

        [Theory]
        [InlineData("(2 / (2 + 3.33) * 4) - -6", "(",")", "2 + 3.33")]
        [InlineData("(2 / 2 * 4) - -6", "(", ")", "2 / 2 * 4")]
        [InlineData("(2 / 2 * 4 - -6)", "(", ")", "2 / 2 * 4 - -6")]
        [InlineData("(2 / (2 + (2 / 4)) * 4) - -6", "(", ")", "2 / 4")]
        [InlineData("(2 / (2 + (2 / 4))", "(", ")", "2 / 4")]
        [InlineData("(2 / (2 + (2-4))", "(", ")", "2-4")]
        public void FindSubExpression_Correct_Results(string expression, string startToken, string endToken, string subExpression)
        {
            Assert.Equal(subExpression, expression.FindMostDeepSubExpression(startToken, endToken));
        }
    }
}