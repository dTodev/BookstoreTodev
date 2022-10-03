using System.Net;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService, IMapper mapper)
        {
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;   
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllAuthors))]
        public IActionResult GetAllAuthors()
        {
            return Ok(_authorService.GetAllAuthors());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthorRange))]
        public IActionResult AddAuthorRange([FromBody] AddMultipleAuthorsRequest addMultipleAuthorRequests)
        {
            if (addMultipleAuthorRequests != null && !addMultipleAuthorRequests.AuthorRequests.Any())
                    return BadRequest(addMultipleAuthorRequests);

            var authorCollection = _mapper.Map<IEnumerable<Author>>(addMultipleAuthorRequests.AuthorRequests);

            var result = _authorService.AddMultipleAuthors(authorCollection);

            if(!result) return BadRequest(result);

            return Ok(result);
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