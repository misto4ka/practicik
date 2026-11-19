using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task21_DayOfWeekEnum : ITask
    {
        public string Title => "========================================== Задание 21. enum дни недели ==========================================\n";
        public string Description => "Описание:\r\n Описать дни недели через enum и определить тип дня.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - enum DayOfWeek { Monday, ..., Sunday }.\r\n - Метод IsWeekend(DayOfWeek day) — true для субботы и воскресенья.\r\n - Метод GetRussianName(DayOfWeek day) — возвращает название на русском.\r\n - Продемонстрировать на всех 7 днях.\r\n" };

        public enum DayOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                string ruName = GetRussianName(day);
                string dayType = IsWeekend(day) ? "Выходной" : "Будний день";
                Console.WriteLine($"{day,-10} -> {ruName,-12} | {dayType} (IsWeekend: {IsWeekend(day)})");
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private bool IsWeekend(DayOfWeek day)
        {
            return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
        }

        private string GetRussianName(DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Monday => "Понедельник",
                DayOfWeek.Tuesday => "Вторник",
                DayOfWeek.Wednesday => "Среда",
                DayOfWeek.Thursday => "Четверг",
                DayOfWeek.Friday => "Пятница",
                DayOfWeek.Saturday => "Суббота",
                DayOfWeek.Sunday => "Воскресенье",
                _ => "Неизвестный день"
            };
        }
    }
}