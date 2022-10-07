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
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using Bookstore.Models.Responses;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, AddBookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AddBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AddBookResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var auth = await _bookRepository.GetBookByName(request._book.Title);
            var authorExists = await _authorRepository.GetById(request._book.AuthorId);

            if (authorExists == null)
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author with such ID does not exist, book can't be created."
                };

            if (auth != null)
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Book already exist"
                };

            var book = _mapper.Map<Book>(request._book);
            var result = await _bookRepository.AddBook(book);

            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Title = result
            };
        }
    }
}
