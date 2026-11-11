using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task14_AgeCalculator : ITask
    {
        public string Title => "==================================================\n Задание 14. Возраст по дате рождения\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  По дате рождения вычислить точный возраст в годах, месяцах и днях.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Ввод даты через DateTime.TryParse (или TryParseExact).\r\n - Учесть, что день рождения в этом году мог ещё не наступить.\r\n - Вывести «X лет, Y месяцев, Z дней».\r\n - Некорректная дата — сообщение об ошибке, не падать.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");



            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }
    }
}
