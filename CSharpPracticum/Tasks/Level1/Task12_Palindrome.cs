using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task12_Palindrome : ITask
    {
        public string Title => "==================================================\n Задание 12. Палиндром (без учёта регистра/пробелов)\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Проверить, является ли строка палиндромом.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Игнорировать регистр (ToLower/ToUpper).\r\n - Игнорировать пробелы и знаки пунктуации (или хотя бы пробелы).\r\n - Не использовать LINQ Reverse.\r\n - Пустая строка и строка из одного символа — палиндром.\r\n" };

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
