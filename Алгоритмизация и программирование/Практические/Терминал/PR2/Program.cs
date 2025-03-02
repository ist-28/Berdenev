using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Выберите задачу" +
                "\n1 - Последовательность чисел" +
                "\n2 - Счастливые билеты" +
                "\n3 - Город" +
                "\n4 - Угадай число");
            int x = int.Parse(Console.ReadLine());

            switch (x) {
                case 0: Console.WriteLine("Такого решения нет"); Main(); break;
                case 1: Posled(); break;
                case 2: Bilet(); break;
                case 3: City(); break;
                case 4: Chislo(); break;
            }
        }
        public static void Posled()
        {
            Console.Clear();
            int a;
            string st;
            int max = int.MinValue;
            int sum = 0;
            int count = 0;
            int endsWith9 = 0;
            int d7Not5 = 0;
            int sign = 1;

            st = Console.ReadLine();
            a = Convert.ToInt32(st);

            while (a != 0)
            {
                if (a > max)
                    max = a;

                sum += a;
                count++;

                if (a % 10 == 9)
                    endsWith9++;

                if (a % 7 == 0 && a % 5 != 0)
                    d7Not5++;

                if (a < 0)
                    sign *= -1;

                st = Console.ReadLine();
                a = Convert.ToInt32(st);
            }

            Console.WriteLine($"a) Максимальный элемент: {max}");
            Console.WriteLine($"b) Сумма: {sum}, Среднее арифметическое: {(double)sum / count}");
            Console.WriteLine($"c) Чисел, оканчивающихся на 9: {endsWith9}");
            Console.WriteLine($"d) Чисел, делящихся на 7 и не делящихся на 5: {d7Not5}");
            Console.WriteLine($"e) Знак произведения (без 0): {(sign > 0 ? "Положительный" : "Отрицательный")}\n");
            Main();

        }
        public static void Bilet()
        {
            Console.Clear ();
            int moscowHappyTickets = 0;
            int piterHappyTickets = 0;

            for (int i = 1; i <= 999; i++)
            {
                string firs = i.ToString("D3");

                for (int j = 1; j <= 999; j++)
                {
                    string second = j.ToString("D3");
                    string ticket = firs + second;

                    // Московский способ
                    int sumFirstThree = int.Parse(ticket[0].ToString()) + int.Parse(ticket[1].ToString()) + int.Parse(ticket[2].ToString());
                    int sumLastThree = int.Parse(ticket[3].ToString()) + int.Parse(ticket[4].ToString()) + int.Parse(ticket[5].ToString());
                    if (sumFirstThree == sumLastThree)
                        moscowHappyTickets++;


                    // Питерский способ
                    int sumEven = int.Parse(ticket[0].ToString()) + int.Parse(ticket[2].ToString()) + int.Parse(ticket[4].ToString());
                    int sumOdd = int.Parse(ticket[1].ToString()) + int.Parse(ticket[3].ToString()) + int.Parse(ticket[5].ToString());

                    if (sumEven == sumOdd)
                        piterHappyTickets++;

                }
            }

            Console.WriteLine($"a) Московский способ: {moscowHappyTickets}");
            Console.WriteLine($"б) Питерский способ: {piterHappyTickets}\n");
            Main();

        }
        public static void City()
        {
            Console.Clear();
            Console.Write("Введите начальное население N: ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Введите процент прироста a: ");
            double aPercent = int.Parse(Console.ReadLine());
            Console.Write("Введите количество лет k: ");
            int k = int.Parse(Console.ReadLine());

            double population = n;
            for (int year = 1; year <= k; year++)
            {
                population *= (1 + aPercent / 100);
            }

            Console.WriteLine($"Население через {k} лет: {(int)population}");
            Main();

        }
        public static void Chislo()
        {
            Console.Clear();
            Random random = new Random();
            int x = random.Next(1, 21);
            int z;
            int attempts = 0;

            do
            {
                Console.Write("Введите ваше число (1-20): ");
                z = int.Parse(Console.ReadLine());
                attempts++;

                if (z < x)
                    Console.WriteLine("Загаданное число больше.");
                else if (z > x)
                    Console.WriteLine("Загаданное число меньше.");

            } while (x != z);

            Console.WriteLine($"Число угадано. Потребовалось {attempts} попыток.");
            Main();
        }
    }
}
