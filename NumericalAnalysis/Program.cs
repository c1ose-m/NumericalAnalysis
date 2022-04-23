using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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
        // Функция
        static double Function(double x)
            => Pow(x, 2) - Cos(PI * x);
        // Производная функции
        static double Derivative(double x)
            => 2 * x + PI * Sin(PI * x);
        // Формула для вычисления x
        static double Formula(double a, double b)
            => a - (Function(a) * (b - a) / (Function(b) - Function(a)));
        // Решение
        public static void Solution()
        {
            List<double> fx = new List<double>();
            List<double> dx = new List<double>();
            bool isIsolation = true, isPositive;
            double[] suspicious = { 0, 0 };
            double[] fxs = { 0, 1 };
            double a, b, h, k;
            int fractional;
            string[] kParts;
            a = Input.Double("\nНачало отрезка: ");
            b = Input.Double("\nКонец отрезка: ");
            h = Input.DoubleNotZero("\nШаг функции: ");
            k = Input.DoubleNotZero("\nТочность: ");
            // Количество знаков после запятой в ответе
            if (k.ToString().Contains('-'))
            {
                kParts = k.ToString().Split('-');
                fractional = ToInt32(kParts[1]);
            }
            else
                fractional = k.ToString().Length;
            // Значения функции на отрезке
            for (double i = a; i <= b + h; i += h)
                fx.Add(Function(i));
            // Нахождение подозрительного на отрезок изоляции
            for (int i = 0; i < fx.Count - 1; i++)
                if (fx[i] < 0 & fx[i + 1] > 0)
                {
                    suspicious[0] = a + h * i;
                    suspicious[1] = a + h * (i + 1);
                }
                else if (fx[i] > 0 & fx[i + 1] < 0)
                {
                    suspicious[0] = a + h * i;
                    suspicious[1] = a + h * (i + 1);
                }
            // Производная на отрезке
            for (double i = suspicious[0]; i <= suspicious[1]; i += (suspicious[1] - suspicious[0]) / 10)
                dx.Add(Derivative(i));
            // Проверка на отрезок изоляции
            if (suspicious[0] > 0)
                isPositive = true;
            else
                isPositive = false;
            foreach (double d in dx)
                if ((d < 0 & isPositive == true) | (d > 0 & isPositive == false))
                {
                    isIsolation = false;
                    break;
                }
            // Если это отрезок изоляции
            if (isIsolation == true)
            {
                // Вычисляем x, пока два соседних не совпадут
                fxs[0] = Round(Formula(suspicious[0], suspicious[1]), fractional + 1);
                while (fxs[0] != fxs[1])
                {
                    fxs[1] = fxs[0];
                    fxs[0] = Round(Formula(fxs[0], suspicious[1]), fractional + 1);
                }
                WriteLine($"\nОтвет: x = {Round(fxs[0], fractional)}");
            }
            // Иначе сказать, что не отрезок
            else
                WriteLine($"\nОтрезок ({Round(suspicious[0], fractional + 1)};" +
                    $"{Round(suspicious[1], fractional + 1)}) - не является отрезком изоляции.");
        }
    }
    internal class Interpolation
    {
        public static void Solution()
        {
            List<List<double>> mainMatrix = new List<List<double>>
            {
                new List<double>{1, 1, 1, 1},
                new List<double>{3.375, 2.25, 1.5, 1},
                new List<double>{8, 4, 2, 1},
                new List<double>{10.648, 4.84, 2.2, 1}
            };
            double det = 1, r, g;
            int n;
            n = Input.IntNotZero("Введите размерность матрицы: ");
            List<List<double>> aMatrix = mainMatrix;
            for (int i = 0; i < n - 1; i++)
            {
                g = aMatrix[i][i];
                for (int j = 0; j < n; j++)
                    aMatrix[i][j] /= g;
                det *= g;
                for (int j = 1 + i; j < n; j++)
                {
                    r = -aMatrix[j][i];
                    for (int k = 0; k < n; k++)
                        aMatrix[j][k] += aMatrix[i][k] * r;
                }
            }
            for (int i = 0; i < n; i++)
                det *= aMatrix[i][i];
            WriteLine(det);
        }
    }
    internal class SimpsonRule
    {
        static double Function(double x)
            => (3 * Pow(x, 2) + Sin(x)) / Pow(x, 2);
        public static void Solution()
        {
            List<double> fxOne = new List<double>();
            List<double> fxTwo = new List<double>();
            double a, b, h, intOne, intTwo, sumOne = 0, sumTwo = 0;
            int nOne, nTwo, m;
            a = Input.Double("\nНачало отрезка: ");
            b = Input.Double("\nКонец отрезка: ");
            nOne = Input.IntNotZero("\nКоличество шагов функции: ");
            h = (b - a) / nOne;
            m = nOne / 2;
            nTwo = nOne * 2;
            for (double i = a; i <= b + h; i += h)
                fxOne.Add(Function(i));
            for (double i = a; i <= b + h / 2; i += h / 2)
                fxTwo.Add(Function(i));
            for (int i = 2; i <= nOne - 1; i += 2)
                sumOne += fxOne[i];
            for (int i = 1; i <= nOne - 1; i += 2)
                sumTwo += fxOne[i];
            intOne = (b - a) / (6 * m) * (fxOne[0] + fxOne[nOne - 1] + 2 * sumOne + 4 * sumTwo);
            sumOne = 0;
            sumTwo = 0;
            for (int i = 2; i <= nTwo - 1; i += 2)
                sumOne += fxTwo[i];
            for (int i = 1; i <= nTwo - 1; i += 2)
                sumTwo += fxTwo[i];
            intTwo = (b - a) / (6 * nOne) * (fxTwo[0] + fxTwo[nTwo - 1] + 2 * sumOne + 4 * sumTwo);
            WriteLine($"\nОтвет: Интеграл 1 = {intOne}\n" +
                      $"       Погрешность = {Round(Abs(intTwo - intOne) / 15, 10)}");
        }
    }
    internal class RungeKuttaMethod
    {
        static double Function(double x, double y)
            => Pow(x, 2) - 2 * y;
        public static void Solution()
        {
            double a, b, h, x0, y0, r1, r2, r3, r4;
            int n;
            a = Input.Double("\nНачало отрезка: ");
            b = Input.Double("\nКонец отрезка: ");
            n = Input.IntNotZero("\nКоличество шагов функции: ");
            h = Input.Double("\nШаг функции: ");
            x0 = Input.Double("\nЗначение x0: ");
            y0 = Input.Double("\nЗначение y0: ");
            WriteLine("x // y");
            for (double i = x0; i <= b; i += h)
            {
                WriteLine($"{i} // {y0}");
                r1 = h * Function(i, y0);
                r2 = h * Function(i + h / 2, y0 + r1 / 2);
                r3 = h * Function(i + h / 2, y0 + r2 / 2);
                r4 = h * Function(i + h, y0 + r3);
                WriteLine($"{r1} // {r2} // {r3} // {r4}");
                y0 = y0 + 1 / (double)6 * (r1 + 2 * r2 + 2 * r3 + r4);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Привет! Тут метод хорд, интерполяция, метод парабол и Рунге-Кутты(2).\n\n");
            /*WriteLine("Метод хорд.");
            SecantMethod.Solution();*/
            WriteLine("\n\nИнтерполяция.");
            Interpolation.Solution();
            /*WriteLine("\n\nМетод парабол.");
            SimpsonRule.Solution();
            WriteLine("\n\nМетод Рунге-Кутты.");
            RungeKuttaMethod.Solution();*/
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
                    return DoubleNotZero(text);
                }
                return input;
            }
            catch (Exception)
            {
                WriteLine("Введенная строка имела неверный формат.");
                return DoubleNotZero(text);
            }
        }
        public static int IntNotZero(string text)
        {
            int input;
            try
            {
                Write(text);
                input = ToInt32(ReadLine());
                if (Check.IsZero(input))
                {
                    WriteLine("Введенная строка имела неверный формат.");
                    return IntNotZero(text);
                }
                return input;
            }
            catch (Exception)
            {
                WriteLine("Введенная строка имела неверный формат.");
                return IntNotZero(text);
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
        public static bool IsZero(int value)
        {
            if (value == 0)
                return true;
            else
                return false;
        }
    }
}
