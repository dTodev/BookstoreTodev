using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using Bookstore.Models.Responses;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    internal class AddMultipleBooksCommandHandler : IRequestHandler<AddMultipleBooksCommand, bool>
    {
        private readonly IBookRepository _bookRepository;

        public AddMultipleBooksCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(AddMultipleBooksCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.AddMultipleBooks(request.books);
        }
    }
}
