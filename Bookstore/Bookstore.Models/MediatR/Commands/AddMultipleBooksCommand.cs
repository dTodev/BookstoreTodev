using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Responses;
using Bookstore.Models.Requests;
using MediatR;
using Bookstore.Models.Models;

namespace Bookstore.Models.MediatR.Commands
{
    public record AddMultipleBooksCommand(IEnumerable<Book> books) : IRequest<bool>
    {
    }
}
