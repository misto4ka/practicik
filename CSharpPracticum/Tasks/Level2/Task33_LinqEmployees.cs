using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task33_LinqEmployees : ITask
    {
        public string Title => "============================= Задание 33. LINQ: сотрудники (top-3, avg по отделу, фильтр) =============================\n";
        public string Description => "Описание:\r\n Выполнить LINQ-запросы по списку сотрудников: топ-3 по зарплате, средняя по отделам, старше 30 лет.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Класс Employee: Name, Age, Salary, Department.\r\n - Список из 10+ сотрудников.\r\n - Запросы через LINQ: топ-3 по зарплате (убывание), средняя зарплата по каждому отделу (GroupBy + Average), сотрудники старше 30 (Where).\r\n - Вывести результаты.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            var employees = new List<Employee>
        {
            new Employee("Алексей", 28, 120000m, "IT"),
            new Employee("Марина", 35, 185000m, "IT"),
            new Employee("Игорь", 42, 210000m, "Management"),
            new Employee("Ольга", 24, 75000m, "HR"),
            new Employee("Дмитрий", 31, 145000m, "IT"),
            new Employee("Елена", 38, 130000m, "Marketing"),
            new Employee("Сергей", 29, 95000m, "Marketing"),
            new Employee("Наталья", 33, 110000m, "HR"),
            new Employee("Артём", 26, 105000m, "IT"),
            new Employee("Виктория", 45, 240000m, "Management"),
            new Employee("Павел", 34, 115000m, "Marketing")
        };

            var top3Salary = employees
                .OrderByDescending(e => e.Salary)
                .Take(3);

            Console.WriteLine("--- 1. Топ-3 сотрудника по зарплате ---");
            foreach (var emp in top3Salary)
            {
                Console.WriteLine($"  {emp.Name,-10} | Отдел: {emp.Department,-10} | Зарплата: {emp.Salary:C}");
            }

            var avgByDept = employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    AvgSalary = g.Average(e => e.Salary)
                });

            Console.WriteLine("\n--- 2. Средняя зарплата по отделам ---");
            foreach (var dept in avgByDept)
            {
                Console.WriteLine($"  Отдел: {dept.Department,-12} | Средняя ЗП: {dept.AvgSalary:C}");
            }

            var olderThan30 = employees
                .Where(e => e.Age > 30);

            Console.WriteLine("\n--- 3. Сотрудники старше 30 лет ---");
            foreach (var emp in olderThan30)
            {
                Console.WriteLine($"  {emp.Name,-10} | Возраст: {emp.Age} | Отдел: {emp.Department}");
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private class Employee
        {
            public string Name { get; }
            public int Age { get; }
            public decimal Salary { get; }
            public string Department { get; }

            public Employee(string name, int age, decimal salary, string department)
            {
                Name = name;
                Age = age;
                Salary = salary;
                Department = department;
            }
        }
    }
}