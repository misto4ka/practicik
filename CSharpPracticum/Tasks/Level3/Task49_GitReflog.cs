using System;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task49_GitReflog : ITask
    {
        public string Title => "==================================================\r\nЗадание 49. Git: восстановление через reflog\r\n--------------------------------------------------";
        public string Description => "Описание: удалить коммит и восстановить его. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\n1. Сделать коммит.\r\n2. Удалить его: git reset --hard HEAD~1.\r\n3. Найти хеш через git reflog.\r\n4. Восстановить: git reset --hard <hash>.\r\n5. Показать git reflog и git log --oneline.\r\n6. Сохранить вывод в git-notes.md." };

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
