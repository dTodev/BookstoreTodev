using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;

namespace Bookstore.DL.Repositories.InMemoryRepositories
{
    public class BookRepositoryOLD //: IBookRepository
    {

        private static List<Book> _books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Title = "Book1",
                AuthorId = 1
            },
            new Book()
            {
                Id = 2,
                Title = "Book2",
                AuthorId = 2
            },
            new Book()
            {
                Id = 3,
                Title = "Book3",
                AuthorId = 3
            }
        };

        public Guid Id { get; set; }

        public BookRepositoryOLD()
        {
            Id = Guid.NewGuid();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(x => x.Id == id);
        }

        public Book? GetBookByName(string name)
        {
            return _books.FirstOrDefault(x => x.Title == name);
        }

        public Book? AddBook(Book book)
        {
            try
            {
                _books.Add(book);
            }
            catch (Exception a)
            {
                return null;
            }

            return book;
        }

        public Book UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(x => x.Title == book.Title);

            if (existingBook == null) return null;
            _books.Remove(existingBook);
            _books.Add(book);
            return book;
        }

        public Book DeleteBook(int bookId)
        {
            if (bookId <= 0) return null;

            var book = _books.FirstOrDefault(x => x.Id == bookId);

            _books.Remove(book);

            return book;
        }
    }
}