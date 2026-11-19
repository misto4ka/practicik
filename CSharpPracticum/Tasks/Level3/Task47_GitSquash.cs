using System;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task47_GitSquash : ITask
    {
        public string Title => "==================================================\r\nЗадание 47. Git: rebase -i (сквош коммитов)\r\n--------------------------------------------------";
        public string Description => "Описание: объединить несколько коммитов в один. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\n1. Создать ветку feature/squash-demo.\r\n2. Сделать 4 маленьких коммита.\r\n3. Выполнить git rebase -i HEAD~4, объединить в один (squash).\r\n4. Показать git log --oneline до и после.\r\n5. Сохранить оба вывода в git-notes.md." };

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
