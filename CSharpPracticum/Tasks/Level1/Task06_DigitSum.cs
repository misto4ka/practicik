using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task06_DigitSum : ITask
    {
        public string Title => "==================================================\n Задание 6. Сумма цифр числа (строка + арифметика)\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Найти сумму цифр введённого числа (например, 1234 → 10).\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Способ 1 (строка): число → ToString() → пройти по символам → int.Parse каждой цифры.\r\n - Способ 2 (арифметика): цикл while (n > 0) { sum += n % 10; n /= 10; }.\r\n - Оба способа оформить отдельными методами.\r\n - Отрицательные числа — брать модуль.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.Write("Введите число: ");
            int chislo = int.Parse(Console.ReadLine());
            chislo = Math.Abs(chislo);
            string chisl0 = chislo.ToString();
            //1 способ
            int sum1 = 0;
            foreach (char c in chisl0)
            {
                sum1 += int.Parse(c.ToString());
            }
            Console.WriteLine($"\n1 способ: Сумма {chislo} = {sum1}\n");

            //2 способ
            int sum2 = 0;
            while (chislo > 0)
            {
                sum2 += chislo % 10;
                chislo /= 10;  
            }
            Console.WriteLine($"2 способ: Сумма {chisl0} = {sum2}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
