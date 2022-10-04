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
        public Task <AddBookResponse> AddBook(AddBookRequest book);
        public Task <Book> DeleteBook(int bookId);
        public Task <IEnumerable<Book>> GetAllBooks();
        public Task <Book?> GetById(int id);
        public Task <Book> GetBookByName(string name);
        public Task <UpdateBookResponse> UpdateBook(UpdateBookRequest book);
        public Task<bool> AddMultipleBooks(IEnumerable<Book> bookCollection);
    }
}
