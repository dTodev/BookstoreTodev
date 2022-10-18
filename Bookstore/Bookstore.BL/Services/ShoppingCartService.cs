using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;
using Bookstore.Models.Models.Configurations;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bookstore.BL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly Dictionary<int, Purchase> _usersAndPurchasesList;
        private readonly IPurchaseService _purchaseService;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IPurchaseService purchaseService, IShoppingCartRepository shoppingCartRepository)
        {
            _usersAndPurchasesList = new Dictionary<int, Purchase>();
            _purchaseService = purchaseService;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Purchase> GetContent(int userId)
        {
            var result = _usersAndPurchasesList.FirstOrDefault(x => x.Key == userId).Value;
            return result;
        }

        public async Task AddToCart(int userId, IEnumerable<Book> itemsForCart)
        {
            if (!_usersAndPurchasesList.ContainsKey(userId))
            {
                _usersAndPurchasesList.Add(userId, new Purchase()
                {
                    Id = new Guid(),
                    Books = itemsForCart.ToList(),
                    UserId = userId
                });
            }
            else
            {
                foreach (var item in itemsForCart)
                {
                    _usersAndPurchasesList[userId].Books.Add(item);
                }
            }
        }

        public async Task RemoveFromCart(int userId, IEnumerable<Book> itemsForRemoval)
        {
            if (_usersAndPurchasesList.ContainsKey(userId))
            {
                foreach (var item in itemsForRemoval)
                {
                    _usersAndPurchasesList[userId].Books.Remove(item);
                }
            }
        }

        public async Task FinishPurchase(int userId)
        {
            if (_usersAndPurchasesList.ContainsKey(userId))
            {
                var purchase = _usersAndPurchasesList[userId];
                await _purchaseService.SavePurchase(purchase);
                _usersAndPurchasesList.Remove(userId);
            }
        }

        public void EmptyCart()
        {
            _usersAndPurchasesList.Clear();
        }

        public Task SaveCurrentCartContentToDB(int userId)
        {
            ShoppingCart cartForSaving = new ShoppingCart()
            {
                Id = new Guid(),
                UserId = _usersAndPurchasesList[userId].UserId,
                Books = _usersAndPurchasesList[userId].Books,
            };

            _shoppingCartRepository.SaveCurrentCartContentToDB(cartForSaving);

            return Task.CompletedTask;
        }

        public async Task<ShoppingCart> RetrieveCartContentFromDB(Guid Id)
        {
            var result = await _shoppingCartRepository.RetrieveCartContentFromDB(Id);

            _usersAndPurchasesList.Add(result.UserId, new Purchase()
            {
                Id = result.Id,
                Books = result.Books,
                UserId = result.UserId
            });

            return result;
        }
    }
}
