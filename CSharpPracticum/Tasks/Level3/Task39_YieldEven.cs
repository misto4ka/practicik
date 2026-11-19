using System;
using System.Collections.Generic;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task39_YieldEven : ITask
    {
        public string Title => "==================================================\r\nЗадание 39. yield return — генератор чётных чисел\r\n--------------------------------------------------";
        public string Description => "Описание: реализовать генератор чётных чисел через yield return. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nМетод IEnumerable<int> GetEvenNumbers(int max) — возвращает чётные числа до max.\r\nРеализовать именно через yield return, без промежуточного списка.\r\nПродемонстрировать работу в foreach.\r\nДополнительно: показать ленивость — вывести сообщение внутри генератора и убедиться, что оно печатается по одному." };

        private static IEnumerable<int> GetEvenNumbers(int max)
        {
            for (int i = 2; i <= max; i += 2)
            {
                Console.WriteLine($"  [генератор] отдаю {i}");
                yield return i;
            }
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            Console.WriteLine("Чётные числа до 10 (сообщения генератора и foreach чередуются):");
            foreach (int number in GetEvenNumbers(10))
            {
                Console.WriteLine($"[foreach] получил {number}");
            }

            Console.WriteLine();
            Console.WriteLine("Вызов GetEvenNumbers(10) без перебора ничего не печатает:");
            IEnumerable<int> lazy = GetEvenNumbers(10);
            Console.WriteLine("  Генератор создан, но ещё не запущен.");

            Console.WriteLine("Берём только первое значение через GetEnumerator:");
            using (IEnumerator<int> enumerator = lazy.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    Console.WriteLine($"[вручную] получил {enumerator.Current}");
                }
            }

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
