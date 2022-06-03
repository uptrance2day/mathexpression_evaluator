using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evalutor
{
    public static class ExpressionHelper
    {
        public static (string, int, int) FindMostDeepSubExpression(this string @this, string subExpressionStartToken, string subExpressionEndToken, int startFromPosition = 0)
        {
            var startPosition = @this.FindToken(subExpressionStartToken, startFromPosition);
            var endPosition = @this.FindToken(subExpressionEndToken, startFromPosition);

            if (startPosition == endPosition)
            {
                return (@this, 0, @this.Length); // no sub expression with passed start and end token found
            }

            if (startPosition == -1 || endPosition == -1)
            {
                throw new Exception("Incorrect expression passed.");
            }
            
            var sub = @this.Substring(startPosition + 1, endPosition - (startPosition + 1));
            var subTokenPosition = sub.FindToken(subExpressionStartToken);

            if (subTokenPosition > -1)
            {
                var initialPositionIndex = subTokenPosition + startFromPosition;
                return @this.FindMostDeepSubExpression(subExpressionStartToken, subExpressionEndToken, initialPositionIndex);
            }
            else
            {
                return (sub, startPosition, endPosition - startPosition + 1); // found expression the most deep
            }
        }
    }
}
