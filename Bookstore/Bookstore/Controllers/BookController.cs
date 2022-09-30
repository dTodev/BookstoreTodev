using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService BookService;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            this.BookService = bookService;
        }

        [HttpGet(nameof(GetGuid))]
        public Guid GetGuid()
        {
            return BookService.GetGuidId();

        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Book> Get()
        {
            return BookService.GetAllBooks();

        }

        [HttpGet(nameof(GetById))]
        public Book GetById(int id)
        {
            return BookService.GetById(id);

        }

        [HttpPost(nameof(AddBook))]
        public Book AddBook(Book book)
        {
            BookService.AddBook(book);
            return book;
        }

        [HttpPost(nameof(UpdateBook))]
        public Book UpdateBook(Book book)
        {
            BookService.UpdateBook(book);
            return book;
        }

        

    }
}