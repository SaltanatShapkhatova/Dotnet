using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;

namespace Final.Controllers
{
    [Produces("application/json")]
    [Route("api/Inventories")]
    public class InventoriesController : Controller
    {
        private readonly InventoryContext _context;

        public InventoriesController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/Inventories
        [HttpGet]
        public IEnumerable<Inventory> GetInventories()
        {
            return _context.Inventories;
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inv = await _context.Inventories.SingleOrDefaultAsync(m => m.ID == id);

            if (inv == null)
            {
                return NotFound();
            }

            return Ok(inv);
        }

        // PUT: api/Inventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory([FromRoute] int id, [FromBody] Inventory inv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inv.ID)
            {
                return BadRequest();
            }

            _context.Entry(inv).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inventories
        //[HttpPost]
        //public async Task<IActionResult> PostRent([FromBody] Rent rent)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Inventories.Add(rent);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRent", new { id = rent.ID }, rent);
        //}
        [HttpPost]
        public void PostPerson([FromBody] Inventory inv)
        {
            _context.Inventories.Add(inv);
            _context.SaveChanges();
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inv = await _context.Inventories.SingleOrDefaultAsync(m => m.ID == id);
            if (inv == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inv);
            await _context.SaveChangesAsync();

            return Ok(inv);
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.ID == id);
        }
    }
}