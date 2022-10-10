using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bookstore.DL.Repositories.MsSql
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>, IUserRoleStore<UserInfo>
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;

        public UserInfoStore(IConfiguration configuration, ILogger<EmployeeRepository> logger, IPasswordHasher<UserInfo> passwordHasher)
        {
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    user.Password = _passwordHasher.HashPassword(user, user.Password);

                    var result = await conn.ExecuteAsync("INSERT INTO [UserInfo] (DisplayName, UserName, Email, Password, CreatedDate) VALUES(@DisplayName, @UserName, @Email, @Password, @CreatedDate)", user);

                    return result > 0 ? IdentityResult.Success : IdentityResult.Failed();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(CreateAsync)}: {e.Message}", e);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "Error in creation"
            });
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserName = @UserName", new { UserName = userName });

                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(FindByNameAsync)}:{e.Message}");

            }
            return null;
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
             await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserId = @UserId", new { UserId = user.UserId });

                    return result?.UserId.ToString();
                }
        }

        public Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            await conn.ExecuteAsync("UPDATE UserInfo SET Password = @passwordHash WHERE UserId = @UserId", new { user.UserId, passwordHash });
        }

        public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<string>("SELECT Password FROM UserInfo WITH (NOLOCK) WHERE UserId = @UserId", new { user.UserId });
        }

        public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IUserRoleStore<UserInfo>.AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IUserRoleStore<UserInfo>.RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            var result = await conn.QueryAsync<string>("SELECT r.RoleName as Name FROM Roles r WHERE r.Id IN (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @UserId)", new { UserId = user.UserId });

            return result.ToList();
        }

        Task<bool> IUserRoleStore<UserInfo>.IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IList<UserInfo>> IUserRoleStore<UserInfo>.GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
