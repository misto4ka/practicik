using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task32_TemperatureValidation : ITask
    {
        public string Title => "================================== Задание 32. Свойства с валидацией (Temperature) ==================================\n";
        public string Description => "Описание:\r\n Реализовать класс Temperature с валидацией температуры в сеттере и вычисляемым свойством Fahrenheit.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Свойство Celsius с проверкой в set: не ниже -273.15.\r\n - При некорректном значении — бросать ArgumentOutOfRangeException.\r\n - Вычисляемое свойство Fahrenheit (только get).\r\n - Продемонстрировать корректное и некорректное присваивание.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            var temp = new Temperature(25.0);
            Console.WriteLine($"Начальная температура: {temp.Celsius:F2} °C = {temp.Fahrenheit:F2} °F");

            temp.Celsius = -40.0;
            Console.WriteLine($"После присваивания -40: {temp.Celsius:F2} °C = {temp.Fahrenheit:F2} °F\n");

            Console.WriteLine("Попытка присвоить -300 °C (ниже абсолютного нуля)...");
            try
            {
                temp.Celsius = -300.0;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Перехвачено ArgumentOutOfRangeException: {ex.Message}");
            }

            Console.WriteLine($"Текущее значение осталось корректным: {temp.Celsius:F2} °C");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private class Temperature
        {
            private double _celsius;

            public double Celsius
            {
                get => _celsius;
                set
                {
                    if (value < -273.15)
                        throw new ArgumentOutOfRangeException(nameof(value), "Температура не может быть ниже абсолютного нуля (-273.15 °C).");
                    _celsius = value;
                }
            }

            public double Fahrenheit => _celsius * 9.0 / 5.0 + 32.0;

            public Temperature(double celsius)
            {
                Celsius = celsius;
            }
        }
    }
}