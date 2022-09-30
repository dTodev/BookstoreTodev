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
        private readonly IAuthorRepository _authorService;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorService = authorRepository;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorService.GetAllAuthors();
        }

        public Author? AddAuthor(Author author)
        {
            return _authorService.AddAuthor(author);
        }

        public Author DeleteAuthor(int authorId)
        {
            return _authorService.DeleteAuthor(authorId);
        }

        public Author? GetById(int id)
        {
            return _authorService.GetById(id);
        }

        public Guid GetGuidId()
        {
            return Guid.NewGuid();
        }

        public Author UpdateAuthor(Author author)
        {
            return UpdateAuthor(author);
        }

    }
}
