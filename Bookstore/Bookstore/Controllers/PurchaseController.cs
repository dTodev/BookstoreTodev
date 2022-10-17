using Bookstore.BL.Interfaces;
using Bookstore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet(nameof(GetAllPurchasesById))]
        public async Task<IActionResult> GetAllPurchasesById(int userId)
        {
            return Ok(await _purchaseService.GetPurchases(userId));
        }

        [HttpPost(nameof(SavePurchase))]
        public async Task<IActionResult> SavePurchase(Purchase purchase)
        {
            await _purchaseService.SavePurchase(purchase);
            return Ok();
        }

        [HttpPost(nameof(DeletePurchase))]
        public async Task<IActionResult> DeletePurchase(Purchase purchase)
        {
            await _purchaseService.DeletePurchase(purchase);
            return Ok();
        }
    }
}
