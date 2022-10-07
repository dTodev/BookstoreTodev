using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Bookstore.Models;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    internal class GetAuthorByNameCommandHandler : IRequestHandler<GetAuthorByNameCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByNameCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(GetAuthorByNameCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetAuthorByName(request.authorName);
        }
    }
}
