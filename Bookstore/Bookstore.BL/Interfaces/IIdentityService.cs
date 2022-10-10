using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.BL.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(UserInfo user);
        Task<UserInfo?> CheckUserAndPass(string userName, string password);

        public Task<IEnumerable<string>> GetUserRoles(UserInfo user);
    }
}
