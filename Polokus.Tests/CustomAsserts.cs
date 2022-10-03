using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Polokus.Tests
{
    public static class CustomAsserts
    {
        public static void MatchRegex(string str, string regex)
        {
            Assert.IsTrue(Regex.IsMatch(str, regex));
        }

        public static void GoodOrder(string str, params string[] orderants)
        {
            if (orderants.Length < 2)
            {
                return;
            }

            for (int i=1; i<orderants.Length; i++)
            {
                int idx1 = str.IndexOf(orderants[i-1]);
                int idx2 = str.IndexOf(orderants[i]);

                Assert.IsFalse(idx1 == -1);
                Assert.IsFalse(idx2 == -1);
                Assert.IsTrue(idx1 < idx2);
            }
        }
    }
}
