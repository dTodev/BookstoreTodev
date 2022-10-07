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
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.BL.CommandHandlers
{
    internal class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<UpdateBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var auth = _bookRepository.GetBookByName(request.bookName.Title);

            if (auth == null)
                return new UpdateBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Book does not exist so it can't be updated"
                };

            var book = _mapper.Map<Book>(request.bookName);
            var result = await _bookRepository.UpdateBook(book);

            return new UpdateBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Title = result
            };
        }
    }
}
