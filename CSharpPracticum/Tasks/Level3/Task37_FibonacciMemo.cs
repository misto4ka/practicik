using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task37_FibonacciMemo : ITask
    {
        public string Title => "==================================================\r\nЗадание 37. Мемоизация Фибоначчи через Dictionary\r\n--------------------------------------------------";
        public string Description => "Описание: реализовать мемоизированную функцию Фибоначчи. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nОбычная рекурсивная версия (для сравнения на маленьком N).\r\nМемоизированная версия с Dictionary<int, long>.\r\nЗамерить время обеих на N = 30–35 через Stopwatch.\r\nВывести оба времени и разницу." };

        private static long FibRecursive(int n)
        {
            if (n <= 1) return n;
            return FibRecursive(n - 1) + FibRecursive(n - 2);
        }

        private static long FibMemo(int n, Dictionary<int, long> cache)
        {
            if (n <= 1) return n;
            if (cache.TryGetValue(n, out long cached)) return cached;

            long result = FibMemo(n - 1, cache) + FibMemo(n - 2, cache);
            cache[n] = result;
            return result;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            Console.WriteLine("Проверка на малом N:");
            for (int i = 0; i <= 10; i++)
            {
                Console.Write($"{FibRecursive(i)} ");
            }
            Console.WriteLine();
            Console.WriteLine();

            const int n = 35;

            Stopwatch sw = Stopwatch.StartNew();
            long recursive = FibRecursive(n);
            sw.Stop();
            double recursiveMs = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            long memo = FibMemo(n, new Dictionary<int, long>());
            sw.Stop();
            double memoMs = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"N = {n}");
            Console.WriteLine($"Рекурсия:    {recursive}, время {recursiveMs:F3} мс");
            Console.WriteLine($"Мемоизация:  {memo}, время {memoMs:F3} мс");
            Console.WriteLine($"Разница во времени: {recursiveMs - memoMs:F3} мс");
            Console.WriteLine($"Результаты совпадают: {recursive == memo}");

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
