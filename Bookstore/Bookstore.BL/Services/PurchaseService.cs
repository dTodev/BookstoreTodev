using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;

namespace Bookstore.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task DeletePurchase(Purchase purchase)
        {
            await _purchaseRepository.DeletePurchase(purchase);
        }

        public async Task<IEnumerable<Purchase>> GetPurchases(int userId)
        {
            return await _purchaseRepository.GetPurchases(userId);
        }

        public async Task SavePurchase(Purchase purchase)
        {
            await _purchaseRepository.SavePurchase(purchase);
        }
    }
}
