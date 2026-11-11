using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task10_SortComparison : ITask
    {
        public string Title => "==================================================\n Задание 10. Сортировка пузырьком + вставками\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Отсортировать массив двумя алгоритмами.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Реализовать BubbleSort и InsertionSort отдельными методами.\r\n - Оба — без LINQ и Array.Sort.\r\n - Замерить время каждого через Stopwatch на массиве 10 000 элементов.\r\n - Вывести оба времени.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            int size = 10000;
            int[] massiv = new int[size];
            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                massiv[i] = rnd.Next(1,10001);
            }

            int[] bubble = (int[])massiv.Clone();
            int[] insertion = (int[])massiv.Clone();

            Stopwatch swBubble = Stopwatch.StartNew();
            BubbleSort(bubble);
            swBubble.Stop();

            Stopwatch swInsertion = Stopwatch.StartNew();
            InsertionSort(insertion);
            swInsertion.Stop();

            Console.WriteLine($"Время сортировки пузырьком: {swBubble.ElapsedMilliseconds} мс");

            Console.WriteLine($"Время сортировки вставками: {swInsertion.ElapsedMilliseconds} мс");

            Console.WriteLine($"Оба массива отсортированы одинаково: {AreArraysEqual(bubble, insertion)}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }

        public void BubbleSort (int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        public void InsertionSort(int[] arr)
        {
            for (int i = 1;  i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        public bool AreArraysEqual(int[] arr1, int[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }

            for (int i = 0;i < arr1.Length;i++)
            {
                if (arr1[i] != arr2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
