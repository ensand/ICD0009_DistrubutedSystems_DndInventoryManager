using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagicalItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MagicalItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MagicalItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MagicalItem>>> GetMagicalItems()
        {
            return await _context.MagicalItems.ToListAsync();
        }

        // GET: api/MagicalItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MagicalItem>> GetMagicalItem(Guid id)
        {
            var magicalItem = await _context.MagicalItems.FindAsync(id);

            if (magicalItem == null)
            {
                return NotFound();
            }

            return magicalItem;
        }

        // PUT: api/MagicalItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagicalItem(Guid id, MagicalItem magicalItem)
        {
            if (id != magicalItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(magicalItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagicalItemExists(id))
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

        // POST: api/MagicalItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MagicalItem>> PostMagicalItem(MagicalItem magicalItem)
        {
            _context.MagicalItems.Add(magicalItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMagicalItem", new { id = magicalItem.Id }, magicalItem);
        }

        // DELETE: api/MagicalItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MagicalItem>> DeleteMagicalItem(Guid id)
        {
            var magicalItem = await _context.MagicalItems.FindAsync(id);
            if (magicalItem == null)
            {
                return NotFound();
            }

            _context.MagicalItems.Remove(magicalItem);
            await _context.SaveChangesAsync();

            return magicalItem;
        }

        private bool MagicalItemExists(Guid id)
        {
            return _context.MagicalItems.Any(e => e.Id == id);
        }
    }
}
