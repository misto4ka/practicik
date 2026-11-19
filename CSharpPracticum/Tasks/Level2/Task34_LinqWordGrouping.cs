using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task34_LinqWordGrouping : ITask
    {
        public string Title => "=================================== Задание 34. LINQ: группировка слов по первой букве ===================================\n";
        public string Description => "Описание:\r\n Сгруппировать список слов по первой букве и отсортировать группы и слова внутри них.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Взять список слов (10+).\r\n - Сгруппировать через LINQ GroupBy(w => w[0]).\r\n - Отсортировать группы по первой букве.\r\n - Внутри группы — отсортировать слова.\r\n - Вывести «Буква: слово1, слово2, ...».\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            var words = new List<string>
        {
            "яблоко", "банан", "арбуз", "вишня", "апельсин",
            "груша", "виноград", "брусника", "ананас", "ежевика",
            "голубика", "абрикос"
        };

            Console.WriteLine($"Исходный список: {string.Join(", ", words)}\n");

            var groupedWords = words
                .GroupBy(w => char.ToUpper(w[0]))
                .OrderBy(g => g.Key);

            Console.WriteLine("Результат группировки:");
            foreach (var group in groupedWords)
            {
                var sortedWordsInGroup = group.OrderBy(w => w);
                Console.WriteLine($"{group.Key}: {string.Join(", ", sortedWordsInGroup)}");
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }
    }
}