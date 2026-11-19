using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task11_MatrixDiagonal : ITask
    {
        public string Title => "==================================================\n Задание 11. Диагональ матрицы NxN\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Найти сумму элементов главной диагонали матрицы NxЁN.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Размер N вводится пользователем (минимум 2).\r\n - Матрица заполняется случайными числами.\r\n - Сумма считается без LINQ, в одном цикле for.\r\n - Вывести саму матрицу и сумму.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.Write("Введите размер матрицы N (минимум 2): ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int n) || n < 2)
            {
                Console.WriteLine("Ошибка! Размер матрицы должен быть целым числом от 2 и больше.");
                return;
            }

            int[,] matrix = new int[n, n];
            Random rnd = new Random();

            Console.WriteLine($"\nСгенерированная матрица {n}x{n}:");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = rnd.Next(1, 10);
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }

            int diagonalSum = 0;
            for (int i = 0; i < n; i++)
            {
                diagonalSum += matrix[i, i];
            }

            Console.WriteLine($"\nСумма элементов главной диагонали: {diagonalSum}");


            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
