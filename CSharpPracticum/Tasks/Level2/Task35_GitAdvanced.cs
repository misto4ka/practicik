using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task35_GitAdvanced : ITask
    {
        public string Title => "======================= Задание 35. Git: .gitignore, .editorconfig, PR, Conventional Commits, rebase =======================\n";
        public string Description => "Описание:\r\n Пройти продвинутый git-сценарий (настройка репозитория, ветки, PR и rebase).\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Добавить .gitignore (bin/, obj/, .vs/, *.user) и .editorconfig отдельным коммитом chore: add gitignore and editorconfig.\r\n - Создать ветку feature/task-35, сделать 2-3 коммита по Conventional Commits.\r\n - Запушить ветку на GitHub/GitLab.\r\n - Открыть Pull Request в main с описанием: что сделано, что проверял.\r\n - Смержить PR (можно squash-merge).\r\n - Создать ветку feature/rebase-demo.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            Console.WriteLine("Задание выполняется в Git / GitHub (не требует программного кода в консоли).");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }
    }
}