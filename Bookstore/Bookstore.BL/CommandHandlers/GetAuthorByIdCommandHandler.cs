﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using MediatR;

namespace Bookstore.BL.CommandHandlers
{
    internal class GetAuthorByIdCommandHandler : IRequestHandler<GetAuthorByIdCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(GetAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetById(request.authorId);
        }
    }
}
