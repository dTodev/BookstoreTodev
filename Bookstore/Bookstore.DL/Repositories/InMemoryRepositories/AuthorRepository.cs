using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.Models;
using Bookstore.Models.Responses;
using Microsoft.Extensions.Logging;

namespace Bookstore.DL.Repositories.InMemoryRepositories
{
    public class AuthorRepository : IAuthorRepository
    {

        private readonly ILogger<AuthorRepository> _logger;

        public AuthorRepository(ILogger<AuthorRepository> logger)
        {
            _logger = logger;
        }

        private static List<Author> _authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Bruce",
                Age = 30
            },
            new Author()
            {
                Id = 2,
                Name = "Gordon",
                Age = 30
            },
            new Author()
            {
                Id = 3,
                Name = "Selina",
                Age = 30
            }
        };

        public Guid Id { get; set; }

        public AuthorRepository()
        {
            Id = Guid.NewGuid();
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authors;
        }

        public Author? GetById(int id)
        {
            return _authors.FirstOrDefault(x => x.Id == id);
        }

        public Author? GetAuthorByName(string name)
        {
            return _authors.FirstOrDefault(x => x.Name == name);
        }

        public Author? AddAuthor(Author user)
        {
            try
            {
                _authors.Add(user);
            }
            catch (Exception a)
            {
                return null;
            }

            return user;
        }

        public Author UpdateAuthor(Author user)
        {
            var existingUser = _authors.FirstOrDefault(x => x.Name == user.Name);

            if (existingUser == null) return null;
            _authors.Remove(existingUser);
            _authors.Add(user);
            return user;
        }

        public Author DeleteAuthor(int userId)
        {
            if (userId <= 0) return null;

            var user = _authors.FirstOrDefault(x => x.Id == userId);

            _authors.Remove(user);

            return user;
        }

        public Guid GetGuidId()
        {
            return Id;
        }

        public bool AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            try
            {
                _authors.AddRange(authorCollection);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Unable to add multiple authors with Message:{e.Message}");
                return false;
            }
        }
    }
}