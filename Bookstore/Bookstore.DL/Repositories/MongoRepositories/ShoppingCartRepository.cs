using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models.Configurations;
using Bookstore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Bookstore.DL.Interfaces;

namespace Bookstore.DL.Repositories.MongoRepositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbSettings;
        private readonly IMongoCollection<ShoppingCart> _databaseCollection;

        public ShoppingCartRepository(IOptionsMonitor<MongoDbConfiguration> mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
            MongoClient _dbClient = new MongoClient(_mongoDbSettings.CurrentValue.ConnectionString);
            var database = _dbClient.GetDatabase(_mongoDbSettings.CurrentValue.DatabaseName);
            _databaseCollection = database.GetCollection<ShoppingCart>(_mongoDbSettings.CurrentValue.ShoppingCartCollectionName);
        }

        public Task SaveCurrentCartContentToDB(ShoppingCart cartForSaving)
        {
            _databaseCollection.InsertOne(cartForSaving);
            return Task.CompletedTask;
        }

        public async Task<ShoppingCart> RetrieveCartContentFromDB(Guid Id)
        {
            var result = (await _databaseCollection.FindAsync(x => x.Id == Id));
            return result.FirstOrDefault();
        }
    }
}
