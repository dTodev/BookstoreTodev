using System.Net;
using AutoMapper;
using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthorController(ILogger<AuthorController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllAuthors))]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _mediator.Send(new GetAllAuthorsCommand()));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAuthorByIdCommand(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet(nameof(GetAuthorByName))]
        public async Task<Author> GetAuthorByName(string name)
        {
            var result = await _mediator.Send(new GetAuthorByNameCommand(name));

            return result;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            var result = await _mediator.Send(new AddAuthorCommand(authorRequest));

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

            var result = await _mediator.Send(new AddMultipleAuthorsCommand(authorCollection));

            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateAuthor))]
        public async Task <IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest authorRequest)
        {
            var result = await _mediator.Send(new UpdateAuthorCommand(authorRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost(nameof(DeleteAuthor))]
        public async Task<IActionResult> DeleteAuthor(int Id)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand(Id));

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound(result);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }
    }
}