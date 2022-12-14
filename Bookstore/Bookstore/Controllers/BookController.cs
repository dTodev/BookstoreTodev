using System.Net;
using AutoMapper;
using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BookController(ILogger<BookController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllBooks))]
        public async Task <IActionResult> GetAllBooks()
        {
            return Ok(await _mediator.Send(new GetAllBooksCommand()));
        }

        [HttpGet(nameof(GetById))]
        public async Task <Book> GetById(int id)
        {
            var result = await _mediator.Send(new GetBookByIdCommand(id));

            return result;
        }

        [HttpGet(nameof(GetBookByName))]
        public async Task<Book> GetBookByName(string name)
        {
            var result = await _mediator.Send(new GetBookByNameCommand(name));

            return result;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddBook))]
        public async Task <IActionResult> AddBook([FromBody] AddBookRequest bookRequest)
        {
            var result = await _mediator.Send(new AddBookCommand(bookRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddBookRange))]
        public async Task<IActionResult> AddBookRange([FromBody] AddMultipleBooksRequest addMultipleBooksRequests)
        {
            if (addMultipleBooksRequests != null && !addMultipleBooksRequests.BookRequests.Any())
                return BadRequest(addMultipleBooksRequests);

            var bookCollection = _mapper.Map<IEnumerable<Book>>(addMultipleBooksRequests.BookRequests);

            var result = await _mediator.Send(new AddMultipleBooksCommand(bookCollection));

            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateBook))]
        public async Task <IActionResult> UpdateBook([FromBody] UpdateBookRequest bookRequest)
        {
            var result = await _mediator.Send(new UpdateBookCommand(bookRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost(nameof(DeleteBook))]
        public async Task<Book> DeleteBook(int Id)
        {
            var result = await _mediator.Send(new DeleteBookCommand(Id));

            return result;
        }
    }
}