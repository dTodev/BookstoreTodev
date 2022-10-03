using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;

namespace Bookstore.BL.Interfaces
{
    public interface IAuthorService
    {
        AddAuthorResponse AddAuthor(AddAuthorRequest user);
        Author DeleteAuthor(int userId);
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Author GetAuthorByName(string name);
        UpdateAuthorResponse UpdateAuthor(UpdateAuthorRequest user);
    }
}
