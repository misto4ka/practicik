using System;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task50_GitBisect : ITask
    {
        public string Title => "==================================================\r\nЗадание 50. Git: симуляция bisect (поиск «плохого» коммита)\r\n--------------------------------------------------";
        public string Description => "Описание: найти «плохой» коммит через git bisect. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\n1. Репозиторий с 10+ коммитами.\r\n2. В одном из средних коммитов намеренно «сломать» функцию.\r\n3. Запустить git bisect start.\r\n4. Пометить git bisect bad (текущий) и git bisect good <hash> (первый рабочий).\r\n5. Пройти бисекцию: на каждом шаге git bisect good / git bisect bad.\r\n6. Найти «плохой» коммит, показать вывод.\r\n7. Завершить: git bisect reset.\r\n8. Сохранить вывод в git-notes.md." };

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            Console.WriteLine("Задание выполняется в Git / GitHub (не требует программного кода в консоли).");

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
