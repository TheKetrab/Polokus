using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Polokus.Core.Helpers
{
    public static class TimeString
    {
        const int S = 1000;
        const int M = 60 * S;
        const int H = 60 * M;

        const string timeRegex = @"^\w*(\d+\w*h)?\w*(\d+\w*m)?\w*(\d+\w*s)?\w*(\d+\w*ms)?\w*$";

        public static bool IsTimeString(string str)
        {
            return Regex.IsMatch(str, timeRegex);
        }

        public static bool IsCroneString(string str)
        {
            return CronExpression.IsValidExpression(str);
        }

        public static int ParseToMiliseconds(string timeString)
        {
            Match match = Regex.Match(timeString, timeRegex);
            if (!match.Success)
            {
                throw new TimeStringException($"Given timeString ({timeString}) does not match to regex.");
            }

            int h = 0;
            int m = 0;
            int s = 0;
            int ms = 0;
            foreach (var group in match.Groups)
            {
                string? val = group?.ToString();
                if (val == null)
                {
                    continue;
                }

                if (val.Contains('h'))
                {
                    h = ConvertWithoutLetter(val, "h");
                }
                else if (val.Contains('m'))
                {
                    m = ConvertWithoutLetter(val, "m");
                }
                else if (val.Contains('s'))
                {
                    s = ConvertWithoutLetter(val, "s");
                }
                else if (val.Contains("ms"))
                {
                    ms = ConvertWithoutLetter(val, "ms");
                }
            }

            return H * h + M * m + S * s + ms;

        }

        private static int ConvertWithoutLetter(string str, string ch)
        {
            string digits = str.Replace(ch, "").Trim();
            if (int.TryParse(digits, out int val))
            {
                return val;
            }

            throw new TimeStringException($"Unable to parse: {digits}.");
        }

        public static string CodeToTimeString(int miliseconds)
        {
            List<string> str = new List<string>();

            int ms = miliseconds % S;
            if (ms > 0) str.Add($"{ms}ms");

            miliseconds = miliseconds / S;
            int s = miliseconds % M;
            if (s > 0) str.Add($"{s}s");

            miliseconds = miliseconds / M;
            int m = miliseconds % H;
            if (m > 0) str.Add($"{m}m");

            miliseconds = miliseconds / H;
            int h = miliseconds;
            if (h > 0) str.Add($"{h}h");

            str.Reverse();
            return string.Join(' ', str);
        }


    }

    public class TimeStringException : Exception
    {
        public TimeStringException() { }
        public TimeStringException(string message) : base(message) { }

    }
}
