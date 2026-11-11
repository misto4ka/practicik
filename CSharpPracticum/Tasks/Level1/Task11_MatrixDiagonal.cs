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



            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
