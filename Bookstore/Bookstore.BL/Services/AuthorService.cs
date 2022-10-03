using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;
using Microsoft.Extensions.Logging;

namespace Bookstore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public AddAuthorResponse AddAuthor(AddAuthorRequest authorRequest)
        {
            try
            {
            var auth = _authorRepository.GetAuthorByName(authorRequest.Name);

                if (auth != null)
                    throw new Exception();
                    return new AddAuthorResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author already exist"
                };

            var author = _mapper.Map<Author>(authorRequest);
            var result = _authorRepository.AddAuthor(author);

            return new AddAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Author = result
            };
            }
            catch (Exception e)
            {
                _logger.LogError("TEST ERROR");
                throw ;
            }
        }

        public Author DeleteAuthor(int authorId)
        {
            return _authorRepository.DeleteAuthor(authorId);
        }

        public Author? GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public Author GetAuthorByName(string name)
        {
            return _authorRepository.GetAuthorByName(name);
        }

        public UpdateAuthorResponse UpdateAuthor(UpdateAuthorRequest authorRequest)
        {
            var auth = _authorRepository.GetAuthorByName(authorRequest.Name);

            if (auth == null)
                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author does not exist so it can't be updated"
                };

            var author = _mapper.Map<Author>(authorRequest);
            var result = _authorRepository.UpdateAuthor(author);

            return new UpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Name = result
            };
        }
    }
}
