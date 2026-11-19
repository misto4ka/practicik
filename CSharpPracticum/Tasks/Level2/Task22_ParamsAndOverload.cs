using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task22_ParamsAndOverload : ITask
    {
        public string Title => "===================================== Задание 22. params-метод + перегрузка =====================================\n";
        public string Description => "Описание:\r\n Написать метод суммы с params и его перегрузку.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Sum(params int[] numbers) — сумма произвольного числа чисел.\r\n - Перегрузка Sum(int[] numbers) — та же логика для массива (или с доп. параметром, чтобы не было конфликта сигнатур).\r\n - Продемонстрировать: Sum(1, 2, 3), Sum(new[] {1, 2, 3}), Sum().\r\n - Пустой вызов — вернуть 0.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            int res1 = Sum(1, 2, 3);
            int res2 = Sum(new[] { 1, 2, 3 });
            int resEmpty = Sum();
            double resDouble = Sum(new double[] { 1.5, 2.5, 3.0 });

            Console.WriteLine($"Sum(1, 2, 3)                    = {res1}");
            Console.WriteLine($"Sum(new[] {{ 1, 2, 3 }})          = {res2}");
            Console.WriteLine($"Sum() [пустой вызов]            = {resEmpty}");
            Console.WriteLine($"Перегрузка Sum(double[] массив) = {resDouble}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private int Sum(params int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return 0;

            int total = 0;
            foreach (int n in numbers)
                total += n;

            return total;
        }

        private double Sum(double[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return 0;

            double total = 0;
            foreach (double n in numbers)
                total += n;

            return total;
        }
    }
}