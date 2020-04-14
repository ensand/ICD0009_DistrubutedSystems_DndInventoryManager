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
    public class OtherEquipmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OtherEquipmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OtherEquipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtherEquipment>>> GetOtherEquipments()
        {
            return await _context.OtherEquipments.ToListAsync();
        }

        // GET: api/OtherEquipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OtherEquipment>> GetOtherEquipment(Guid id)
        {
            var otherEquipment = await _context.OtherEquipments.FindAsync(id);

            if (otherEquipment == null)
            {
                return NotFound();
            }

            return otherEquipment;
        }

        // PUT: api/OtherEquipments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOtherEquipment(Guid id, OtherEquipment otherEquipment)
        {
            if (id != otherEquipment.Id)
            {
                return BadRequest();
            }

            _context.Entry(otherEquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtherEquipmentExists(id))
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

        // POST: api/OtherEquipments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OtherEquipment>> PostOtherEquipment(OtherEquipment otherEquipment)
        {
            _context.OtherEquipments.Add(otherEquipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOtherEquipment", new { id = otherEquipment.Id }, otherEquipment);
        }

        // DELETE: api/OtherEquipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OtherEquipment>> DeleteOtherEquipment(Guid id)
        {
            var otherEquipment = await _context.OtherEquipments.FindAsync(id);
            if (otherEquipment == null)
            {
                return NotFound();
            }

            _context.OtherEquipments.Remove(otherEquipment);
            await _context.SaveChangesAsync();

            return otherEquipment;
        }

        private bool OtherEquipmentExists(Guid id)
        {
            return _context.OtherEquipments.Any(e => e.Id == id);
        }
    }
}
