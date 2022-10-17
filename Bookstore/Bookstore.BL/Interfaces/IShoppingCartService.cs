using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;

namespace Bookstore.BL.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<Purchase> GetContent(int userId);
        public Task AddToCart(int userId, IEnumerable<Book> itemsForCart);
        public Task RemoveFromCart(int userId, IEnumerable<Book> itemsForRemoval);
        public void EmptyCart();
        public Task FinishPurchase(int userId);
        public Task SaveCurrentCartContentToDB(int userId);
        public Task<ShoppingCart> RetrieveCartContentFromDB(Guid Id);
    }
}
