using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task26_DiscountStrategy : ITask
    {
        public string Title => "==================================== Задание 26. Паттерн «Стратегия» (скидки) =====================================\n";
        public string Description => "Описание:\r\n Реализовать калькулятор скидок через паттерн «Стратегия».\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Интерфейс IDiscountStrategy с методом Apply(decimal price).\r\n - Реализации: NoDiscount, VipDiscount (10%), PromoDiscount (20%).\r\n - Класс PriceCalculator, принимающий стратегию в конструкторе.\r\n - Продемонстрировать расчёт цены с каждой стратегией.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            decimal originalPrice = 1000m;
            Console.WriteLine($"Исходная цена товара: {originalPrice:C}\n");

            var noDiscountCalc = new PriceCalculator(new NoDiscount());
            var vipDiscountCalc = new PriceCalculator(new VipDiscount());
            var promoDiscountCalc = new PriceCalculator(new PromoDiscount());

            Console.WriteLine($"Без скидки (NoDiscount):      {noDiscountCalc.Calculate(originalPrice):C}");
            Console.WriteLine($"VIP-клиент (VipDiscount 10%): {vipDiscountCalc.Calculate(originalPrice):C}");
            Console.WriteLine($"Промокод (PromoDiscount 20%): {promoDiscountCalc.Calculate(originalPrice):C}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private interface IDiscountStrategy
        {
            decimal Apply(decimal price);
        }

        private class NoDiscount : IDiscountStrategy
        {
            public decimal Apply(decimal price) => price;
        }

        private class VipDiscount : IDiscountStrategy
        {
            public decimal Apply(decimal price) => price * 0.90m;
        }

        private class PromoDiscount : IDiscountStrategy
        {
            public decimal Apply(decimal price) => price * 0.80m;
        }

        private class PriceCalculator
        {
            private readonly IDiscountStrategy _strategy;

            public PriceCalculator(IDiscountStrategy strategy)
            {
                _strategy = strategy;
            }

            public decimal Calculate(decimal price) => _strategy.Apply(price);
        }
    }
}