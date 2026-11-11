using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task09_ArrayStats : ITask
    {
        public string Title => "==================================================\n Задание 9. Массивы: min/max/sum/avg\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Для массива чисел найти минимум, максимум, сумму и среднее.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Заполнить массив (например, случайными числами 1–100, размер 20).\r\n - Реализовать без LINQ (Min/Max/Sum/Average запрещены).\r\n - Оформить 4 отдельных метода.\r\n - Пустой массив — обработать (вернуть ошибку или 0, но не падать).\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.WriteLine("Заполненный массив:");
            int[] numbers = new int[20];
            Random rnd = new Random();
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rnd.Next(1,101);
                Console.Write($"{numbers[i]} ");
            }
            Console.WriteLine();

            //1метод
            Console.WriteLine($"\nСумма чисел массива: {GetSum(numbers)}");

            //2метод
            Console.WriteLine($"\nСреднее арифметическое массива: {GetAverage(numbers)}");

            //3метод
            Console.WriteLine($"\nМинимальное значение массива: {GetMin(numbers)}");

            //4метод
            Console.WriteLine($"\nМаксимальное значение массива: {GetMax(numbers)}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }

        public int GetSum(int[] arr)
        {
            if (arr == null ||  arr.Length == 0)
            {
                return 0;
            }
            int sum = 0;
            for (int i = 0;i < arr.Length;i++)
            {
                sum += arr[i];
            }
            return sum;
        }

        public double GetAverage(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return 0;
            }
            double average = (double)GetSum(arr) / arr.Length;
            return average;
        }

        public int GetMin(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return 0;
            }
            int min = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            return min;
        }
        public int GetMax(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return 0;
            }
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
    }
}
