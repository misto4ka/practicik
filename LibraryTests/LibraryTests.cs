using System;
using CSharpPracticum.Tasks.Level3;
using Xunit;

namespace Level3.Tests
{
    /// <summary>Юнит-тесты для класса Library.</summary>
    public class LibraryTests
    {
        private static Book NewBook(string title) => new Book(title, "Автор");

        /// <summary>Выдача свободной книги: книга у читателя и помечена как недоступная.</summary>
        [Fact]
        public void Borrow_AvailableBook_AddsToReaderAndMarksUnavailable()
        {
            Library library = new Library();
            Reader reader = new Reader("Анна");
            Book book = NewBook("Книга 1");
            library.AddBook(book);

            library.Borrow(reader, book);

            Assert.False(book.IsAvailable);
            Assert.Contains(book, reader.BorrowedBooks);
        }

        /// <summary>Повторная выдача уже выданной книги вызывает исключение.</summary>
        [Fact]
        public void Borrow_AlreadyBorrowedBook_ThrowsBookNotAvailableException()
        {
            Library library = new Library();
            Reader first = new Reader("Анна");
            Reader second = new Reader("Борис");
            Book book = NewBook("Книга 1");
            library.AddBook(book);
            library.Borrow(first, book);

            Assert.Throws<BookNotAvailableException>(() => library.Borrow(second, book));
            Assert.Empty(second.BorrowedBooks);
        }

        /// <summary>Три книги на одного читателя разрешены.</summary>
        [Fact]
        public void Borrow_ThreeBooks_Succeeds()
        {
            Library library = new Library();
            Reader reader = new Reader("Анна");

            for (int i = 1; i <= 3; i++)
            {
                Book book = NewBook($"Книга {i}");
                library.AddBook(book);
                library.Borrow(reader, book);
            }

            Assert.Equal(3, reader.BorrowedBooks.Count);
        }

        /// <summary>Четвёртая книга превышает лимит и вызывает исключение.</summary>
        [Fact]
        public void Borrow_FourthBook_ThrowsBorrowLimitExceededException()
        {
            Library library = new Library();
            Reader reader = new Reader("Анна");

            for (int i = 1; i <= 3; i++)
            {
                Book book = NewBook($"Книга {i}");
                library.AddBook(book);
                library.Borrow(reader, book);
            }

            Book fourth = NewBook("Книга 4");
            library.AddBook(fourth);

            Assert.Throws<BorrowLimitExceededException>(() => library.Borrow(reader, fourth));
            Assert.True(fourth.IsAvailable);
            Assert.Equal(3, reader.BorrowedBooks.Count);
        }

        /// <summary>После возврата книга снова доступна.</summary>
        [Fact]
        public void Return_BorrowedBook_MakesBookAvailableAgain()
        {
            Library library = new Library();
            Reader reader = new Reader("Анна");
            Book book = NewBook("Книга 1");
            library.AddBook(book);
            library.Borrow(reader, book);

            library.Return(reader, book);

            Assert.True(book.IsAvailable);
            Assert.Empty(reader.BorrowedBooks);
        }

        /// <summary>Возврат книги, которую читатель не брал, вызывает исключение.</summary>
        [Fact]
        public void Return_BookNotBorrowedByReader_ThrowsInvalidOperationException()
        {
            Library library = new Library();
            Reader reader = new Reader("Анна");
            Book book = NewBook("Книга 1");
            library.AddBook(book);

            Assert.Throws<InvalidOperationException>(() => library.Return(reader, book));
        }

        /// <summary>После возврата читатель снова может взять книгу.</summary>
        [Fact]
        public void Return_FreesSlot_ReaderCanBorrowAgainAfterLimit()
        {
            Library library = new Library();
            Reader reader = new Reader("Анна");
            Book[] books = new Book[4];

            for (int i = 0; i < books.Length; i++)
            {
                books[i] = NewBook($"Книга {i + 1}");
                library.AddBook(books[i]);
            }

            for (int i = 0; i < 3; i++)
                library.Borrow(reader, books[i]);

            library.Return(reader, books[0]);
            library.Borrow(reader, books[3]);

            Assert.Equal(3, reader.BorrowedBooks.Count);
            Assert.Contains(books[3], reader.BorrowedBooks);
        }

        /// <summary>Выдача без читателя вызывает ArgumentNullException.</summary>
        [Fact]
        public void Borrow_NullReader_ThrowsArgumentNullException()
        {
            Library library = new Library();
            Book book = NewBook("Книга 1");
            library.AddBook(book);

            Assert.Throws<ArgumentNullException>(() => library.Borrow(null, book));
        }
    }
}
