using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task20_GenericsSwapMax : ITask
    {
        public string Title => "================================ Задание 20. Обобщения: Swap<T>, Max<T> с IComparable<T> ================================\n";
        public string Description => "Описание:\r\n Написать обобщённые методы Swap<T> и Max<T>.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Swap<T>(ref T a, ref T b) — меняет значения местами.\r\n - Max<T>(T a, T b) where T : IComparable<T> — возвращает большее.\r\n - Продемонстрировать на int, string, double.\r\n - Swap — без ограничений, Max — с ограничением.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            // 1. int
            int a = 10, b = 25;
            Console.WriteLine($"[int]    До Swap: a = {a}, b = {b} | Max = {Max(a, b)}");
            Swap(ref a, ref b);
            Console.WriteLine($"[int]    После Swap: a = {a}, b = {b}\n");

            // 2. double
            double d1 = 3.14, d2 = 2.71;
            Console.WriteLine($"[double] До Swap: d1 = {d1}, d2 = {d2} | Max = {Max(d1, d2)}");
            Swap(ref d1, ref d2);
            Console.WriteLine($"[double] После Swap: d1 = {d1}, d2 = {d2}\n");

            // 3. string
            string s1 = "Alpha", s2 = "Omega";
            Console.WriteLine($"[string] До Swap: s1 = \"{s1}\", s2 = \"{s2}\" | Max = \"{Max(s1, s2)}\"");
            Swap(ref s1, ref s2);
            Console.WriteLine($"[string] После Swap: s1 = \"{s1}\", s2 = \"{s2}\"");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        private T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }
    }
}