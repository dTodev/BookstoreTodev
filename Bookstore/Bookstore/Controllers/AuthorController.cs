using System.Net;
using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllAuthors))]
        public IActionResult GetAllAuthors()
        {
            _logger.LogInformation("Information Test");
            _logger.LogWarning("Warning Test");
            _logger.LogError("Error Test");
            _logger.LogCritical("Critical Test");
            return Ok(_authorService.GetAllAuthors());
        }

        [HttpGet(nameof(GetById))]
        public Author GetById(int id)
        {
            return _authorService.GetById(id);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]
        public IActionResult AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            var result = _authorService.AddAuthor(authorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateAuthor))]
        public IActionResult UpdateAuthor([FromBody] UpdateAuthorRequest authorRequest)
        {
            var result = _authorService.UpdateAuthor(authorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }
    }
}