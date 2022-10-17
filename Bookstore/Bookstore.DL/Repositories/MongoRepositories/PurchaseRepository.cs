using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;
using Bookstore.Models.Models.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bookstore.DL.Repositories.MongoRepositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbSettings;
        private readonly IMongoCollection<Purchase> _databaseCollection;

        public PurchaseRepository(IOptionsMonitor<MongoDbConfiguration> mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
            MongoClient _dbClient = new MongoClient(_mongoDbSettings.CurrentValue.ConnectionString);
            var database = _dbClient.GetDatabase(_mongoDbSettings.CurrentValue.DatabaseName);
            _databaseCollection = database.GetCollection<Purchase>(_mongoDbSettings.CurrentValue.PurchaseCollectionName);
        }

        public Task SavePurchase(Purchase purchase)
        {
            _databaseCollection.InsertOne(purchase);
            return Task.CompletedTask;
        }

        public Task DeletePurchase(Purchase purchase)
        {
            _databaseCollection.DeleteOneAsync(x => x.Id == purchase.Id);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Purchase>> GetPurchases(int userId)
        {
            var result = (await _databaseCollection.FindAsync(x => x.UserId == userId)).ToList();
            return result;
        }
    }
}
