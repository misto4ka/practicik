using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task08_FizzBuzz : ITask
    {
        public string Title => "==================================================\n Задание 8. FizzBuzz 1–100\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Вывести числа от 1 до 100 с заменами.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Кратно 3 → Fizz.\r\n - Кратно 5 → Buzz.\r\n - Кратно 15 → FizzBuzz.\r\n - Иначе — само число.\r\n - Порядок проверок: сначала 15, потом 3 и 5.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            for (int i = 1; i <= 100;  i++)
            {
                if (i % 15 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (i % 5  == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
