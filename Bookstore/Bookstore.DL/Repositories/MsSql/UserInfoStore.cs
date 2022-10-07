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
    public class UserInfoStore : IUserPasswordStore<UserInfo>
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly IConfiguration _configuration;

        public UserInfoStore(IConfiguration configuration, ILogger<EmployeeRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [UserInfo] (UserId, DisplayName, UserName, Email, Password, CreatedDate) VALUES(@UserId, @DisplayName, @UserName, @Email, @Password, @BirthDate, @CreatedDate)", user);

                    return IdentityResult.Success;
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
            throw new NotImplementedException();
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

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserName = @UserName", new { UserName = userName });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(FindByNameAsync)}: {e.Message}", e);
            }
            return new UserInfo();
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }

        public Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
