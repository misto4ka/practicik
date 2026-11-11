using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task03_Calculator : ITask
    {
        public string Title => "==================================================\n Задание 3. Калькулятор с обработкой деления на ноль\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Принимать два числа и оператор (+ - * /), выводить результат.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Оператор обрабатывать через switch.\r\n - При делении на ноль — вывести сообщение об ошибке, не ронять приложение.\r\n - Некорректный оператор — вывести «Неизвестная операция».\r\n - Ввод чисел — через double.Parse или TryParse.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.Write("Введите переменную а = ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("\nВведите переменную b = ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("\nВыберите действие (+, -, *, /): ");
            string vibor = (Console.ReadLine());
            switch (vibor)
            {
                case "+":
                    double plus = a + b;
                    Console.WriteLine($"{a} + {b} = {plus}");
                break;

                case "-":
                    double minus = a - b;
                    Console.WriteLine($"{a} - {b} = {minus}");
                break;

                case "*":
                    double ymn = a * b;
                    Console.WriteLine($"{a} * {b} = {ymn}");
                break;

                case "/":
                    if (b == 0)
                    {
                        Console.WriteLine("Ошибка, деление на 0 невозможно");
                    }
                    else
                    {
                        double delenie = a / b;
                        Console.WriteLine($"{a} / {b} = {delenie}");
                    }
                break;
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
