using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;
using Bookstore.Models.Models.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bookstore.DL.Repositories.MsSql
{
    public class EmployeeRepository : IEmployeesRepository
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly IConfiguration _configuration;

        public EmployeeRepository(ILogger<EmployeeRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Employee?> AddEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Employee] (EmployeeID, NationalIDNumber, EmployeeName, LoginID, JobTitle, BirthDate, MaritalStatus, Gender, HireDate, VacationHours, SickLeaveHours, rowguid, ModifiedDate) VALUES(@EmployeeID, @NationalIDNumber, @EmployeeName, @LoginID, @JobTitle, @BirthDate, @MaritalStatus, @Gender, @HireDate, @VacationHours, @SickLeaveHours, @rowguid, @ModifiedDate)", employee);

                    return employee;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddEmployee)}: {e.Message}", e);
            }
            return new Employee();
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var temp = await GetEmployeeDetails(employeeId);

                    var result = await conn.ExecuteAsync("DELETE FROM [Employee] WHERE EmployeeID = @EmployeeID", temp);

                    return temp;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteEmployee)}: {e.Message}", e);
            }
            return new Employee();
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE [Employee] SET [EmployeeID] = @EmployeeID, [NationalIDNumber] = @NationalIDNumber, [EmployeeName] = @EmployeeName, [LoginID] = @LoginID, [JobTitle] = @JobTitle, [BirthDate] = @BirthDate, [MaritalStatus] = @MaritalStatus, [Gender] = @Gender, [HireDate] = @HireDate, [VacationHours] = @VacationHours, [SickLeaveHours] = @SickLeaveHours, [rowguid] = @rowguid, [ModifiedDate] = @ModifiedDate WHERE EmployeeID = @EmployeeID", employee);

                    return employee;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateEmployee)}: {e.Message}", e);
            }
            return new Employee();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WITH(NOLOCK) WHERE EmployeeID = @EmployeeID", new { EmployeeID = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeDetails)}: {e.Message}", e);
            }
            return new Employee();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Employee>("SELECT * FROM Employee WITH (NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeDetails)}: {e.Message}", e);
            }
            return Enumerable.Empty<Employee>();
        }

        public async Task<bool> CheckEmployee(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WITH(NOLOCK) WHERE EmployeeID = @EmployeeID", new { EmployeeID = id });

                    if(result == null)
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(CheckEmployee)}: {e.Message}", e);
            }
            return true;
        }
    }
}
