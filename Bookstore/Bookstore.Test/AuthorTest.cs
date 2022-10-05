using System.Xml.Linq;
using AutoMapper;
using Bookstore.AutoMapper;
using Bookstore.BL.Services;
using Bookstore.Controllers;
using Bookstore.DL.Interfaces;
using Bookstore.Models;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;

namespace Bookstore.Test
{
    public class AuthorTest
    {
        private IList<Author> _authors = new List<Author>()
        {
            new Author()
                {
                Id = 1,
                Age = 60,
                DateOfBirth = DateTime.Now,
                Name = "Autor Name",
                NickName = "NickName"
                },
            new Author{
                Id = 2,
                Age = 50,
                DateOfBirth = DateTime.Now,
                Name = "Another Name",
                NickName = "Another NickName"
                }
        };

        private readonly IMapper _mapper;
        private Mock<ILogger<AuthorService>> _loggerMock;
        private Mock<ILogger<AuthorController>> _loggerAuthorControllerMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;

        public AuthorTest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapping());
                });

            _mapper = mockMapperConfig.CreateMapper();

            _loggerMock = new Mock<ILogger<AuthorService>>();
            _loggerAuthorControllerMock = new Mock<ILogger<AuthorController>>();

            _authorRepositoryMock = new Mock<IAuthorRepository>();

            _bookRepositoryMock = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task Author_GetAll_Count_Check()
        {
            //Setup
            var expectedCount = 2;

            _authorRepositoryMock.Setup(x => x.GetAllAuthors()).ReturnsAsync(_authors);

            //Inject
            var service = new AuthorService(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //Act
            var result = await controller.GetAllAuthors();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var authors = okObjectResult.Value as IEnumerable<Author>;
            Assert.NotNull(authors);
            Assert.NotEmpty(authors);
            Assert.Equal(expectedCount, authors.Count());
        }

        [Fact]
        public async Task Author_GetAuthorById_Ok()
        {
            //Setup
            var authorId = 1;
            var expectedAuthor = _authors.First(x => x.Id == authorId);
            _authorRepositoryMock.Setup(x => x.GetById(authorId)).ReturnsAsync(expectedAuthor);

            //Inject
            var service = new AuthorService(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //Act
            var result = await controller.GetById(authorId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var author = okObjectResult.Value as Author;
            Assert.NotNull(author);
            Assert.Equal(authorId, author.Id);
        }

        [Fact]
        public async Task Author_GetAuthorById_NotFound()
        {
            //Setup
            var authorId = 3;
            _authorRepositoryMock.Setup(x => x.GetById(authorId)).ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

            //Inject
            var service = new AuthorService(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //Act
            var result = await controller.GetById(authorId);

            //Assert
            var notFoundObjectResult = result as NotFoundResult;
            Assert.NotNull(notFoundObjectResult);
        }

        [Fact]
        public async Task AddAuthorOk()
        {
            //Setup
            var authorRequest = new AddAuthorRequest()
            {
                NickName = "New nickname",
                Age = 22,
                DateOfBirth = DateTime.Now,
                Name = "Test Author Name"
            };

            var expectedAuthors = 3;

            _authorRepositoryMock.Setup(x => x.AddAuthor(It.IsAny<Author>())).Callback(() =>
            {
                _authors.Add(new Author()
                {
                    Id = 3,
                    Name = authorRequest.Name,
                    Age = authorRequest.Age,
                    DateOfBirth = authorRequest.DateOfBirth,
                    NickName = authorRequest.NickName
                });
            })!.ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == expectedAuthors));

            //Inject
            var service = new AuthorService(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //Act
            var result = await controller.AddAuthor(authorRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var resultValue = okObjectResult.Value as AddAuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(expectedAuthors, resultValue.Author.Id);
        }

        [Fact]
        public async Task Author_AddAuthorWhenExist()
        {
            //Setup
            var authorRequest = new AddAuthorRequest()
            {
                Age = 60,
                DateOfBirth = DateTime.Now,
                Name = "Autor Name",
                NickName = "NickName"
            };

            _authorRepositoryMock.Setup(x => x.GetAuthorByName(authorRequest.Name))!.ReturnsAsync(_authors.FirstOrDefault(x => x.Name == authorRequest.Name));

            //Inject
            var service = new AuthorService(_authorRepositoryMock.Object, _bookRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //Act
            var result = await controller.AddAuthor(authorRequest);

            //Assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);

            var resultValue = badRequestObjectResult.Value as AddAuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Author already exist", resultValue.Message);
        }
    }
}