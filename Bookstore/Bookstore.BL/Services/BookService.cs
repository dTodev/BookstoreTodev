using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.MsSql;
using Bookstore.Models;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;
using Microsoft.Extensions.Logging;

namespace Bookstore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<AuthorService> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task <IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllBooks();
        }

        public async Task <AddBookResponse> AddBook(AddBookRequest bookRequest)
        {
            var auth = await _bookRepository.GetBookByName(bookRequest.Title);

            if (auth != null)
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Book already exist"
                };

            var book = _mapper.Map<Book>(bookRequest);
            var result = await _bookRepository.AddBook(book);

            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Title = result
            };
        }

        public async Task <Book> DeleteBook(int bookId)
        {
            return await _bookRepository.DeleteBook(bookId);
        }
        
        public async Task <Book?> GetById(int id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<Book> GetBookByName(string name)
        {
            return await _bookRepository.GetBookByName(name);
        }

        public async Task <UpdateBookResponse> UpdateBook(UpdateBookRequest bookRequest)
        {
            var auth = _bookRepository.GetBookByName(bookRequest.Title);

            if (auth == null)
                return new UpdateBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Book does not exist so it can't be updated"
                };

            var book = _mapper.Map<Book>(bookRequest);
            var result = await _bookRepository.UpdateBook(book);

            return new UpdateBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Title = result
            };
        }

        public async Task<bool> AddMultipleBooks(IEnumerable<Book> bookCollection)
        {
            return await _bookRepository.AddMultipleBooks(bookCollection);
        }
    }
}
