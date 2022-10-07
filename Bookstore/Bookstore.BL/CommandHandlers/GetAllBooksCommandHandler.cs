using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.MsSql;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    public class GetAllBooksCommandHandler : IRequestHandler<GetAllBooksCommand, IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetAllBooks();
        }
    }
}
