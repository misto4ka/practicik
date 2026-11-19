using System;
using System.Collections.Generic;
using System.Linq;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class BookNotAvailableException : InvalidOperationException
    {
        public BookNotAvailableException(string title)
            : base($"Книга «{title}» уже выдана.") { }
    }

    public class BorrowLimitExceededException : InvalidOperationException
    {
        public BorrowLimitExceededException(string readerName, int limit)
            : base($"Читатель {readerName} уже взял максимум книг ({limit}).") { }
    }

    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public bool IsAvailable { get; internal set; } = true;

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }

    public class Reader
    {
        private readonly List<Book> _borrowedBooks = new List<Book>();

        public string Name { get; }
        public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks;

        public Reader(string name)
        {
            Name = name;
        }

        internal void AddBook(Book book)
        {
            _borrowedBooks.Add(book);
        }

        internal void RemoveBook(Book book)
        {
            _borrowedBooks.Remove(book);
        }
    }

    public class Library
    {
        public const int MaxBooksPerReader = 3;

        private readonly List<Book> _books = new List<Book>();

        public IReadOnlyList<Book> Books => _books;

        public void AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            _books.Add(book);
        }

        public void Borrow(Reader reader, Book book)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (book == null) throw new ArgumentNullException(nameof(book));

            if (!book.IsAvailable)
                throw new BookNotAvailableException(book.Title);

            if (reader.BorrowedBooks.Count >= MaxBooksPerReader)
                throw new BorrowLimitExceededException(reader.Name, MaxBooksPerReader);

            book.IsAvailable = false;
            reader.AddBook(book);
        }

        public void Return(Reader reader, Book book)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (book == null) throw new ArgumentNullException(nameof(book));

            if (!reader.BorrowedBooks.Contains(book))
                throw new InvalidOperationException($"Читатель {reader.Name} не брал книгу «{book.Title}».");

            reader.RemoveBook(book);
            book.IsAvailable = true;
        }
    }

    public class Task45_Library : ITask
    {
        public string Title => "==================================================\r\nЗадание 45. Мини-проект «Библиотека» + юнит-тесты\r\n--------------------------------------------------";
        public string Description => "Описание: классы библиотеки + тесты. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nКлассы: Book (Title, Author, IsAvailable), Reader (Name, BorrowedBooks), Library (выдача/возврат).\r\nЛогика: выдача книги читателю, возврат, лимит (не больше 3 книг на читателя).\r\nОбработка: книгу уже выдали, лимит превышен — бросать исключения.\r\nНаписать 5+ юнит-тестов (xUnit/NUnit) на ключевые сценарии (папка LibraryTests).\r\nВсе тесты должны проходить." };

        private static void Try(string action, Action code)
        {
            Console.Write($"{action}: ");
            try
            {
                code();
                Console.WriteLine("успешно");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"ошибка — {ex.Message}");
            }
        }

        private static void PrintBooks(Reader reader)
        {
            string titles = reader.BorrowedBooks.Count == 0
                ? "нет книг"
                : string.Join(", ", reader.BorrowedBooks.Select(b => b.Title));
            Console.WriteLine($"  У читателя {reader.Name}: {titles}");
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            Library library = new Library();
            Book war = new Book("Война и мир", "Л. Толстой");
            Book crime = new Book("Преступление и наказание", "Ф. Достоевский");
            Book master = new Book("Мастер и Маргарита", "М. Булгаков");
            Book onegin = new Book("Евгений Онегин", "А. Пушкин");

            foreach (Book book in new[] { war, crime, master, onegin })
                library.AddBook(book);

            Reader anna = new Reader("Анна");
            Reader boris = new Reader("Борис");

            Try("Анна берёт «Война и мир»", () => library.Borrow(anna, war));
            Try("Анна берёт «Преступление и наказание»", () => library.Borrow(anna, crime));
            Try("Анна берёт «Мастер и Маргарита»", () => library.Borrow(anna, master));
            Try("Анна берёт «Евгений Онегин» (лимит)", () => library.Borrow(anna, onegin));
            Try("Борис берёт «Война и мир» (уже выдана)", () => library.Borrow(boris, war));
            PrintBooks(anna);

            Try("Анна возвращает «Война и мир»", () => library.Return(anna, war));
            Try("Борис берёт «Война и мир»", () => library.Borrow(boris, war));
            Try("Борис возвращает «Мастер и Маргарита» (не брал)", () => library.Return(boris, master));
            PrintBooks(anna);
            PrintBooks(boris);

            Console.WriteLine();
            Console.WriteLine("Тесты запускаются командой: dotnet test (в папке LibraryTests).");

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
