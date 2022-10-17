using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;

namespace Bookstore.DL.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Task SaveCurrentCartContentToDB(ShoppingCart cartForSaving);
        public Task<ShoppingCart> RetrieveCartContentFromDB(Guid Id); 
    }
}
