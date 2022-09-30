using Bookstore.Models.Models;

namespace Bookstore.Models
{
    public interface IAuthorRepository
    {
        Author? AddAuthor(Author user);
        Author DeleteAuthor(int userId);
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Author UpdateAuthor(Author user);
        Guid GetGuidId();
    }
}