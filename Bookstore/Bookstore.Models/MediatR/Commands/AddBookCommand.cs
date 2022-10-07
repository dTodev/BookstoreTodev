using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;
using MediatR;

namespace Bookstore.Models.MediatR.Commands
{
    public record AddBookCommand(AddBookRequest book) : IRequest<AddBookResponse>
    {
        public readonly AddBookRequest _book = book;
    }
}
