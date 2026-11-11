using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

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



            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
