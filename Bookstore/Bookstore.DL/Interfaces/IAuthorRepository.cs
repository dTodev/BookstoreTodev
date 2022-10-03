using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;

namespace Bookstore.Models
{
    public interface IAuthorRepository
    {
        Author AddAuthor(Author user);
        Author DeleteAuthor(int userId);
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Author GetAuthorByName(string name);
        Author UpdateAuthor(Author user);
        public bool AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}