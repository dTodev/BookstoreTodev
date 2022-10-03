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
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person> GetAllUsers()
        {
            return _personRepository.GetAllUsers();
        }

        public Person? AddUser(Person user)
        {
            return _personRepository.AddUser(user);
        }

        public Person DeleteUser(int userId)
        {
            return _personRepository.DeleteUser(userId);
        }
        
        public Person? GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public Person UpdateUser(Person user)
        {
            return UpdateUser(user);
        }
    }
}
