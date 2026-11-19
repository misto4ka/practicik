using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task23_NullableOperators : ITask
    {
        public string Title => "======================================== Задание 23. Nullable-типы, ??, ?. ========================================\n";
        public string Description => "Описание:\r\n Продемонстрировать работу с nullable-типами и операторами ??, ?., HasValue/Value.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Метод int? FindValue(string key) — возвращает null, если ключа нет.\r\n - Использовать ?? для значения по умолчанию.\r\n - Использовать ?. для безопасного доступа к свойству объекта, который может быть null.\r\n - Использовать HasValue/Value с проверкой.\r\n - Продемонстрировать на примере с найденным и ненайденным ключом.\r\n" };

        private class UserProfile
        {
            public string? Nickname { get; set; }
        }

        private readonly Dictionary<string, int> _storage = new()
    {
        { "apples", 15 },
        { "bananas", 42 }
    };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            string[] keysToSearch = { "apples", "oranges" };

            foreach (string key in keysToSearch)
            {
                Console.WriteLine($"--- Поиск ключа: \"{key}\" ---");
                int? found = FindValue(key);

                if (found.HasValue)
                    Console.WriteLine($"  HasValue == true -> Value = {found.Value}");
                else
                    Console.WriteLine("  HasValue == false -> значение отсутствует (null)");

                int finalValue = found ?? -1;
                Console.WriteLine($"  Результат с оператором (?? -1): {finalValue}\n");
            }

            UserProfile? existingUser = new UserProfile { Nickname = "Coder2026" };
            UserProfile? nullUser = null;

            Console.WriteLine("--- Безопасный доступ через ?. ---");
            Console.WriteLine($"  existingUser?.Nickname: {existingUser?.Nickname ?? "Гость"}");
            Console.WriteLine($"  nullUser?.Nickname:     {nullUser?.Nickname ?? "Гость (объект был null)"}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private int? FindValue(string key)
        {
            if (_storage.TryGetValue(key, out int value))
                return value;

            return null;
        }
    }
}