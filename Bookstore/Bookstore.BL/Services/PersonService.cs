using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;

namespace Bookstore.BL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personService;

        public PersonService(IPersonRepository personRepository)
        {
            _personService = personRepository;
        }

        public IEnumerable<Person> GetAllUsers()
        {
            return _personService.GetAllUsers();
        }

        public Person? AddUser(Person user)
        {
            return _personService.AddUser(user);
        }

        public Person DeleteUser(int userId)
        {
            return _personService.DeleteUser(userId);
        }
        
        public Person? GetById(int id)
        {
            return _personService.GetById(id);
        }

        public Guid GetGuidId()
        {
            return Guid.NewGuid();
        }

        public Person UpdateUser(Person user)
        {
            return UpdateUser(user);
        }
    }
}
