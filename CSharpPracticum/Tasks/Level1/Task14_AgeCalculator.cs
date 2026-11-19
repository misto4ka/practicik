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

            Console.Write("Введите дату рождения (например, 25.08.2000): ");
            string input = Console.ReadLine();

            if (!DateTime.TryParse(input, out DateTime birthDate))
            {
                Console.WriteLine("Ошибка! Некорректный формат даты.");
                return;
            }

            DateTime today = DateTime.Today;

            if (birthDate > today)
            {
                Console.WriteLine("Ошибка! Дата рождения не может быть в будущем.");
                return;
            }

            CalculateAndPrintAge(birthDate, today);


            Console.WriteLine("Нажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(1);
        }

        public void CalculateAndPrintAge(DateTime birthDate, DateTime today)
        {
            int years = today.Year - birthDate.Year;
            int months = today.Month - birthDate.Month;
            int days = today.Day - birthDate.Day;

            if (days < 0)
            {
                months--;
                DateTime prevMonth = today.AddMonths(-1);
                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            string yearsText = GetDeclension(years, "год", "года", "лет");
            string monthsText = GetDeclension(months, "месяц", "месяца", "месяцев");
            string daysText = GetDeclension(days, "день", "дня", "дней");

            Console.WriteLine($"Ваш точный возраст: {years} {yearsText}, {months} {monthsText}, {days} {daysText}.");
        }

        public string GetDeclension(int number, string form1, string form2, string form5)
        {
            int n = Math.Abs(number) % 100;
            if (n >= 11 && n <= 19)
            {
                return form5;
            }

            int lastDigit = n % 10;
            if (lastDigit == 1)
            {
                return form1;
            }
            if (lastDigit >= 2 && lastDigit <= 4)
            {
                return form2;
            }
            return form5;
        }

    }
}
