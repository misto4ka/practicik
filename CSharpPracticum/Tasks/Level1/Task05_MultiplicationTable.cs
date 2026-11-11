using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task05_MultiplicationTable : ITask
    {
        public string Title => "==================================================\n Задание 5. Таблица умножения 10x10\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Вывести таблицу умножения 10x10.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Использовать вложенные циклы for.\r\n - Выровнять столбцы.\r\n - Заголовки строк/столбцов — не обязательны, но приветствуются.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            for(int x = 1; x <= 10; x++)
            {
               for (int y = 1; y <= 10; y++)
                {
                    Console.Write($"{x*y,4} ");

                }

               Console.WriteLine();
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
