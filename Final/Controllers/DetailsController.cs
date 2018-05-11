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
    [Route("api/Details")]
    public class DetailsController : Controller
    {
        private readonly InventoryContext _context;

        public DetailsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/Details
        [HttpGet]
        public IEnumerable<Detail> GetDetails()
        {
            return _context.Details;
        }

        // GET: api/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detail = await _context.Details.SingleOrDefaultAsync(m => m.ID == id);

            if (detail == null)
            {
                return NotFound();
            }

            return Ok(detail);
        }

        // PUT: api/Details/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetail([FromRoute] int id, [FromBody] Detail detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detail.ID)
            {
                return BadRequest();
            }

            _context.Entry(detail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailExists(id))
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

        // POST: api/Details
        [HttpPost]
        public async Task<IActionResult> PostDetail([FromBody] Detail detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Details.Add(detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetail", new { id = detail.ID }, detail);
        }

        // DELETE: api/Details/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var detail = await _context.Details.SingleOrDefaultAsync(m => m.ID == id);
            if (detail == null)
            {
                return NotFound();
            }

            _context.Details.Remove(detail);
            await _context.SaveChangesAsync();

            return Ok(detail);
        }

        private bool DetailExists(int id)
        {
            return _context.Details.Any(e => e.ID == id);
        }
    }
}