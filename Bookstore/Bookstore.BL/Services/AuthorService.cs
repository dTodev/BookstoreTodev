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
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository, IMapper mapper, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task <IEnumerable<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAllAuthors();
        }

        public async Task <AddAuthorResponse> AddAuthor(AddAuthorRequest authorRequest)
        {
            try
            {
            var auth = await _authorRepository.GetAuthorByName(authorRequest.Name);

                if (auth != null)
                    return new AddAuthorResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author already exist"
                };

            var author = _mapper.Map<Author>(authorRequest);
            var result = await _authorRepository.AddAuthor(author);

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

        public async Task <UpdateAuthorResponse> DeleteAuthor(int authorId)
        {
            var authorExists = await _authorRepository.GetById(authorId);

            if (authorExists == null)
                return new UpdateAuthorResponse
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Message = "Author does not exist, delete is impossible."
                };

            var authorHasBooks = await _bookRepository.GetByAuthorId(authorId);

            if (authorHasBooks != null)
                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author has books, so delete is impossible."
                };

            var result = await _authorRepository.DeleteAuthor(authorId);

            return new UpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Success."
            };
        }

        public async Task <Author?> GetById(int id)
        {
            return await _authorRepository.GetById(id);
        }

        public async Task <Author> GetAuthorByName(string name)
        {
            return await _authorRepository.GetAuthorByName(name);
        }

        public async Task <UpdateAuthorResponse> UpdateAuthor(UpdateAuthorRequest authorRequest)
        {
            var auth = await _authorRepository.GetAuthorByName(authorRequest.Name);

            if (auth == null)
                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author does not exist so it can't be updated"
                };

            var author = _mapper.Map<Author>(authorRequest);
            var result = await _authorRepository.UpdateAuthor(author);

            return new UpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Author = result
            };
        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            return await _authorRepository.AddMultipleAuthors(authorCollection);
        }
    }
}
