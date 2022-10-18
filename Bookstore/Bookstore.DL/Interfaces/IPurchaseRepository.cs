﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;

namespace Bookstore.DL.Interfaces
{
    public interface IPurchaseRepository
    {
        Task SavePurchase(Purchase purchase);
        Task DeletePurchase(Purchase purchase);
        Task<IEnumerable<Purchase>> GetPurchases(int userId);
    }
}
