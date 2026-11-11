using CSharpPracticum.Tasks.Level1;
using System;
using System.Collections.Generic;
using System.Text;
using CSharpPracticum.Common;
using CSharpPracticum.Tasks.Level2;

namespace CSharpPracticum.Menu
{
    public class LevelMenu
    {
        public static void Show(int LevelMenu)
        {
            //Базовый уровень
            while (true)
            {
                if (LevelMenu == 1)
                {
                    Console.WriteLine("--- Уровень 1: Базовый ---");
                    Console.WriteLine("1. Hello, World + текущая дата");
                    Console.WriteLine("2. Переменные примитивных типов");
                    Console.WriteLine("3. Калькулятор с обработкой деления на ноль");
                    Console.WriteLine("4. Чётность числа (два способа)");
                    Console.WriteLine("5. Таблица умножения 10×10");
                    Console.WriteLine("6. Сумма цифр числа (строка + арифметика)");
                    Console.WriteLine("7. Реверс строки без LINQ");
                    Console.WriteLine("8. FizzBuzz 1–100");
                    Console.WriteLine("9. Массивы: min/max/sum/avg");
                    Console.WriteLine("10. Сортировка пузырьком + вставками");
                    Console.WriteLine("11. Диагональ матрицы N×N");
                    Console.WriteLine("12. Палиндром (без учёта регистра/пробелов)");
                    Console.WriteLine("13. Счётчик вхождений слов в тексте (Dictionary)");
                    Console.WriteLine("14. Возраст по дате рождения");
                    Console.WriteLine("15. Git: репозиторий → ветка → merge → разрешение конфликта");
                    Console.WriteLine("16. Выход\n");

                    Console.Write("Выберите задание (1-16): ");
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        Console.Clear();
                        Task01_HelloWorld task = new Task01_HelloWorld();
                        task.Run();
                    }
                    else if (choice == 2)
                    {
                        Console.Clear();
                        Task02_Variables task = new Task02_Variables();
                        task.Run();
                    }
                    else if (choice == 3)
                    {
                        Console.Clear();
                        Task03_Calculator task = new Task03_Calculator();
                        task.Run();
                    }
                    else if (choice == 4)
                    {
                        Console.Clear();
                        Task04_Parity task = new Task04_Parity();
                        task.Run();
                    }
                    else if (choice == 5)
                    {
                        Console.Clear();
                        Task05_MultiplicationTable task = new Task05_MultiplicationTable();
                        task.Run();
                    }
                    else if (choice == 6)
                    {
                        Console.Clear();
                        Task06_DigitSum task = new Task06_DigitSum();
                        task.Run();
                    }
                    else if (choice == 7)
                    {
                        Console.Clear();
                        Task07_StringReverse task = new Task07_StringReverse();
                        task.Run();
                    }
                    else if (choice == 8)
                    {
                        Console.Clear();
                        Task08_FizzBuzz task = new Task08_FizzBuzz();
                        task.Run();
                    }
                    else if (choice == 9)
                    {
                        Console.Clear();
                        Task09_ArrayStats task = new Task09_ArrayStats();
                        task.Run();
                    }
                    else if (choice == 10)
                    {
                        Console.Clear();
                        Task10_SortComparison task = new Task10_SortComparison();
                        task.Run();
                    }
                    else if (choice == 11)
                    {
                        Console.Clear();
                        Task11_MatrixDiagonal task = new Task11_MatrixDiagonal();
                        task.Run();
                    }
                    else if (choice == 12)
                    {
                        Console.Clear();
                        Task12_Palindrome task = new Task12_Palindrome();
                        task.Run();
                    }
                    else if (choice == 13)
                    {
                        Console.Clear();
                        Task13_WordCounter task = new Task13_WordCounter();
                        task.Run();
                    }
                    else if (choice == 14)
                    {
                        Console.Clear();
                        Task14_AgeCalculator task = new Task14_AgeCalculator();
                        task.Run();
                    }
                    else if (choice == 15)
                    {
                        Console.Clear();
                        Task15_GitBasics task = new Task15_GitBasics();
                        task.Run();
                    }
                    else if (choice == 16)
                    {
                        Console.Clear();
                        MenuRunner.Run();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Неверный ввод. Введите от 1 до 16\n");
                    }
                }

                //Cредний уровень
                while (true)
                {

                    if (LevelMenu == 2)
                    {
                        Console.WriteLine("--- Уровень 2: Средний ---");
                        Console.WriteLine("16. Методы расширения для string");
                        Console.WriteLine("17. StringBuildervs конкатенация (замер)");
                        Console.WriteLine("18. TryParsevstry / catch");
                        Console.WriteLine("19. Свой стек / очередь на массиве");
                        Console.WriteLine("20. Обобщения: Swap<T>, Max<T>сIComparable<T>");
                        Console.WriteLine("21. enum - дни недели");
                        Console.WriteLine("22. params-метод + перегрузка");
                        Console.WriteLine("23. Nullable - типы, ??, ?.");
                        Console.WriteLine("24. ООП: Animal → Dog / Cat / Bird");
                        Console.WriteLine("25. Интерфейс IShape+ сортировка фигур");
                        Console.WriteLine("26. Паттерн «Стратегия» (скидки)");
                        Console.WriteLine("27. IComparable / IComparer<T> для Student");
                        Console.WriteLine("28. Equals / GetHashCodeдляPoint");
                        Console.WriteLine("29. Паттерн «Фабрика» (уведомления)");
                        Console.WriteLine("31. IDisposable + using");
                        Console.WriteLine("32. Свойства с валидацией (Temperature)");
                        Console.WriteLine("33. LINQ: сотрудники (top-3, avg по отделу, фильтр)");
                        Console.WriteLine("34. LINQ: группировка слов по первой букве");
                        Console.WriteLine("35. Git: .gitignore, .editorconfig, PR, Conventional Commits, rebase");
                        Console.WriteLine("36. Выход\n");

                        Console.Write("Выберите задание (16-36): ");
                        int choice = int.Parse(Console.ReadLine());
                        if (choice == 16)
                        {
                            Console.Clear();
                            Task16_StringExtensions task = new Task16_StringExtensions();
                            task.Run();
                        }
                        else if (choice == 17)
                        {
                            Console.Clear();

                        }
                        else if (choice == 18)
                        {
                            Console.Clear();

                        }
                        else if (choice == 19)
                        {
                            Console.Clear();

                        }
                        else if (choice == 20)
                        {
                            Console.Clear();

                        }
                        else if (choice == 21)
                        {
                            Console.Clear();

                        }
                        else if (choice == 22)
                        {
                            Console.Clear();

                        }
                        else if (choice == 23)
                        {
                            Console.Clear();

                        }
                        else if (choice == 24)
                        {
                            Console.Clear();

                        }
                        else if (choice == 25)
                        {
                            Console.Clear();

                        }
                        else if (choice == 26)
                        {
                            Console.Clear();

                        }
                        else if (choice == 27)
                        {
                            Console.Clear();

                        }
                        else if (choice == 28)
                        {
                            Console.Clear();

                        }
                        else if (choice == 29)
                        {
                            Console.Clear();

                        }
                        else if (choice == 30)
                        {
                            Console.Clear();

                        }
                        else if (choice == 31)
                        {
                            Console.Clear();

                        }
                        else if (choice == 32)
                        {
                            Console.Clear();

                        }
                        else if (choice == 33)
                        {
                            Console.Clear();

                        }
                        else if (choice == 34)
                        {
                            Console.Clear();

                        }
                        else if (choice == 35)
                        {
                            Console.Clear();

                        }
                        else if (choice == 36)
                        {
                            Console.Clear();
                            MenuRunner.Run();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Неверный ввод. Введите от 16 до 36\n");
                        }
                    }

                }
            }
                
        }
    }
}
