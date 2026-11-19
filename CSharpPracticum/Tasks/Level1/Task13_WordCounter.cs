using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace CSharpPracticum.Tasks.Level1
{
    internal class Task13_WordCounter : ITask
    {
        public string Title => "==================================================\n Задание 13. Счётчик вхождений слов в тексте (Dictionary)\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Посчитать, сколько раз каждое слово встречается в тексте.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Использовать Dictionary <string, int>.\r\n - Разбить текст на слова (по пробелам и знакам пунктуации).\r\n - Привести слова к нижнему регистру.\r\n - Вывести пары «слово → количество», отсортированные по убыванию количества.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.WriteLine("Введите текст для подсчета слов (или нажмите Enter для тестового текста):");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                input = "Кот, собака и еще один кот! Собака любит играть, а кот — спать. Кот — ленивый.";
                Console.WriteLine($"Используется тестовый текст: \"{input}\"\n");
            }

            CountWords(input);


            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }

        public void CountWords(string text)
        {

            char[] separators = new char[]
            {
        ' ', '\t', '\n', '\r', '.', ',', '!', '?', ':', ';', '-', '—', '"', '\'', '(', ')', '[', ']'
            };


            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            foreach (string rawWord in words)
            {
                string word = rawWord.ToLower();

                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
                else
                {
                    wordCounts[word] = 1;
                }
            }


            var sortedWords = wordCounts.OrderByDescending(pair => pair.Value);

            Console.WriteLine("Результат подсчета (по убыванию):");
            foreach (var pair in sortedWords)
            {
                Console.WriteLine($"{pair.Key} → {pair.Value}");
            }
        }

    }
}
