using System;
using System.Threading.Tasks;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task40_AsyncAwait : ITask
    {
        public string Title => "==================================================\r\nЗадание 40. async/await + обработка исключений\r\n--------------------------------------------------";
        public string Description => "Описание: асинхронный метод с эмуляцией «скачивания». ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nasync Task<string> DownloadAsync(string url) — внутри Task.Delay(1000).\r\nИмитировать ошибку: если url пустой — бросить ArgumentException.\r\nВызвать через await внутри try/catch.\r\nПродемонстрировать успешный и ошибочный вызов." };

        private static async Task<string> DownloadAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL не может быть пустым.", nameof(url));

            await Task.Delay(1000);
            return $"Содержимое страницы {url} (загружено)";
        }

        private static async Task RunAsync()
        {
            try
            {
                Console.WriteLine("Скачиваем https://example.com ...");
                string result = await DownloadAsync("https://example.com");
                Console.WriteLine($"Успех: {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            try
            {
                Console.WriteLine();
                Console.WriteLine("Скачиваем пустой адрес ...");
                string result = await DownloadAsync("");
                Console.WriteLine($"Успех: {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка поймана в try/catch: {ex.Message}");
            }

            Console.WriteLine();
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
