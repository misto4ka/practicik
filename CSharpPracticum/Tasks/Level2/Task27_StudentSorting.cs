using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task27_StudentSorting : ITask
    {
        public string Title => "================================= Задание 27. IComparable/IComparer<T> для Student ==================================\n";
        public string Description => "Описание:\r\n Научить класс Student сортироваться по среднему баллу и по имени.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Класс Student: Name, AverageGrade.\r\n - Реализовать IComparable<Student> — сортировка по среднему баллу (по убыванию).\r\n - Реализовать IComparer<Student> ByNameComparer — сортировка по имени.\r\n - Продемонстрировать обе сортировки на List<Student>.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            var students = new List<Student>
        {
            new Student("Даниил", 4.2),
            new Student("Анна", 4.9),
            new Student("Максим", 3.8),
            new Student("Олег", 4.6)
        };

            Console.WriteLine("--- Исходный список ---");
            PrintStudents(students);

            students.Sort();
            Console.WriteLine("\n--- Сортировка по умолчанию (IComparable: средний балл по убыванию) ---");
            PrintStudents(students);

            students.Sort(new ByNameComparer());
            Console.WriteLine("\n--- Сортировка через ByNameComparer (по имени А-Я) ---");
            PrintStudents(students);

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private void PrintStudents(List<Student> students)
        {
            foreach (var s in students)
            {
                Console.WriteLine($"  {s.Name,-10} | Средний балл: {s.AverageGrade:F2}");
            }
        }

        private class Student : IComparable<Student>
        {
            public string Name { get; }
            public double AverageGrade { get; }

            public Student(string name, double averageGrade)
            {
                Name = name;
                AverageGrade = averageGrade;
            }

            public int CompareTo(Student? other)
            {
                if (other == null) return 1;
                return other.AverageGrade.CompareTo(AverageGrade);
            }
        }

        private class ByNameComparer : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                return string.Compare(x.Name, y.Name, StringComparison.CurrentCulture);
            }
        }
    }
}