using System.Text.RegularExpressions;

namespace Polokus.Tests.Helpers
{
    public static class CustomAsserts
    {
        public static void MatchRegex(string str, string regex)
        {
            if (!Regex.IsMatch(str, regex))
                throw new Exception(str);
        }

        public static void MatchAnyRegex(string str, params string[] regexes)
        {
            if (!regexes.Any(r => Regex.IsMatch(str, r)))
                throw new Exception(str);
        }

        public static void GoodOrder(string str, params string[] orderants)
        {
            if (orderants.Length < 2)
            {
                return;
            }

            for (int i = 1; i < orderants.Length; i++)
            {
                int idx1 = str.IndexOf(orderants[i - 1]);
                int idx2 = str.IndexOf(orderants[i]);

                Assert.IsFalse(idx1 == -1);
                Assert.IsFalse(idx2 == -1);
                Assert.IsTrue(idx1 < idx2);
            }
        }
    }
}
