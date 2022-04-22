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
            h = Input.DoubleNotZero("\nШаг функции: ");
            k = Input.DoubleNotZero("\nТочность: ");
            if (k.ToString().Contains('-'))
            {
                kParts = k.ToString().Split('-');
                fractional = -ToDouble(kParts[1]);
            }
            else
                fractional = k.ToString().Length;

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
                return input;
            }
            catch (Exception)
            {
                WriteLine("Введенная строка имела неверный формат.");
                return Double(text);
            }
        }
        public static double DoubleNotZero(string text)
        {
            double input;
            try
            {
                Write(text);
                input = ToDouble(ReadLine());
                if (Check.IsZero(input))
                {
                    WriteLine("Введенная строка имела неверный формат.");
                    return Double(text);
                }
                return input;
            }
            catch (Exception)
            {
                WriteLine("Введенная строка имела неверный формат.");
                return Double(text);
            }
        }
        public static string String(string text)
        {
            string input;
            Write(text);
            input = ReadLine();
            return input;
        }
    }
    internal class Check
    {
        public static bool IsZero(double value)
        {
            if (value == 0)
                return true;
            else
                return false;
        }
    }
}
