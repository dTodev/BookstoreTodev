using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;
using Bookstore.Models.Responses;
using MediatR;

namespace Bookstore.Models.MediatR.Commands
{
    public record DeleteBookCommand(int bookId) : IRequest<Book>
    {

    }
}
