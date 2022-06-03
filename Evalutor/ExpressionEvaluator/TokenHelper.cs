using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evalutor
{
    public static class TokenHelper
    {
        public static int FindToken(this string @this, string token, int startFromPosition = 0)
        {
            return @this.IndexOf(token, startFromPosition);
        }
    }
}
