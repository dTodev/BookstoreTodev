using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using Bookstore.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookstore.BL.CommandHandlers
{
    internal class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AddAuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository, IMapper mapper, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddAuthorResponse> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var auth = await _authorRepository.GetAuthorByName(request._author.Name);

                if (auth != null)
                    return new AddAuthorResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author already exist"
                    };

                var author = _mapper.Map<Author>(request._author);
                var result = await _authorRepository.AddAuthor(author);

                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result
                };
            }
            catch (Exception e)
            {
                _logger.LogError("TEST ERROR");
                throw;
            }
        }
    }
}
