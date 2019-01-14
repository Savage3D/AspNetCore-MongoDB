using CafesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafesApi.Services
{
    public interface ICafeService
    {
        Task<IEnumerable<Cafe>> GetAllCafesAsync();
        Task<Cafe> GetCafeByIdAsync(string id);
        Task<Cafe> CreateCafeAsync(CreatedCafeDto createdCafe);
        Task<bool> UpdateCafeAsync(string id, Cafe updatedCafe);
        Task<bool> ModifyCafeAsync(string id, JsonPatchDocument<Cafe> modifiedCafe);
        Task<bool> DeleteCafeAsync(string id);
    }
}
