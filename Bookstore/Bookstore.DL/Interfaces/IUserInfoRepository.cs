using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models.Users;

namespace Bookstore.DL.Interfaces
{
    public interface IUserInfoRepository
    {
        public Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
