using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task25_ShapeSorting : ITask
    {
        public string Title => "================================== Задание 25. Интерфейс IShape + сортировка фигур ==================================\n";
        public string Description => "Описание:\r\n Реализовать интерфейс фигур IShape и отсортировать их по площади.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - IShape с методами Area() и Perimeter().\r\n - Классы Circle, Rectangle, Triangle — реализуют IShape.\r\n - Создать List<IShape> из 5+ фигур.\r\n - Отсортировать по площади (через List.Sort с лямбдой или OrderBy).\r\n - Вывести фигуры с площадями до и после сортировки.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            List<IShape> shapes = new List<IShape>
        {
            new Rectangle(5, 4),       // S = 20
            new Circle(2),             // S +-= 12.57
            new Triangle(3, 4, 5),     // S = 6
            new Circle(4),             // S +-= 50.27
            new Rectangle(2, 3),       // S = 6
            new Triangle(6, 8, 10)     // S = 24
        };

            Console.WriteLine("--- Фигуры до сортировки ---");
            PrintShapes(shapes);

            shapes.Sort((a, b) => a.Area().CompareTo(b.Area()));

            Console.WriteLine("\n--- Фигуры после сортировки по возрастанию площади ---");
            PrintShapes(shapes);

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private void PrintShapes(List<IShape> shapes)
        {
            foreach (var shape in shapes)
            {
                Console.WriteLine($"{shape,-28} | Площадь: {shape.Area(),6:F2} | Периметр: {shape.Perimeter(),6:F2}");
            }
        }

        private interface IShape
        {
            double Area();
            double Perimeter();
        }

        private class Circle : IShape
        {
            public double Radius { get; }
            public Circle(double radius) => Radius = radius;

            public double Area() => Math.PI * Radius * Radius;
            public double Perimeter() => 2 * Math.PI * Radius;
            public override string ToString() => $"Круг (R={Radius})";
        }

        private class Rectangle : IShape
        {
            public double Width { get; }
            public double Height { get; }
            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public double Area() => Width * Height;
            public double Perimeter() => 2 * (Width + Height);
            public override string ToString() => $"Прямоугольник ({Width}x{Height})";
        }

        private class Triangle : IShape
        {
            public double A { get; }
            public double B { get; }
            public double C { get; }

            public Triangle(double a, double b, double c)
            {
                A = a;
                B = b;
                C = c;
            }

            public double Area()
            {
                double p = Perimeter() / 2.0;
                return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
            }

            public double Perimeter() => A + B + C;
            public override string ToString() => $"Треугольник ({A}, {B}, {C})";
        }
    }
}