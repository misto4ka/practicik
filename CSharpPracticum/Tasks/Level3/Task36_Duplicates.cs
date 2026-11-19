using System;
using System.Collections.Generic;
using System.Linq;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task36_Duplicates : ITask
    {
        public string Title => "==================================================\r\nЗадание 36. Дубликаты в List (HashSet + GroupBy)\r\n--------------------------------------------------";
        public string Description => "Описание: найти дубликаты в списке чисел двумя способами. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nСпособ 1: через HashSet<int> — при добавлении проверять Contains.\r\nСпособ 2: через LINQ GroupBy(x => x).Where(g => g.Count() > 1).\r\nОба способа оформить отдельными методами.\r\nПродемонстрировать на списке с повторяющимися значениями." };

        private static List<int> FindDuplicatesWithHashSet(List<int> numbers)
        {
            HashSet<int> seen = new HashSet<int>();
            HashSet<int> duplicates = new HashSet<int>();

            foreach (int n in numbers)
            {
                if (seen.Contains(n))
                    duplicates.Add(n);
                else
                    seen.Add(n);
            }

            return duplicates.ToList();
        }

        private static List<int> FindDuplicatesWithGroupBy(List<int> numbers)
        {
            return numbers
                .GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            List<int> numbers = new List<int> { 1, 2, 3, 2, 4, 5, 3, 6, 2, 7, 1, 8 };
            Console.WriteLine($"Исходный список: {string.Join(", ", numbers)}");

            List<int> byHashSet = FindDuplicatesWithHashSet(numbers);
            List<int> byGroupBy = FindDuplicatesWithGroupBy(numbers);

            Console.WriteLine($"HashSet: {string.Join(", ", byHashSet.OrderBy(x => x))}");
            Console.WriteLine($"GroupBy: {string.Join(", ", byGroupBy.OrderBy(x => x))}");

            bool same = byHashSet.OrderBy(x => x).SequenceEqual(byGroupBy.OrderBy(x => x));
            Console.WriteLine($"Наборы дубликатов совпадают: {same}");

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
