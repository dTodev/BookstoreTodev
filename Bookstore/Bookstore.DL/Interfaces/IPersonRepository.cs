using Bookstore.Models.Models;

namespace Bookstore.DL.Interfaces
{
    public interface IPersonRepository
    {
        Person? AddUser(Person user);
        Person DeleteUser(int userId);
        IEnumerable<Person> GetAllUsers();
        Person? GetById(int id);
        Person UpdateUser(Person user);
        Guid GetGuidId();
    }
}