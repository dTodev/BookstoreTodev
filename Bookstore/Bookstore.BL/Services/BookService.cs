using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;

namespace Bookstore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookService;

        public BookService(IBookRepository bookRepository)
        {
            _bookService = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookService.GetAllBooks();
        }

        public Book? AddBook(Book book)
        {
            return _bookService.AddBook(book);
        }

        public Book DeleteBook(int bookId)
        {
            return _bookService.DeleteBook(bookId);
        }
        
        public Book? GetById(int id)
        {
            return _bookService.GetById(id);
        }

        public Guid GetGuidId()
        {
            return Guid.NewGuid();
        }

        public Book UpdateBook(Book book)
        {
            return UpdateBook(book);
        }
    }
}
