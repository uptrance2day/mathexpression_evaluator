using Evalutor;

namespace Evaluator.Tests
{
    public class TokenHelperTests
    {
        [Fact]
        public void FindToken_Error_On_Null()
        {
            string? expression = null;
            Assert.Throws<NullReferenceException>(() => expression.FindToken("("));
            Assert.Throws<ArgumentNullException>(() => string.Empty.FindToken(null));
        }

        [Fact]
        public void FindToken_NoError_On_Empty()
        {
            Assert.Equal(-1, string.Empty.FindToken("("));
            Assert.Equal(0, "empty".FindToken(string.Empty));
        }

        [Theory]
        [InlineData("(2 / (2 + 3.33) * 4) - -6", "/", 3)]
        [InlineData("(2 / (2 + 3.33) * 4) - -6", "(", 0)]
        [InlineData("(2 / (2 + 3.33) * 4) - -6", ")", 14)]
        [InlineData("(2 / (2 + 3.33) * 4) - -6", ")", 19, 15)]
        public void FintToken_Correct_Results(string expression, string token, int position, int startFrom = 0)
        {
            Assert.Equal(position, expression.FindToken(token, startFrom));
        }
    }
}