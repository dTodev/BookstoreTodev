using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using Bookstore.Models.Responses;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    internal class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthorResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<UpdateAuthorResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var auth = await _authorRepository.GetAuthorByName(request.authorName.Name);

            if (auth == null)
                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author does not exist so it can't be updated"
                };

            var author = _mapper.Map<Author>(request.authorName);
            var result = await _authorRepository.UpdateAuthor(author);

            return new UpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Author = result
            };
        }
    }
}
