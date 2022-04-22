using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Convert;
using static System.Math;

namespace NumericalAnalysis
{
    internal class SecantMethod
    {
        static double Function(double x)
            => Pow(x, 2) - Cos(PI * x);
        static double Derivative(double x)
            => 2 * x + PI * Sin(PI * x);
        public static void Solution()
        {
            List<double> fx;
            double a, b, h, k, fractional;
            string[] kParts;
            a = Input.Double("\nНачало отрезка: ");
            b = Input.Double("\nКонец отрезка: ");
            h = Input.Double("\nШаг функции: ");
            k = Input.Double("\nТочность: ");
            kParts = k.ToString().Split(',');
            if (kParts.Length != 1)
                fractional = -kParts[1].Length;
            else
                fractional = kParts[0].Length;
            WriteLine(fractional);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SecantMethod.Solution();
            Read();
        }
    }
    internal class Input
    {
        public static double Double(string text)
        {
            double input;
            try
            {
                Write(text);
                input = ToDouble(ReadLine());
            }
            catch (Exception)
            {
                WriteLine("Введенная строка имела неверный формат.");
                return Double(text);
            }
            return input;
        }
        public static string String(string text)
        {
            string input;
            Write(text);
            input = ReadLine();
            return input;
        }
    }
}
