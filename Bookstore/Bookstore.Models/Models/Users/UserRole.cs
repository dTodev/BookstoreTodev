using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Models.Models.Users
{
    public class UserRole : IdentityRole
    {
        public int UserId { get; set; }
    }
}
