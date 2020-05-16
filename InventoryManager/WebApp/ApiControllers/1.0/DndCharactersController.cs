using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.V1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DndCharactersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public DndCharactersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/DndCharacters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DndCharacter>>> GetDndCharacters()
        {
            var dndCharacters = (await _bll.DndCharacters.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new DndCharacter()
                {
                    Id = bllEntity.Id,
                    Name = bllEntity.Name,
                    OtherEquipment = bllEntity.OtherEquipment.Select(e => new OtherEquipment()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Comment = e.Comment,
                        Weight = e.Weight,
                        ValueInGp = e.ValueInGp,
                        Quantity = e.Quantity
                    }).ToArray()
                });

            return Ok(dndCharacters);
        }

        // GET: api/DndCharacters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DndCharacter>> GetDndCharacter(Guid id)
        {
            var dndCharacter = await _bll.DndCharacters.FirstOrDefaultAsync(id, User.UserGuidId());

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
