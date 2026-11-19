using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task24_AnimalHierarchy : ITask
    {
        public string Title => "==================================== Задание 24. ООП: Animal -> Dog/Cat/Bird ====================================\n";
        public string Description => "Описание:\r\n Построить иерархию животных с абстрактным базовым классом и полиморфизмом.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Абстрактный класс Animal: свойство Name, абстрактный метод MakeSound(), переопределённый ToString().\r\n - Классы Dog, Cat, Bird — наследники, реализуют MakeSound().\r\n - Bird дополнительно имеет метод Fly().\r\n - Продемонстрировать полиморфизм: List<Animal> с разными животными, вызов MakeSound() в цикле.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            List<Animal> animals = new List<Animal>
        {
            new Dog("Бобик"),
            new Cat("Мурка"),
            new Bird("Кеша")
        };

            foreach (Animal animal in animals)
            {
                Console.Write($"{animal} -> говорит: ");
                animal.MakeSound();

                if (animal is Bird bird)
                {
                    bird.Fly();
                }
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private abstract class Animal
        {
            public string Name { get; }

            protected Animal(string name)
            {
                Name = name;
            }

            public abstract void MakeSound();

            public override string ToString()
            {
                return $"[{GetType().Name}] Имя: {Name}";
            }
        }

        private class Dog : Animal
        {
            public Dog(string name) : base(name) { }
            public override void MakeSound() => Console.WriteLine("Гав-гав!");
        }

        private class Cat : Animal
        {
            public Cat(string name) : base(name) { }
            public override void MakeSound() => Console.WriteLine("Мяу!");
        }

        private class Bird : Animal
        {
            public Bird(string name) : base(name) { }
            public override void MakeSound() => Console.WriteLine("Чирик-чирик!");
            public void Fly() => Console.WriteLine($"   -> {Name} взлетает в небо!");
        }
    }
}