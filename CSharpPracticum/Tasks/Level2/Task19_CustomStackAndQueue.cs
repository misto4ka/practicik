using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task19_CustomStackAndQueue : ITask
    {
        public string Title => "==================================== Задание 19. Свой стек/очередь на массиве ====================================\n";
        public string Description => "Описание:\r\n Реализовать MyStack<T> и MyQueue<T> на массиве.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Стек: Push, Pop, Peek, Count, IsEmpty.\r\n - Очередь: Enqueue, Dequeue, Peek, Count, IsEmpty.\r\n - При переполнении — расширять внутренний массив (удвоение).\r\n - Pop/Dequeue на пустой коллекции — бросать InvalidOperationException.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            Console.WriteLine("--- Тест MyStack<int> (начальная ёмкость 2) ---");
            var stack = new MyStack<int>(2);
            Console.WriteLine($"Пуст ли стек: {stack.IsEmpty}");

            for (int i = 1; i <= 5; i++)
            {
                stack.Push(i * 10);
                Console.WriteLine($"Push({i * 10}) -> Count: {stack.Count}, Peek: {stack.Peek()}");
            }

            while (!stack.IsEmpty)
            {
                Console.WriteLine($"Pop() -> {stack.Pop()} (осталось: {stack.Count})");
            }

            try
            {
                stack.Pop();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ожидаемое исключение при Pop на пустом стеке: {ex.Message}\n");
            }

            Console.WriteLine("--- Тест MyQueue<string> (начальная ёмкость 2) ---");
            var queue = new MyQueue<string>(2);
            string[] items = { "Первый", "Второй", "Третий", "Четвёртый", "Пятый" };

            foreach (var item in items)
            {
                queue.Enqueue(item);
                Console.WriteLine($"Enqueue(\"{item}\") -> Count: {queue.Count}, Peek: {queue.Peek()}");
            }

            while (!queue.IsEmpty)
            {
                Console.WriteLine($"Dequeue() -> \"{queue.Dequeue()}\" (осталось: {queue.Count})");
            }

            try
            {
                queue.Dequeue();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ожидаемое исключение при Dequeue на пустой очереди: {ex.Message}");
            }

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }
    }

    public class MyStack<T>
    {
        private T[] _items;
        private int _count;

        public MyStack(int capacity = 4)
        {
            _items = new T[capacity];
            _count = 0;
        }

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }
            _items[_count++] = item;
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Стек пуст.");

            T item = _items[--_count];
            _items[_count] = default!;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Стек пуст.");

            return _items[_count - 1];
        }
    }

    public class MyQueue<T>
    {
        private T[] _items;
        private int _head;
        private int _tail;
        private int _count;

        public MyQueue(int capacity = 4)
        {
            _items = new T[capacity];
        }

        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public void Enqueue(T item)
        {
            if (_count == _items.Length)
            {
                T[] newArray = new T[_items.Length * 2];
                for (int i = 0; i < _count; i++)
                {
                    newArray[i] = _items[(_head + i) % _items.Length];
                }
                _items = newArray;
                _head = 0;
                _tail = _count;
            }

            _items[_tail] = item;
            _tail = (_tail + 1) % _items.Length;
            _count++;
        }

        public T Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Очередь пуста.");

            T item = _items[_head];
            _items[_head] = default!;
            _head = (_head + 1) % _items.Length;
            _count--;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Очередь пуста.");

            return _items[_head];
        }
    }
}