using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models.Users;

namespace Bookstore.DL.Interfaces
{
    public interface IEmployeesRepository
    {
        public Task<Employee> AddEmployee(Employee employee);
        public Task<Employee> DeleteEmployee(int id);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<Employee?> GetEmployeeDetails(int id);
        public Task<IEnumerable<Employee>> GetEmployeeDetails();
        public Task<bool> CheckEmployee(int id);
    }
}
