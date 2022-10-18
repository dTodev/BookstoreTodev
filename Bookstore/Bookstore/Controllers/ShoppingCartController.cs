using System;
using Bookstore.BL.Interfaces;
using Bookstore.BL.Services;
using Bookstore.Models.Models;
using Bookstore.Models.Models.Users;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shopingCartService)
        {
            _shoppingCartService = shopingCartService;
        }

        [HttpPost(nameof(AddToCart))]
        public async Task<IActionResult> AddToCart(int userId, IEnumerable<Book> itemsForCart)
        {
            await _shoppingCartService.AddToCart(userId, itemsForCart);
            return Ok();
        }

        [HttpPost(nameof(RemoveFromCart))]
        public async Task<IActionResult> RemoveFromCart(int userId, IEnumerable<Book> itemsForCart)
        {
            await _shoppingCartService.RemoveFromCart(userId, itemsForCart);
            return Ok();
        }

        [HttpGet(nameof(GetContent))]
        public async Task<IActionResult> GetContent(int userId)
        {
            return Ok(await _shoppingCartService.GetContent(userId));
        }

        [HttpPost(nameof(FinishPurchase))]
        public async Task<IActionResult> FinishPurchase(int userId)
        {
            await _shoppingCartService.FinishPurchase(userId);
            return Ok();
        }

        [HttpPost(nameof(EmptyCart))]
        public async Task<IActionResult> EmptyCart()
        {
            _shoppingCartService.EmptyCart();
            return Ok();
        }

        [HttpPost(nameof(SaveCartContentSessionById))]
        public async Task<IActionResult> SaveCartContentSessionById(int userId)
        {
            await _shoppingCartService.SaveCurrentCartContentToDB(userId);
            return Ok();
        }

        [HttpGet(nameof(RetrieveCartContentSessionById))]
        public async Task<IActionResult> RetrieveCartContentSessionById(Guid Id)
        {
            await _shoppingCartService.RetrieveCartContentFromDB(Id);
            return Ok();
        }
    }
}
