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
    public class WeaponsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeaponsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Weapons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weapon>>> GetWeapons()
        {
            return await _context.Weapons.ToListAsync();
        }

        // GET: api/Weapons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weapon>> GetWeapon(Guid id)
        {
            var weapon = await _context.Weapons.FindAsync(id);

            if (weapon == null)
            {
                return NotFound();
            }

            return weapon;
        }

        // PUT: api/Weapons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeapon(Guid id, Weapon weapon)
        {
            if (id != weapon.Id)
            {
                return BadRequest();
            }

            _context.Entry(weapon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponExists(id))
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

        // POST: api/Weapons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Weapon>> PostWeapon(Weapon weapon)
        {
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeapon", new { id = weapon.Id }, weapon);
        }

        // DELETE: api/Weapons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Weapon>> DeleteWeapon(Guid id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();

            return weapon;
        }

        private bool WeaponExists(Guid id)
        {
            return _context.Weapons.Any(e => e.Id == id);
        }
    }
}
