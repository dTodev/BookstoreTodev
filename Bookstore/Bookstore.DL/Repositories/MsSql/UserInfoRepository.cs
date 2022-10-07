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
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly ILogger<UserInfoRepository> _logger;
        private readonly IConfiguration _configuration;

        public UserInfoRepository(ILogger<UserInfoRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE Email = @Email, Password = @Password", new { Email = email, Password = password });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetUserInfoAsync)}: {e.Message}", e);
            }
            return new UserInfo();
        }
    }
}
