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
        public Task <AddAuthorResponse> AddAuthor(AddAuthorRequest user);
        public Task <UpdateAuthorResponse> DeleteAuthor(int userId);
        public Task <IEnumerable<Author>> GetAllAuthors();
        public Task <Author?> GetById(int id);
        public Task <Author> GetAuthorByName(string name);
        public Task <UpdateAuthorResponse> UpdateAuthor(UpdateAuthorRequest user);
        public Task <bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
