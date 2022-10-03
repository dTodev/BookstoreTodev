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
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book? AddBook(Book book)
        {
            return _bookRepository.AddBook(book);
        }

        public Book DeleteBook(int bookId)
        {
            return _bookRepository.DeleteBook(bookId);
        }
        
        public Book? GetById(int id)
        {
            return _bookRepository.GetById(id);
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
