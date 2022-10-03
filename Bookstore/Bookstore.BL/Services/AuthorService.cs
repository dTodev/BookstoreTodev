using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.Models;

namespace Bookstore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public Author? AddAuthor(Author author)
        {
            return _authorRepository.AddAuthor(author);
        }

        public Author DeleteAuthor(int authorId)
        {
            return _authorRepository.DeleteAuthor(authorId);
        }

        public Author? GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public Author UpdateAuthor(Author author)
        {
            return UpdateAuthor(author);
        }

    }
}
