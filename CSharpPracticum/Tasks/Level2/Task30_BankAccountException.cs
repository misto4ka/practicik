using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task30_BankAccountException : ITask
    {
        public string Title => "==================================== Задание 30. BankAccount + свой Exception =====================================\n";
        public string Description => "Описание:\r\n Реализовать банковский счёт с собственным исключением при нехватке средств.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Класс BankAccount с приватным балансом.\r\n - Методы Deposit(decimal), Withdraw(decimal).\r\n - Свой класс InsufficientFundsException : Exception.\r\n - Withdraw бросает исключение, если сумма больше баланса.\r\n - Продемонстрировать: успешное снятие и попытку снять больше баланса (с try/catch).\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            var account = new BankAccount(1000m);
            Console.WriteLine($"Открыт счёт. Начальный баланс: {account.Balance:C}");

            account.Deposit(500m);
            Console.WriteLine($"Пополнение на 500 -> Баланс: {account.Balance:C}");

            account.Withdraw(300m);
            Console.WriteLine($"Успешное снятие 300 -> Баланс: {account.Balance:C}\n");

            Console.WriteLine("Попытка снять 2000 (больше текущего баланса)...");
            try
            {
                account.Withdraw(2000m);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Перехвачено InsufficientFundsException: {ex.Message}");
            }

            Console.WriteLine($"Итоговый баланс счёта: {account.Balance:C}");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private class InsufficientFundsException : Exception
        {
            public decimal CurrentBalance { get; }
            public decimal AttemptedAmount { get; }

            public InsufficientFundsException(decimal currentBalance, decimal attemptedAmount)
                : base($"Недостаточно средств! Баланс: {currentBalance:C}, попытка снять: {attemptedAmount:C}.")
            {
                CurrentBalance = currentBalance;
                AttemptedAmount = attemptedAmount;
            }
        }

        private class BankAccount
        {
            private decimal _balance;

            public decimal Balance => _balance;

            public BankAccount(decimal initialBalance = 0m)
            {
                if (initialBalance < 0)
                    throw new ArgumentException("Начальный баланс не может быть отрицательным.");
                _balance = initialBalance;
            }

            public void Deposit(decimal amount)
            {
                if (amount <= 0)
                    throw new ArgumentException("Сумма пополнения должна быть больше нуля.");
                _balance += amount;
            }

            public void Withdraw(decimal amount)
            {
                if (amount <= 0)
                    throw new ArgumentException("Сумма снятия должна быть больше нуля.");

                if (amount > _balance)
                    throw new InsufficientFundsException(_balance, amount);

                _balance -= amount;
            }
        }
    }
}