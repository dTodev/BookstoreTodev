using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.InMemoryRepositories;
using Bookstore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService PersonService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            this.PersonService = personService;
        }

        [HttpGet(nameof(Get))]
        public IEnumerable<Person> Get()
        {
            return PersonService.GetAllUsers();
        }

        [HttpGet(nameof(GetById))]
        public Person GetById(int id)
        {
            return PersonService.GetById(id);
        }

        [HttpPost(nameof(AddUser))]
        public Person AddUser(Person user)
        {
            PersonService.AddUser(user);
            return user;
        }

        [HttpPost(nameof(UpdateUser))]
        public Person UpdateUser(Person user)
        {
            PersonService.UpdateUser(user);
            return user;
        }
    }
}