using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;

namespace Bookstore.BL.Interfaces
{
    public interface IBookService
    {
        AddBookResponse AddBook(AddBookRequest book);
        Book DeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id);
        UpdateBookResponse UpdateBook(UpdateBookRequest book);
    }
}
