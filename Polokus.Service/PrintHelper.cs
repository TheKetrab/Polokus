using System.Drawing;

namespace Polokus.Service
{
    public static class PrintHelper
    {
        public static void Print(string str, ConsoleColor? color = null, string? lineEnd = "\r\n")
        {
            var prevClr = Console.ForegroundColor;
            if (color != null)
            {
                Console.ForegroundColor = color.Value;
            }

            Console.Write(str + lineEnd);
            Console.ForegroundColor = prevClr;
        }

        public static string GetLine(int len = 30)
        {
            return new string('-', len);
        }

        public static void PrintLine()
        {
            Console.WriteLine(" " + GetLine());
        }

        public static void PrintHeader()
        {
            Console.WriteLine();
            PrintLine();
            Print(new string(' ', 10) + "POLOKUS SERVICE", ConsoleColor.DarkBlue);
            PrintLine();
            Console.WriteLine();
        }

        public static void PrintInfo(string info)
        {
            Print("[Polokus] > ", ConsoleColor.Yellow, "");
            Print(info, ConsoleColor.Yellow);
        }
    }

}