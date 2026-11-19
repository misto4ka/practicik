using System;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task46_GitStash : ITask
    {
        public string Title => "==================================================\r\nЗадание 46. Git: git stash / stash pop\r\n--------------------------------------------------";
        public string Description => "Описание: продемонстрировать работу со stash. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\n1. Внести незакоммиченные изменения в файл.\r\n2. Спрятать их: git stash.\r\n3. Проверить git stash list — изменения в списке.\r\n4. Переключиться на другую ветку, вернуться.\r\n5. Достать изменения: git stash pop.\r\n6. Сохранить вывод git stash list до и после в git-notes.md." };

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
