using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task07_StringReverse : ITask
    {
        public string Title => "==================================================\n Задание 7. Реверс строки без LINQ\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Перевернуть введённую строку.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Не использовать Array.Reverse и LINQ.\r\n - Реализовать через цикл for от конца к началу (или через char[] и swap).\r\n - Пустая строка и строка из одного символа — обрабатываются корректно.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.Write("Введите строку: ");
            string stroka = Console.ReadLine();
            string result = " ";
            for (int i = stroka.Length - 1; i >= 0; i--)
            { 
                result += stroka[i];
            }
            Console.WriteLine($"Перевёрнутая строка:{result}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
