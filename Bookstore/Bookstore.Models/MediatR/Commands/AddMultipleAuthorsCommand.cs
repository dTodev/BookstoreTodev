using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;
using MediatR;

namespace Bookstore.Models.MediatR.Commands
{
    public record AddMultipleAuthorsCommand(IEnumerable<Author> authors) : IRequest<bool>
    {
    }
}
