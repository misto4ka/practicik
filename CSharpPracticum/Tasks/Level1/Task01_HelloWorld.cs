using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task01_HelloWorld : ITask
    {
        public string Title => "==================================================\n Задание 1. Hello, World + текущая дата\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Вывести приветствие и текущую дату.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Использовать Console.WriteLine.\r\n - Дату получить через DateTime.Now, отформатировать как dd.MM.yyyy.\r\n - Вывести две строки: приветствие и дату.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.WriteLine($" Hello world! {DateTime.Now.ToString("dd.MM.yyyy")}\n");

            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
