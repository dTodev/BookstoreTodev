using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Models;
using Bookstore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository authorInMemoryRepository;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger, IAuthorRepository authorInMemoryRepository)
        {
            _logger = logger;
            this.authorInMemoryRepository = authorInMemoryRepository;
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return authorInMemoryRepository.GetGuidId();

        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Author> Get()
        {
            return authorInMemoryRepository.GetAllAuthors();

        }

        [HttpGet(nameof(GetById))]
        public Author GetById(int id)
        {
            return authorInMemoryRepository.GetById(id);

        }

        [HttpPost(nameof(AddAuthor))]
        public Author AddAuthor(Author author)
        {
            authorInMemoryRepository.AddAuthor(author);
            return author;
        }

        [HttpPost(nameof(UpdateAuthor))]
        public Author UpdateAuthor(Author author)
        {
            authorInMemoryRepository.UpdateAuthor(author);
            return author;
        }


    }
}