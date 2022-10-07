using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.MediatR.Commands;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    internal class AddMultipleAuthorsCommandHandler : IRequestHandler<AddMultipleAuthorsCommand, bool>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddMultipleAuthorsCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> Handle(AddMultipleAuthorsCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.AddMultipleAuthors(request.authors);
        }
    }
}
