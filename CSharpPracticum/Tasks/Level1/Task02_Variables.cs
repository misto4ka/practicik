using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task02_Variables : ITask
    {
        public string Title => "==================================================\n Задание 2. Переменные примитивных типов\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Объявить переменные всех примитивных типов и вывести их значения и типы.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Объявить минимум: int, long, double, float, decimal, bool, char, string.\r\n - Для каждой переменной вывести значение и GetType().Name.\r\n - decimal — с суффиксом m, float—сf.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");
            
            int a = 1;
            long b = 200200200200;
            double c = 3.14;
            float d = 4.44f;
            decimal e = 5.15m;
            bool f = false;
            char g = 'g';
            string h = "hello";
            Console.WriteLine($" a = {a.GetType().Name}\n b = {b.GetType().Name}\n c = {c.GetType().Name}\n d = {d.GetType().Name}");
            Console.WriteLine($" e = {e.GetType().Name}\n f = {f.GetType().Name}\n g = {g.GetType().Name}\n h = {h.GetType().Name}\n");

            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
