using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task18_TryParseVsTryCatch : ITask
    {
        public string Title => "====================================== Задание 18. TryParse vs try/catch =======================================\n";
        public string Description => "Описание:\r\n Сравнить два способа обработки некорректного ввода числа.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Способ 1: int.TryParse — вернуть bool + значение через out.\r\n - Способ 2: int.Parse внутри try/catch — ловить FormatException.\r\n - Оба оформить отдельными методами.\r\n - Продемонстрировать на корректном и некорректном вводе.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            string[] inputs = { "123", "abc" };

            foreach (string input in inputs)
            {
                Console.WriteLine($"Тестируем ввод: \"{input}\"");

                if (ParseWithTryParse(input, out int res1))
                    Console.WriteLine($"  [TryParse]  Успех -> {res1}");
                else
                    Console.WriteLine($"  [TryParse]  Ошибка преобразования (возвращено {res1})");

                if (ParseWithTryCatch(input, out int res2))
                    Console.WriteLine($"  [try/catch] Успех -> {res2}\n");
                else
                    Console.WriteLine($"  [try/catch] Ошибка преобразования (возвращено {res2})\n");
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private bool ParseWithTryParse(string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        private bool ParseWithTryCatch(string input, out int result)
        {
            try
            {
                result = int.Parse(input);
                return true;
            }
            catch (FormatException)
            {
                result = 0;
                return false;
            }
        }
    }
}