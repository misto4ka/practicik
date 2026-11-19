using CSharpPracticum.Common;
using CSharpPracticum.Tasks.Level1;
using CSharpPracticum.Tasks.Level2;
using CSharpPracticum.Tasks.Level3;
using System;
using System.Collections.Generic;
using System.Text;

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
                            Task17_StringBuilderBenchmark task = new Task17_StringBuilderBenchmark();
                            task.Run();

                        }
                        else if (choice == 18)
                        {
                            Console.Clear();
                            Task18_TryParseVsTryCatch task = new Task18_TryParseVsTryCatch();
                            task.Run();

                        }
                        else if (choice == 19)
                        {
                            Console.Clear();
                            Task19_CustomStackAndQueue task = new Task19_CustomStackAndQueue();
                            task.Run();
                        }
                        else if (choice == 20)
                        {
                            Console.Clear();
                            Task20_GenericsSwapMax task = new Task20_GenericsSwapMax();
                            task.Run();

                        }
                        else if (choice == 21)
                        {
                            Console.Clear();
                            Task21_DayOfWeekEnum task = new Task21_DayOfWeekEnum();
                            task.Run();

                        }
                        else if (choice == 22)
                        {
                            Console.Clear();
                            Task22_ParamsAndOverload task = new Task22_ParamsAndOverload();
                            task.Run();
                        }
                        else if (choice == 23)
                        {
                            Console.Clear();
                            Task23_NullableOperators task = new Task23_NullableOperators();
                            task.Run();
                        }
                        else if (choice == 24)
                        {
                            Console.Clear();
                            Task24_AnimalHierarchy task = new Task24_AnimalHierarchy();
                            task.Run();
                        }
                        else if (choice == 25)
                        {
                            Console.Clear();
                            Task25_ShapeSorting task = new Task25_ShapeSorting();
                            task.Run();
                        }
                        else if (choice == 26)
                        {
                            Console.Clear();
                            Task26_DiscountStrategy task = new Task26_DiscountStrategy();
                            task.Run();
                        }
                        else if (choice == 27)
                        {
                            Console.Clear();
                            Task27_StudentSorting task = new Task27_StudentSorting();
                            task.Run();
                        }
                        else if (choice == 28)
                        {
                            Console.Clear();
                            Task28_PointEquality task = new Task28_PointEquality();
                            task.Run();
                        }
                        else if (choice == 29)
                        {
                            Console.Clear();
                            Task29_NotificationFactory task = new Task29_NotificationFactory();
                            task.Run();
                        }
                        else if (choice == 30)
                        {
                            Console.Clear();
                            Task30_BankAccountException task = new Task30_BankAccountException();
                            task.Run();
                        }
                        else if (choice == 31)
                        {
                            Console.Clear();
                            Task31_FileWriterDisposable task = new Task31_FileWriterDisposable();
                            task.Run();
                        }
                        else if (choice == 32)
                        {
                            Console.Clear();
                            Task32_TemperatureValidation task = new Task32_TemperatureValidation();
                            task.Run();
                        }
                        else if (choice == 33)
                        {
                            Console.Clear();
                            Task33_LinqEmployees task = new Task33_LinqEmployees();
                            task.Run();
                        }
                        else if (choice == 34)
                        {
                            Console.Clear();
                            Task34_LinqWordGrouping task = new Task34_LinqWordGrouping();
                            task.Run();
                        }
                        else if (choice == 35)
                        {
                            Console.Clear();
                            Task35_GitAdvanced task = new Task35_GitAdvanced();
                            task.Run();
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

                    //Продвинутый уровень
                    while (true)
                    {
                        if (LevelMenu == 3)
                        {
                            Console.WriteLine("--- Уровень 3: Продвинутый ---");
                            Console.WriteLine("36. Поиск дубликатов (Duplicates)");
                            Console.WriteLine("37. Числа Фибоначчи с мемоизацией (FibonacciMemo)");
                            Console.WriteLine("38. List vs LinkedList (замер)");
                            Console.WriteLine("39. Генератор чётных чисел (YieldEven)");
                            Console.WriteLine("40. Асинхронность: async / await");
                            Console.WriteLine("41. Параллельное выполнение: Task.WhenAll");
                            Console.WriteLine("42. Отмена задач: CancellationToken");
                            Console.WriteLine("43. Порядок блоков catch (CatchOrder)");
                            Console.WriteLine("44. Консольное приложение: TodoList");
                            Console.WriteLine("45. Консольное приложение: Library");
                            Console.WriteLine("46. Git: git stash");
                            Console.WriteLine("47. Git: git squash");
                            Console.WriteLine("48. Git: теги (git tags)");
                            Console.WriteLine("49. Git: git reflog");
                            Console.WriteLine("50. Git: git bisect");
                            Console.WriteLine("51. Выход\n");

                            Console.Write("Выберите задание (36-51): ");
                            int choice = int.Parse(Console.ReadLine());

                            if (choice == 36)
                            {
                                Console.Clear();
                                Task36_Duplicates task = new Task36_Duplicates();
                                task.Run();
                            }
                            else if (choice == 37)
                            {
                                Console.Clear();
                                Task37_FibonacciMemo task = new Task37_FibonacciMemo();
                                task.Run();
                            }
                            else if (choice == 38)
                            {
                                Console.Clear();
                                Task38_ListVsLinkedList task = new Task38_ListVsLinkedList();
                                task.Run();
                            }
                            else if (choice == 39)
                            {
                                Console.Clear();
                                Task39_YieldEven task = new Task39_YieldEven();
                                task.Run();
                            }
                            else if (choice == 40)
                            {
                                Console.Clear();
                                Task40_AsyncAwait task = new Task40_AsyncAwait();
                                task.Run();
                            }
                            else if (choice == 41)
                            {
                                Console.Clear();
                                Task41_WhenAll task = new Task41_WhenAll();
                                task.Run();
                            }
                            else if (choice == 42)
                            {
                                Console.Clear();
                                Task42_CancellationToken task = new Task42_CancellationToken();
                                task.Run();
                            }
                            else if (choice == 43)
                            {
                                Console.Clear();
                                Task43_CatchOrder task = new Task43_CatchOrder();
                                task.Run();
                            }
                            else if (choice == 44)
                            {
                                Console.Clear();
                                Task44_TodoList task = new Task44_TodoList();
                                task.Run();
                            }
                            else if (choice == 45)
                            {
                                Console.Clear();
                                Task45_Library task = new Task45_Library();
                                task.Run();
                            }
                            else if (choice == 46)
                            {
                                Console.Clear();
                                Task46_GitStash task = new Task46_GitStash();
                                task.Run();
                            }
                            else if (choice == 47)
                            {
                                Console.Clear();
                                Task47_GitSquash task = new Task47_GitSquash();
                                task.Run();
                            }
                            else if (choice == 48)
                            {
                                Console.Clear();
                                Task48_GitTags task = new Task48_GitTags();
                                task.Run();
                            }
                            else if (choice == 49)
                            {
                                Console.Clear();
                                Task49_GitReflog task = new Task49_GitReflog();
                                task.Run();
                            }
                            else if (choice == 50)
                            {
                                Console.Clear();
                                Task50_GitBisect task = new Task50_GitBisect();
                                task.Run();
                            }
                            else if (choice == 51)
                            {
                                Console.Clear();
                                MenuRunner.Run();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Неверный ввод. Введите от 36 до 51\n");
                            }
                        }
                    }
                }
            }       
        }
    }
}
