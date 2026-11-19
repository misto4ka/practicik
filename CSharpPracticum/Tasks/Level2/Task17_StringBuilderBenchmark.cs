using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Diagnostics;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task17_StringBuilderBenchmark : ITask
    {
        public string Title => "==================================== Задание 17. StringBuilder vs конкатенация ====================================\n";
        public string Description => "Описание:\r\n Сравнить производительность конкатенации через + и StringBuilder.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Собрать строку из 10 000 чисел двумя способами.\r\n - Замерить время каждого через Stopwatch.\r\n - Вывести оба времени и разницу.\r\n - Объяснить в комментарии, почему StringBuilder быстрее.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            const int count = 10000;

            Stopwatch swConcat = Stopwatch.StartNew();
            string strConcat = "";
            for (int i = 0; i < count; i++)
            {
                strConcat += i.ToString();
            }
            swConcat.Stop();

            Stopwatch swSb = Stopwatch.StartNew();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(i);
            }
            string strSb = sb.ToString();
            swSb.Stop();

            Console.WriteLine($"Результаты идентичны: {strConcat == strSb} (длина: {strSb.Length} симв.)\n");
            Console.WriteLine($"Время конкатенации (+): {swConcat.Elapsed.TotalMilliseconds:F4} мс ({swConcat.ElapsedTicks} тиков)");
            Console.WriteLine($"Время StringBuilder:    {swSb.Elapsed.TotalMilliseconds:F4} мс ({swSb.ElapsedTicks} тиков)");

            double diffMs = swConcat.Elapsed.TotalMilliseconds - swSb.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Разница:                {diffMs:F4} мс (StringBuilder быстрее в ~{(double)swConcat.ElapsedTicks / Math.Max(1, swSb.ElapsedTicks):F1} раз)");

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }
    }
}