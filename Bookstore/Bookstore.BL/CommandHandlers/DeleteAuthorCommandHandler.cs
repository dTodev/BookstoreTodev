using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.MsSql;
using Bookstore.Models;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using Bookstore.Models.Responses;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    internal class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, UpdateAuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public async Task<UpdateAuthorResponse> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorExists = await _authorRepository.GetById(request.authorId);

            if (authorExists == null)
                return new UpdateAuthorResponse
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Message = "Author does not exist, delete is impossible."
                };

            var authorHasBooks = await _bookRepository.GetByAuthorId(request.authorId);

            if (authorHasBooks != null)
                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author has books, so delete is impossible."
                };

            var result = await _authorRepository.DeleteAuthor(request.authorId);

            return new UpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Success."
            };
        }
    }
}
