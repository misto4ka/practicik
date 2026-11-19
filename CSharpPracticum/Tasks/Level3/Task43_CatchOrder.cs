using System;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }

    public class Task43_CatchOrder : ITask
    {
        public string Title => "==================================================\r\nЗадание 43. Множественные типы исключений, порядок catch\r\n--------------------------------------------------";
        public string Description => "Описание: продемонстрировать порядок обработки исключений. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nМетод, который может бросить FormatException, DivideByZeroException или свой CustomException.\r\nНесколько catch-блоков в правильном порядке (от частного к общему).\r\nПродемонстрировать все три сценария.\r\nВ конце — finally с выводом «завершено».\r\nОбъяснить в комментарии, почему порядок catch важен." };

        private static void Process(int scenario)
        {
            switch (scenario)
            {
                case 1:
                    int number = int.Parse("не число");
                    Console.WriteLine(number);
                    break;
                case 2:
                    int zero = 0;
                    Console.WriteLine(10 / zero);
                    break;
                case 3:
                    throw new CustomException("Сработало собственное исключение.");
                default:
                    Console.WriteLine("Ошибок нет.");
                    break;
            }
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            for (int scenario = 1; scenario <= 3; scenario++)
            {
                Console.WriteLine($"Сценарий {scenario}:");

                // Блоки catch проверяются сверху вниз, срабатывает первый подходящий по типу.
                // Поэтому сначала идут частные исключения, а общий Exception — в самом конце.
                // Если поставить catch (Exception) первым, он перехватит всё, а блоки ниже
                // станут недостижимыми (компилятор выдаст ошибку CS0160).
                try
                {
                    Process(scenario);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"  FormatException: {ex.Message}");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"  DivideByZeroException: {ex.Message}");
                }
                catch (CustomException ex)
                {
                    Console.WriteLine($"  CustomException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Общее исключение: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("  завершено");
                }
            }

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
