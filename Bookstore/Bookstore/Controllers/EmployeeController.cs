using AutoMapper;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models.MediatR.Commands;
using Bookstore.Models.Models;
using Bookstore.Models.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(nameof(AddEmployee))]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            return Ok(await _employeeService.AddEmployee(employee));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            return Ok(await _employeeService.DeleteEmployee(Id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost(nameof(UpdateEmployee))]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            return Ok(await _employeeService.UpdateEmployee(employee));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetEmployeeDetailsById))]
        public async Task<IActionResult> GetEmployeeDetailsById(int Id)
        {
            return Ok(await _employeeService.GetEmployeeDetails(Id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetEmployeeDetails))]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            return Ok(await _employeeService.GetEmployeeDetails());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(CheckEmployee))]
        public async Task<IActionResult> CheckEmployee(int Id)
        {
            return Ok(await _employeeService.CheckEmployee(Id));
        }
    }
}
