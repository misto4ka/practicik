using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task42_CancellationToken : ITask
    {
        public string Title => "==================================================\r\nЗадание 42. CancellationToken — отмена через 2 сек\r\n--------------------------------------------------";
        public string Description => "Описание: реализовать долгую операцию с поддержкой отмены. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nasync Task LongOperationAsync(CancellationToken token) — цикл с Task.Delay(500, token) 10 раз.\r\nСоздать CancellationTokenSource, запустить операцию, отменить через 2 секунды.\r\nОбработать OperationCanceledException.\r\nВывести «операция отменена»." };

        private static async Task LongOperationAsync(CancellationToken token)
        {
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(500, token);
                Console.WriteLine($"  Шаг {i} из 10 выполнен");
            }

            Console.WriteLine("  Операция завершена полностью.");
        }

        private static async Task RunAsync()
        {
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                cts.CancelAfter(TimeSpan.FromSeconds(2));

                try
                {
                    Console.WriteLine("Операция запущена, отмена через 2 секунды...");
                    await LongOperationAsync(cts.Token);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("операция отменена");
                }
            }

            Console.WriteLine("Приложение продолжает работать.");
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            RunAsync().GetAwaiter().GetResult();

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
