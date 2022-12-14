using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    internal class GetBookByIdCommandHandler : IRequestHandler<GetBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByIdCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetById(request.bookId);
        }
    }
}
