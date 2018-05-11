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
    [Route("api/Departments")]
    public class DepartmentsController : Controller
    {
        private readonly InventoryContext _context;

        public DepartmentsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments;
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Department = await _context.Departments.SingleOrDefaultAsync(m => m.ID == id);

            if (Department == null)
            {
                return NotFound();
            }

            return Ok(Department);
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment([FromRoute] int id, [FromBody] Department Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Department.ID)
            {
                return BadRequest();
            }

            _context.Entry(Department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromBody] Department Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Departments.Add(Department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = Department.ID }, Department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Department = await _context.Departments.SingleOrDefaultAsync(m => m.ID == id);
            if (Department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(Department);
            await _context.SaveChangesAsync();

            return Ok(Department);
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.ID == id);
        }
    }
}