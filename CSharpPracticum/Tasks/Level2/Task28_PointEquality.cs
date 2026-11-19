using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task28_PointEquality : ITask
    {
        public string Title => "==================================== Задание 28. Equals/GetHashCode для Point =====================================\n";
        public string Description => "Описание:\r\n Переопределить Equals и GetHashCode и реализовать IEquatable<Point> для класса Point(x, y).\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Переопределить Equals(object) и GetHashCode().\r\n - Реализовать IEquatable<Point>.\r\n - Проверить: две точки с одинаковыми координатами равны.\r\n - Проверить в HashSet<Point>: добавление дубликата не увеличивает Count.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            var p1 = new Point(3, 7);
            var p2 = new Point(3, 7);
            var p3 = new Point(5, 1);

            Console.WriteLine($"p1 = {p1}, p2 = {p2}, p3 = {p3}");
            Console.WriteLine($"ReferenceEquals(p1, p2): {ReferenceEquals(p1, p2)} (разные объекты в памяти)");
            Console.WriteLine($"p1.Equals(p2):           {p1.Equals(p2)}");
            Console.WriteLine($"p1.Equals(p3):           {p1.Equals(p3)}");
            Console.WriteLine($"p1.GetHashCode() == p2.GetHashCode(): {p1.GetHashCode() == p2.GetHashCode()}\n");

            var pointsSet = new HashSet<Point>();
            pointsSet.Add(p1);
            Console.WriteLine($"Добавили p1 -> HashSet.Count = {pointsSet.Count}");

            bool addedDuplicate = pointsSet.Add(p2);
            Console.WriteLine($"Пытаемся добавить дубликат p2 (результат: {addedDuplicate}) -> HashSet.Count = {pointsSet.Count}");

            pointsSet.Add(p3);
            Console.WriteLine($"Добавили p3 -> HashSet.Count = {pointsSet.Count}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private class Point : IEquatable<Point>
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Equals(Point? other)
            {
                if (other is null) return false;
                if (ReferenceEquals(this, other)) return true;
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object? obj) => Equals(obj as Point);

            public override int GetHashCode() => HashCode.Combine(X, Y);

            public override string ToString() => $"({X}, {Y})";
        }
    }
}