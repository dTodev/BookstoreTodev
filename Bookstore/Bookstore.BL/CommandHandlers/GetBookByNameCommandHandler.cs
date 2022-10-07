using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    internal class GetBookByNameCommandHandler : IRequestHandler<GetBookByNameCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByNameCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByNameCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetBookByName(request.bookName);
        }
    }
}
