using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.IO;

namespace CSharpPracticum.Tasks.Level2
{

    internal class Task31_FileWriterDisposable : ITask
    {
        public string Title => "========================================== Задание 31. IDisposable + using ==========================================\n";
        public string Description => "Описание:\r\n Реализовать IDisposable для класса, работающего с файлом, и продемонстрировать using.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Класс FileWriter с полем StreamWriter.\r\n - Реализовать Dispose() (закрыть StreamWriter).\r\n - Продемонстрировать использование через using.\r\n - Дополнительно: продемонстрировать using-декларацию (C# 8+, using var).\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("=================================================================================================================\n");

            string filePath = Path.Combine(Path.GetTempPath(), "task31_demo.txt");

            using (var writer = new FileWriter(filePath, append: false))
            {
                writer.WriteLine("Первая строка: записана через классический блок using.");
            }
            Console.WriteLine("Блок using завершён — файл закрыт.");

            WriteWithUsingDeclaration(filePath);
            Console.WriteLine("Метод с using var завершён — файл снова закрыт без ошибок.");

            Console.WriteLine("\nСодержимое файла:");
            Console.WriteLine(File.ReadAllText(filePath));

            File.Delete(filePath);

            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }

        private void WriteWithUsingDeclaration(string path)
        {
            using var writer = new FileWriter(path, append: true);
            writer.WriteLine("Вторая строка: дописана через using var (C# 8+).");
        }

        private class FileWriter : IDisposable
        {
            private readonly StreamWriter _writer;
            private bool _disposed;

            public FileWriter(string path, bool append = false)
            {
                _writer = new StreamWriter(path, append);
            }

            public void WriteLine(string text)
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(FileWriter));

                _writer.WriteLine(text);
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    _writer.Dispose();
                    _disposed = true;
                }
            }
        }
    }
}