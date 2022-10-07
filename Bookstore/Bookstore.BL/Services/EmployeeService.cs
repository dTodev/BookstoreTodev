using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.DL.Repositories.MsSql;
using Bookstore.Models;
using Bookstore.Models.Models;
using Bookstore.Models.Models.Users;

namespace Bookstore.BL.Services
{
    public class EmployeeService : IEmployeeService, IUserInfoService
    {
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IUserInfoRepository _userInfoRepository;

        public EmployeeService(IEmployeesRepository employeeRepository, IUserInfoRepository userInfoRepository)
        {
            _employeeRepository = employeeRepository;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await _employeeRepository.UpdateEmployee(employee);
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            return await _employeeRepository.GetEmployeeDetails(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            return await _employeeRepository.GetEmployeeDetails();
        }

        public async Task<bool> CheckEmployee(int id)
        {
            return await _employeeRepository.CheckEmployee(id);
        }

        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            return await _userInfoRepository.GetUserInfoAsync(email, password);
        }
    }
}
