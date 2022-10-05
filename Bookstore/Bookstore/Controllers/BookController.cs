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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;

        public BookController(ILogger<BookController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAllBooks))]
        public async Task <IActionResult> GetAllBooks()
        {
            return Ok(await _bookService.GetAllBooks());
        }

        [HttpGet(nameof(GetById))]
        public async Task <Book> GetById(int id)
        {
            return await _bookService.GetById(id);
        }

        [HttpGet(nameof(GetBookByName))]
        public async Task<Book> GetBookByName(string name)
        {
            return await _bookService.GetBookByName(name);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddBook))]
        public async Task <IActionResult> AddBook([FromBody] AddBookRequest bookRequest)
        {
            var result = await _bookService.AddBook(bookRequest);

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

            var result = await _bookService.AddMultipleBooks(bookCollection);

            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateBook))]
        public async Task <IActionResult> UpdateBook([FromBody] UpdateBookRequest bookRequest)
        {
            var result = await _bookService.UpdateBook(bookRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost(nameof(DeleteBook))]
        public async Task<Book> DeleteBook(int Id)
        {
            return await _bookService.DeleteBook(Id);
        }
    }
}