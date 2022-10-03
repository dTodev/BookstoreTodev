using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;

namespace Bookstore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public AddBookResponse AddBook(AddBookRequest bookRequest)
        {
            var auth = _bookRepository.GetBookByName(bookRequest.Title);

            if (auth != null)
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Book already exist"
                };

            var book = _mapper.Map<Book>(bookRequest);
            var result = _bookRepository.AddBook(book);

            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Title = result
            };
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

        public UpdateBookResponse UpdateBook(UpdateBookRequest bookRequest)
        {
            var auth = _bookRepository.GetBookByName(bookRequest.Title);

            if (auth == null)
                return new UpdateBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Book does not exist so it can't be updated"
                };

            var book = _mapper.Map<Book>(bookRequest);
            var result = _bookRepository.UpdateBook(book);

            return new UpdateBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Title = result
            };
        }
    }
}
