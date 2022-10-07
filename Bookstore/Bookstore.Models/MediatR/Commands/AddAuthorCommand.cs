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
    public record AddAuthorCommand(AddAuthorRequest author) : IRequest<AddAuthorResponse>
    {
        public readonly AddAuthorRequest _author = author;
    }
}
