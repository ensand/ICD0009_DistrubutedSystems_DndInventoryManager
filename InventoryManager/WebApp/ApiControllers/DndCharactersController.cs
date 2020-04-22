using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class DndCharactersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DndCharactersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DndCharacters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DndCharacter>>> GetDndCharacters()
        {
            return await _context.DndCharacters.Where(o => o.AppUserId == User.UserGuidId()).ToListAsync();
        }

        // GET: api/DndCharacters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DndCharacter>> GetDndCharacter(Guid id)
        {
            var dndCharacter = await _context.DndCharacters.FirstOrDefaultAsync(o => o.Id == id && o.AppUserId == User.UserGuidId());

            if (dndCharacter == null)
            {
                return NotFound();
            }

            return dndCharacter;
        }

        // PUT: api/DndCharacters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDndCharacter(Guid id, DndCharacter dndCharacter)
        {
            if (id != dndCharacter.Id)
            {
                return BadRequest();
            }

            _context.Entry(dndCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DndCharacterExists(id))
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

        // POST: api/DndCharacters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DndCharacter>> PostDndCharacter(DndCharacter dndCharacter)
        {
            _context.DndCharacters.Add(dndCharacter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDndCharacter", new { id = dndCharacter.Id }, dndCharacter);
        }

        // DELETE: api/DndCharacters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DndCharacter>> DeleteDndCharacter(Guid id)
        {
            var dndCharacter = await _context.DndCharacters.FindAsync(id);
            if (dndCharacter == null)
            {
                return NotFound();
            }

            _context.DndCharacters.Remove(dndCharacter);
            await _context.SaveChangesAsync();

            return dndCharacter;
        }

        private bool DndCharacterExists(Guid id)
        {
            return _context.DndCharacters.Any(e => e.Id == id);
        }
    }
}
