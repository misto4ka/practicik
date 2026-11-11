using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task04_Parity : ITask
    {
        public string Title => "==================================================\n Задание 4. Чётность числа (два способа)\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Определить, чётное ли введённое число.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Способ 1: через % 2 == 0.\r\n - Способ 2: через тернарный оператор ? :.\r\n - Оба способа реализовать отдельными методами.\r\n - Вывести результат обоих способов.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.Write("Введите число: ");
            int x = Convert.ToInt16(Console.ReadLine());
            //1 способ
            if (x % 2 == 0)
            {
                Console.WriteLine($" 1 способ: {x} - Чётное");
            }
            else
            {
                Console.WriteLine($" 1 способ: {x} - Нечётное");
            }
            //2 способ
            string y = (((x & 1) == 0)) ? "Чётное" : "Нечётное";
            Console.WriteLine($" 2 способ: {x} - {y}");

            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
