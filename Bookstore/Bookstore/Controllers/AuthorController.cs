using System.Net;
using AutoMapper;
using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;
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
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _authorService.GetAllAuthors());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _authorService.GetById(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(await _authorService.GetById(id));
        }

        [HttpGet(nameof(GetAuthorByName))]
        public async Task<Author> GetAuthorByName(string name)
        {
            return await _authorService.GetAuthorByName(name);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            var result = await _authorService.AddAuthor(authorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthorRange))]
        public async Task <IActionResult> AddAuthorRange([FromBody] AddMultipleAuthorsRequest addMultipleAuthorRequests)
        {
            if (addMultipleAuthorRequests != null && !addMultipleAuthorRequests.AuthorRequests.Any())
                    return BadRequest(addMultipleAuthorRequests);

            var authorCollection = _mapper.Map<IEnumerable<Author>>(addMultipleAuthorRequests.AuthorRequests);

            var result = await _authorService.AddMultipleAuthors(authorCollection);

            if(!result) return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateAuthor))]
        public async Task <IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest authorRequest)
        {
            var result = await _authorService.UpdateAuthor(authorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        //[HttpPost(nameof(DeleteAuthor))]
        //public async Task<UpdateAuthorResponse> DeleteAuthor(int Id)
        //{
        //    return await _authorService.DeleteAuthor(Id);
        //}

        [HttpPost(nameof(DeleteAuthor))]
        public async Task<IActionResult> DeleteAuthor(int Id)
        {
            var result = await _authorService.DeleteAuthor(Id);

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound(result);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }
    }
}