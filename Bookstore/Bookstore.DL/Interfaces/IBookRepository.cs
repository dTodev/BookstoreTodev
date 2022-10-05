using Bookstore.Models.Models;

namespace Bookstore.DL.Interfaces
{
    public interface IBookRepository
    {
        public Task <Book?> AddBook(Book book);
        public Task <Book> DeleteBook(int bookId);
        public Task <IEnumerable<Book>> GetAllBooks();
        public Task <Book?> GetById(int id);
        public Task<Book?> GetByAuthorId(int authorId);
        public Task <Book> GetBookByName(string name);
        public Task <Book> UpdateBook(Book book);
        public Task<bool> AddMultipleBooks(IEnumerable<Book> bookCollection);
        
    }
}