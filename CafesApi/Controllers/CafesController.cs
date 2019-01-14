using CafesApi.Models;
using CafesApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CafesController : ControllerBase
    {
        private readonly ICafeService _cafeService;

        public CafesController(ICafeService cafeService)
        {
            _cafeService = cafeService;
        }

        // GET: api/cafes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cafe>>> GetAllCafesAsync()
        {
            return Ok(await _cafeService.GetAllCafesAsync());
        }

        // GET: api/cafes/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Cafe>> GetCafeByIdAsync(string id)
        {
            Cafe cafe = await _cafeService.GetCafeByIdAsync(id);
            if (cafe == null) return NotFound();
            return Ok(cafe);
        }

        // POST: api/cafes
        [HttpPost]
        public async Task<ActionResult<Cafe>> CreateCafeAsync([FromBody] CreatedCafeDto createdCafe)
        {
            return Ok(await _cafeService.CreateCafeAsync(createdCafe));
        }

        // PUT: api/cafes/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCafeAsync(string id, [FromBody] Cafe updatedCafe)
        {
            bool isUpdateSuccessful = await _cafeService.UpdateCafeAsync(id, updatedCafe);
            if (isUpdateSuccessful) return NoContent();
            return BadRequest();
        }

        // PATCH: api/cafes/id
        [HttpPatch("{id}")]
        public async Task<ActionResult> ModifyCafeAsync(string id, [FromBody] JsonPatchDocument<Cafe> modifiedCafe)
        {
            bool isModificationSuccessful = await _cafeService.ModifyCafeAsync(id, modifiedCafe);
            if (isModificationSuccessful) return NoContent();
            return BadRequest();
        }

        // DELETE: api/cafes/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCafeAsync(string id)
        {
            bool isDeletionSuccessful = await _cafeService.DeleteCafeAsync(id);
            if (isDeletionSuccessful) return NoContent();
            return BadRequest();
        }
    }
}
