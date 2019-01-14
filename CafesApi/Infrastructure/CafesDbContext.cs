using CafesApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CafesApi.Infrastructure
{
    public class CafesDbContext
    {
        private readonly IMongoDatabase _db;

        public CafesDbContext(IOptions<DbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DbName);
        }

        public IMongoCollection<Cafe> Cafes => _db.GetCollection<Cafe>("Cafes");
    }
}
