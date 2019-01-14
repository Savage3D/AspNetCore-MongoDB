using CafesApi.Infrastructure;
using CafesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafesApi.Services
{
    public class CafeService : ICafeService
    {
        private readonly CafesDbContext _context;

        public CafeService(CafesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cafe>> GetAllCafesAsync()
        {
            var filter = Builders<Cafe>.Filter.Empty;
            return await _context.Cafes.Find(filter).ToListAsync();
        }

        public async Task<Cafe> GetCafeByIdAsync(string id)
        {
            if (!IsIdValid(id)) return null;

            var filter = Builders<Cafe>.Filter.Eq(c => c.Id, id);
            return await _context.Cafes.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Cafe> CreateCafeAsync(CreatedCafeDto createdCafe)
        {
            Cafe cafe = new Cafe
            {
                Name = createdCafe.Name,
                Description = createdCafe.Description,
                Tags = createdCafe.Tags
            };

            await _context.Cafes.InsertOneAsync(cafe);
            return cafe;
        }

        public async Task<bool> UpdateCafeAsync(string id, Cafe updatedCafe)
        {
            if (!IsIdValid(id)) return false;

            var filter = Builders<Cafe>.Filter.Eq(c => c.Id, id);
            var result = await _context.Cafes.ReplaceOneAsync(filter, updatedCafe);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> ModifyCafeAsync(string id, JsonPatchDocument<Cafe> modifiedCafe)
        {
            if (!IsIdValid(id)) return false;

            var cafeToModify = await GetCafeByIdAsync(id);
            modifiedCafe.ApplyTo(cafeToModify);

            return await UpdateCafeAsync(id, cafeToModify);
        }

        public async Task<bool> DeleteCafeAsync(string id)
        {
            if (!IsIdValid(id)) return false;

            var filter = Builders<Cafe>.Filter.Eq(c => c.Id, id);
            var result = await _context.Cafes.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        private bool IsIdValid(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
