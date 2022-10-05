using Bookstore.Models.Models;

namespace Bookstore.Models
{
    public interface IAuthorRepository
    {
        public Task<Author> AddAuthor(Author user);
        public Task<Author> DeleteAuthor(int userId);
        public Task<IEnumerable<Author>> GetAllAuthors();
        public Task<Author?> GetById(int id);
        public Task<Author> GetAuthorByName(string name);
        public Task<Author> UpdateAuthor(Author user);
        public Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}