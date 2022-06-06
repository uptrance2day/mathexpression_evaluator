using System.Text.RegularExpressions;

namespace Evalutor
{
    public class Calculator
    {
        private readonly string[] _prioritiedOperationPatterns;
        private readonly string _numberPattern;
        public Calculator()
        {
            _prioritiedOperationPatterns = new [] {
                    $"[{Token.Multiply}{Token.Divide}]",
                    $"[{Token.Add}{Token.Subtract}]"
            };
            _numberPattern = @"(-){0,1}\d+\.?\d*";
        }

        public decimal Eval(string expression)
        {
            while (true)
            {
                var subExpression = expression.FindMostDeepSubExpression(Token.OpenBracket, Token.CloseBracket);
                var value = EvalBasic(subExpression.Item1);
                if (subExpression.Item3 == expression.Length)
                {
                    return value;
                }
                expression = expression.Remove(subExpression.Item2, subExpression.Item3).Insert(subExpression.Item2, value.ToString()).Replace("--", "");
            }
        } 

        private decimal EvalBasic(string expression)
        {
            foreach(var operationsPattern in _prioritiedOperationPatterns)
            {
                while (true)
                {
                    var operationMatches = Regex.Matches(expression, operationsPattern);
                    if (operationMatches.Count == 0)
                    {
                        break;
                    }

                    var operationMatch = operationMatches.First();

                    var token = operationMatch.Value;
                    var index = operationMatch.Index;
                    int? nextOperationIndex = null;
                    if (operationMatches.Count > 1)
                    {
                        var nextOperationMatch = operationMatches.Where(t=> t != operationMatch).First();
                        nextOperationIndex = nextOperationMatch.Index;
                    }

                    var numberMatches = Regex.Matches(expression, _numberPattern);

                    if (numberMatches.Count == 0)
                    {
                        throw new Exception("Incorrect expression passed.");
                    }

                    var rightValue = numberMatches.FirstOrDefault(t => t.Index >= index && (nextOperationIndex == null || t.Index <= nextOperationIndex));
                    var leftValue = numberMatches.OrderByDescending(t => t.Index).FirstOrDefault(t => t.Index < index);

                    if (rightValue == null || leftValue == null)
                    {
                        return decimal.TryParse(expression, out var parsedValue) ? parsedValue : throw new Exception("Incorrect expression passed.");
                    }

                    var result = EvalOperation(decimal.Parse(leftValue.Value), rightValue.Index == index ? Math.Abs(decimal.Parse(rightValue.Value)) : decimal.Parse(rightValue.Value), token);
                    var evaluatedSubstring = expression.Substring(leftValue.Index, rightValue.Index - leftValue.Index + rightValue.Value.Length);
                    expression = expression.Replace(evaluatedSubstring, result.ToString());
                }
            }

            return decimal.TryParse(expression, out var value) ? value : throw new Exception("Incorrect expression passed.");
        }

        private decimal EvalOperation(decimal left, decimal right, string token)
        {
            switch (token)
            {
                case Token.Multiply:
                    {
                        return left * right;
                    }
                case Token.Divide:
                    {
                        return left / right;
                    }
                case Token.Add:
                    {
                        return left + right;
                    }
                case Token.Subtract:
                    {
                        return left - right;
                    }
                default: throw new NotImplementedException($"Unknown token passed ({token})");
            }
        }
    }
}
