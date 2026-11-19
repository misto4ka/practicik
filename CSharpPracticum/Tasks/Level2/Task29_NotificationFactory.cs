using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task29_NotificationFactory : ITask
    {
        public string Title => "=================================== Задание 29. Паттерн «Фабрика» (уведомления) ===================================\n";
        public string Description => "Описание:\r\n Реализовать фабрику уведомлений для Email, SMS и Push.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Интерфейс INotification с методом Send(string message).\r\n - Реализации: EmailNotification, SmsNotification, PushNotification.\r\n - NotificationFactory.Create(NotificationType type) — возвращает нужную реализацию.\r\n - enum NotificationType { Email, Sms, Push }.\r\n - Продемонстрировать создание и отправку через фабрику.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements);
            Console.WriteLine("=================================================================================================================\n");

            string text = "Ваш заказ №405 готов к выдаче!";

            foreach (NotificationType type in Enum.GetValues(typeof(NotificationType)))
            {
                INotification notification = NotificationFactory.Create(type);
                Console.Write($"Тип {type,-5} ({notification.GetType().Name,-17}) -> ");
                notification.Send(text);
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private enum NotificationType
        {
            Email,
            Sms,
            Push
        }

        private interface INotification
        {
            void Send(string message);
        }

        private class EmailNotification : INotification
        {
            public void Send(string message) => Console.WriteLine($"[Email] Отправлено письмо: \"{message}\"");
        }

        private class SmsNotification : INotification
        {
            public void Send(string message) => Console.WriteLine($"[SMS] Отправлено СМС: \"{message}\"");
        }

        private class PushNotification : INotification
        {
            public void Send(string message) => Console.WriteLine($"[Push] Всплывающее уведомление: \"{message}\"");
        }

        private static class NotificationFactory
        {
            public static INotification Create(NotificationType type)
            {
                return type switch
                {
                    NotificationType.Email => new EmailNotification(),
                    NotificationType.Sms => new SmsNotification(),
                    NotificationType.Push => new PushNotification(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type), "Неизвестный тип уведомления")
                };
            }
        }
    }
}