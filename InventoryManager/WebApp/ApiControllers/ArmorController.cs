using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArmorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArmorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Armor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Armor>>> GetArmors()
        {
            return await _context.Armors.Where(a => a.AppUserId == User.UserGuidId()).ToListAsync();
        }

        // GET: api/Armor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Armor>> GetArmor(Guid id)
        {
            var armor = await _context.Armors.FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == User.UserGuidId());

            if (armor == null)
            {
                return NotFound();
            }

            return armor;
        }

        // PUT: api/Armor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArmor(Guid id, Armor armor)
        {
            if (id != armor.Id)
            {
                return BadRequest();
            }

            _context.Entry(armor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArmorExists(id))
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

        // POST: api/Armor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Armor>> PostArmor(Armor armor)
        {
            _context.Armors.Add(armor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArmor", new { id = armor.Id }, armor);
        }

        // DELETE: api/Armor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Armor>> DeleteArmor(Guid id)
        {
            var armor = await _context.Armors.FindAsync(id);
            
            if (armor == null)
            {
                return NotFound();
            }

            _context.Armors.Remove(armor);
            await _context.SaveChangesAsync();

            return armor;
        }

        private bool ArmorExists(Guid id)
        {
            return _context.Armors.Any(e => e.Id == id);
        }
    }
}
