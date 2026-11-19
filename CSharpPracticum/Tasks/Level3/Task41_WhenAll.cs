using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task41_WhenAll : ITask
    {
        public string Title => "==================================================\r\nЗадание 41. Task.WhenAll — параллельно 10 задач\r\n--------------------------------------------------";
        public string Description => "Описание: запустить 10 задач параллельно и сравнить с последовательным выполнением. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nМетод Task<int> WorkAsync(int id) — Task.Delay(500), возвращает id.\r\nПоследовательный запуск 10 задач в цикле с await.\r\nПараллельный запуск через Task.WhenAll.\r\nЗамерить время обоих вариантов через Stopwatch.\r\nВывести оба времени и объяснить разницу." };

        private static async Task<int> WorkAsync(int id)
        {
            await Task.Delay(500);
            return id;
        }

        private static async Task RunAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<int> sequential = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                sequential.Add(await WorkAsync(i));
            }
            sw.Stop();
            double sequentialMs = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            int[] parallel = await Task.WhenAll(Enumerable.Range(1, 10).Select(WorkAsync));
            sw.Stop();
            double parallelMs = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Последовательно: {sequentialMs:F0} мс, результаты: {string.Join(", ", sequential)}");
            Console.WriteLine($"Task.WhenAll:    {parallelMs:F0} мс, результаты: {string.Join(", ", parallel)}");
            Console.WriteLine($"Разница: {sequentialMs - parallelMs:F0} мс");
            Console.WriteLine($"Результаты одинаковые: {sequential.SequenceEqual(parallel)}");
            Console.WriteLine();
            Console.WriteLine("Объяснение: в цикле с await каждая задача ждёт окончания предыдущей, поэтому время");
            Console.WriteLine("складывается (10 x 500 мс). Task.WhenAll запускает все задачи сразу и ждёт их вместе,");
            Console.WriteLine("поэтому общее время примерно равно самой долгой задаче (около 500 мс).");
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            Console.WriteLine("Выполняется (около 6 секунд)...");
            RunAsync().GetAwaiter().GetResult();

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
