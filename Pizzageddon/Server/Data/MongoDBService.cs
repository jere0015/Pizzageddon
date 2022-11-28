using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace Pizzageddon.Server.Data
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Pizza> _pizzaCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _pizzaCollection = database.GetCollection<Pizza>(mongoDBSettings.Value.CollectionName);
        }

        public async Task CreateAsync(Pizza pizza)
        {
            await _pizzaCollection.InsertOneAsync(pizza);
            return;
        }

        public async Task<List<Pizza>> GetAsync()
        {
            return await _pizzaCollection.Find(new BsonDocument()).ToListAsync();
        }
        
        public async Task AddToPizzaAsync(string id)
        {
            FilterDefinition<Pizza> filter = Builders<Pizza>.Filter.Eq("id", id);
            UpdateDefinition<Pizza> update = Builders<Pizza>.Update.AddToSet("id", id);
            await _pizzaCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Pizza> filter = Builders<Pizza>.Filter.Eq("id", id);
            await _pizzaCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
