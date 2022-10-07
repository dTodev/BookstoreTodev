using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models.Users;

namespace Bookstore.BL.Interfaces
{
    public interface IUserInfoService
    {
        public Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
