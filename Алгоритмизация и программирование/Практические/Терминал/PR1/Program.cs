using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Выберите действие\n" +
                "1 - Квадратное уравнение\n" +
                "2 - Max, Min\n" +
                "3 - Действительные числа c 0\n" +
                "4 - Действительные числа нахлждение средней арифмет и геометр\n" +
                "5 - Действительные числа с заменной");
            int a = int.Parse(Console.ReadLine());

            switch (a)
            {
                case 0:
                    Console.WriteLine("Такого действия нет\n");
                    Main();
                    break;
                case 1:
                    Root_of_equ();
                    break;
                case 2:
                    Min_Max();
                    break;
                case 3:
                    Deist_0();
                    break;
                case 4:
                    Deist_arif_geom();
                    break;
                case 5:
                    Deist_zamena();
                    break;

            }
        }
        public static void Root_of_equ()
        {
            Console.Clear();

            double x1, x2;

            Console.Write("Напишите а = ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("Напишите b = ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("Напишите c = ");
            double c = double.Parse(Console.ReadLine());

            double dis = Math.Pow(b, 2) - 4 * a * c;

            if (a == 0)
            {
                Console.WriteLine("Так как a = 0, мы получаем линейное уравнение");
                Main();
            }
            else if (dis > 0)
            {
                x1 = (-b + Math.Sqrt(dis)) / (2 * a);
                x2 = (-b - Math.Sqrt(dis)) / (2 * a);

                Console.WriteLine($"Дискрименант = {dis} > 0, Следовательно x1 = {x1}, x2 = {x2}\n");
                Main();
            }
            else if (dis == 0)
            {
                x1 = -b / (2 * a);
                Console.WriteLine($"Дискрименант = {dis}, Следовательно корень один => x = {x1}\n");
                Main();
            }
            else
            {
                Console.WriteLine($"Дискрименант = {dis} < 0, Следовательно кореней нет\n");
                Main();
            }
        }
        public static void Min_Max()
        {
            Console.Clear();

            Console.Write("Введите A - ");
            int A = int.Parse(Console.ReadLine());
            Console.Write("Введите B - ");
            int B = int.Parse(Console.ReadLine());
            Console.Write("Введите C - ");
            int C = int.Parse(Console.ReadLine());

            if (A % 2 == 0 || B % 2 == 0 || C % 2 == 0)
            {
                int max;
                if (A == B || A == C || B == C)
                {
                    Console.WriteLine("Нельзя определить максимальное из 3х чисел");
                    Main();
                }
                else
                {
                    max = Math.Max(A, Math.Max(B,C));
                    Console.WriteLine($"Максимальное = {max}");
                    Main();
                }
            }
            else {
                int min;
                if (A == B || A == C || B == C)
                {
                    Console.WriteLine("Нельзя определить минимальное из 3х чисел");
                    Main();
                }
                else
                {
                     min = Math.Min(A, Math.Min(B, C));
                     Console.WriteLine($"Минимальное = {min}");
                    Main();

                }
            }

        }
        public static void Deist_0() {
            Console.Clear();

            double X1, Y1, Z1;
            Console.Write("Введите X - ");
            double X = double.Parse(Console.ReadLine());
            Console.Write("Введите Y - ");
            double Y = double.Parse(Console.ReadLine());
            Console.Write("Введите Z - ");
            double Z = double.Parse(Console.ReadLine());

            if(X == 0 || Y == 0 || Z == 0)
            {
                X1 = (Y + Z) / 2;
                Y1 = (Z + X) / 2;
                Z1 = (X + Y) / 2;

                Console.WriteLine($"Ответ = {X1} {Y1} {Z1}");
                Main();
            }
            else
            {
                double min;
                min = Math.Min(X, Math.Min(Y, Z));
                X = min;
                Y = min;
                Z = min;

                Console.WriteLine($"Ответ = {X} {Y} {Z}");
                Main();
            }
        }
        public static void Deist_arif_geom()
        {
            Console.Clear();

            Console.Write("Введите A - ");
            double A = double.Parse(Console.ReadLine());
            Console.Write("Введите B - ");
            double B = double.Parse(Console.ReadLine());
            Console.Write("Введите C - ");
            double C = double.Parse(Console.ReadLine());
            Console.Write("Введите D - ");
            double D = double.Parse(Console.ReadLine());

            if (A < B && B < C && C < D)
            {
                double arif = (A + B + C + D) / 4;
                Console.WriteLine($"Среднее арифметическое = {arif}");
                Main();
            }
            else if (A > B && B > C && C > D) {
                Console.WriteLine($"Ответ = {A} {B} {C} {D}");
                Main();
            }
            else
            {
                double geom = A * B * C * B;
                geom = Math.Pow(geom, 1.0/4.0);
                Console.WriteLine($"Среднее геометрическое = {geom}");
                Main();
            }
        }
        public static void Deist_zamena()
        {
            Console.Clear();

            Console.Write("Введите A - ");
            double A = double.Parse(Console.ReadLine());
            Console.Write("Введите B - ");
            double B = double.Parse(Console.ReadLine());
            Console.Write("Введите C - ");
            double C = double.Parse(Console.ReadLine());
            Console.Write("Введите D - ");
            double D = double.Parse(Console.ReadLine());

            if (A < B && B < C && C < D)
            {
                A = 133;
                B = 133;
                C = 133;
                D = 133;

                Console.WriteLine($"Ответ = {A} {B} {C} {D}");
                Main();
            }
            else if (A > B && B > C && C > D)
            {
                A = -A;
                B = -B;
                C = -C;
                D = -D;

                Console.WriteLine($"Ответ = {A} {B} {C} {D}");
                Main();
            }
            else
            {
                Console.WriteLine($"Ответ = {A} {B} {C} {D}");
                Main();
            }
        }

    }
}
