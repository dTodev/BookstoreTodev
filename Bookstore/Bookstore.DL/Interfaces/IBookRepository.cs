using Bookstore.Models.Models;

namespace Bookstore.DL.Interfaces
{
    public interface IBookRepository
    {
        Book? AddBook(Book book);
        Book DeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id);
        Book UpdateBook(Book book);
        Guid GetGuidId();
    }
}