using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task15_GitBasics : ITask
    {
        public string Title => "==================================================\n Задание 15. Git: репозиторий → ветка → merge → разрешение конфликта\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Пройти базовый git-сценарий.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - git init — создать локальный репозиторий.\r\n - Сделать 2–3 коммита с осмысленными сообщениями.\r\n - Создать ветку feature/git-basics, внести изменение, закоммитить.\r\n - Вернуться в main, влить ветку: git merge feature/git-basics.\r\n - Смоделировать конфликт: в двух ветках изменить одну и ту же строку README.md, сделать merge, вручную разрешить конфликт (убрать маркеры<<<<<<<).\r\n - Сохранить вывод git log --graph --oneline --all в git-notes.md.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            Console.WriteLine("Задание выполняется в Git / GitHub (не требует программного кода в консоли).");

            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
