using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;
using MediatR;

namespace Bookstore.Models.MediatR.Commands
{
    public record GetBookByNameCommand(string bookName) : IRequest<Book>
    {
    }
}
