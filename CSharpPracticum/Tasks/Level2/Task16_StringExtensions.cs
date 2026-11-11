using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    internal class Task16_StringExtensions : ITask
    {
        public string Title => "==================================================\n Задание 16. Методы расширения для string\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Написать extension-методы для string.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Реализовать в статическом классе StringExtensions.\r\n - Методы: IsPalindrome(), WordCount(), ReverseString().\r\n - Методы должны быть public static с this string первым параметром.\r\n - Продемонстрировать вызов на 2–3 строках.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");



            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }
    }
}
