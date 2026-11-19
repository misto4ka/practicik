using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task38_ListVsLinkedList : ITask
    {
        public string Title => "==================================================\r\nЗадание 38. List<T> vs LinkedList<T> (бенчмарк)\r\n--------------------------------------------------";
        public string Description => "Описание: сравнить вставку в середину List<T> и LinkedList<T>. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nСоздать List<int> и LinkedList<int> по 100 000 элементов.\r\nВставить 1000 элементов в середину каждой коллекции.\r\nЗамерить время через Stopwatch.\r\nВывести оба времени и объяснить результат в комментарии." };

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            const int size = 100000;
            const int inserts = 1000;

            List<int> list = Enumerable.Range(0, size).ToList();
            LinkedList<int> linked = new LinkedList<int>(Enumerable.Range(0, size));

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < inserts; i++)
            {
                list.Insert(list.Count / 2, i);
            }
            sw.Stop();
            double listInsertMs = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            LinkedListNode<int> node = linked.First;
            for (int i = 0; i < linked.Count / 2; i++)
            {
                node = node.Next;
            }
            for (int i = 0; i < inserts; i++)
            {
                node = linked.AddAfter(node, i);
            }
            sw.Stop();
            double linkedInsertMs = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Вставка {inserts} элементов в середину ({size} элементов в коллекции):");
            Console.WriteLine($"  List<int>:       {listInsertMs:F3} мс");
            Console.WriteLine($"  LinkedList<int>: {linkedInsertMs:F3} мс");

            long checksum = 0;
            sw.Restart();
            for (int i = 0; i < inserts; i++)
            {
                checksum += list[list.Count / 2];
            }
            sw.Stop();
            double listIndexMs = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            for (int i = 0; i < inserts; i++)
            {
                checksum += linked.ElementAt(linked.Count / 2);
            }
            sw.Stop();
            double linkedIndexMs = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine();
            Console.WriteLine($"Доступ по индексу к середине ({inserts} раз):");
            Console.WriteLine($"  List<int>:       {listIndexMs:F3} мс");
            Console.WriteLine($"  LinkedList<int>: {linkedIndexMs:F3} мс");
            Console.WriteLine($"  (контрольная сумма: {checksum})");

            Console.WriteLine();
            Console.WriteLine("Вывод: LinkedList быстрее на вставке в середину, но медленнее на поиске по индексу.");

            // Почему так: List<T> хранит элементы в одном массиве, поэтому вставка в середину
            // сдвигает все элементы справа (O(n)), зато доступ по индексу мгновенный (O(1)).
            // LinkedList<T> — цепочка узлов: вставка рядом с уже найденным узлом не требует
            // сдвигов (O(1)), но чтобы попасть на позицию по индексу, нужно пройти по узлам (O(n)).

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
