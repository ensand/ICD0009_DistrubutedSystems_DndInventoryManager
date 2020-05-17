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
    /// <summary>
    /// DndCharacters API
    /// </summary>
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
        /// <summary>
        /// Get all DnDCharacters with no details
        /// </summary>
        /// <returns>All the characters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DndCharacter>>> GetDndCharacters()
        {
            var dndCharacters = (await _bll.DndCharacters.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new DndCharacter()
                {
                    Id = bllEntity.Id,
                    Name = bllEntity.Name,
                    Comment = bllEntity.Comment,
                    ArmorCount = bllEntity.ArmorCount,
                    MagicalItemCount = bllEntity.MagicalItemCount,
                    OtherEquipmentCount = bllEntity.OtherEquipmentCount,
                    WeaponCount = bllEntity.WeaponCount,
                    TreasureInGp = bllEntity.TreasureInGp
                });
        
            return Ok(dndCharacters);
        }

        // GET: api/DndCharacters/5
        /// <summary>
        /// Get the specific DnDCharacter with all the details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Character by ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DndCharacterDetails>> GetDndCharacter(Guid id)
        {
            var dndCharacter = await _bll.DndCharacters.FirstOrDefaultAsync(id, User.UserGuidId());

            if (dndCharacter == null)
            {
                return NotFound(id);
            }
            
            var characterDetails = new DndCharacterDetails()
            {
                Id = dndCharacter.Id,
                Name = dndCharacter.Name,
                Comment = dndCharacter.Comment,
                PlatinumPieces = dndCharacter.PlatinumPieces,
                GoldPieces = dndCharacter.GoldPieces,
                ElectrumPieces = dndCharacter.ElectrumPieces,
                SilverPieces = dndCharacter.SilverPieces,
                CopperPieces = dndCharacter.CopperPieces,
                MagicalItemCount = dndCharacter.MagicalItemCount,
                OtherEquipmentCount = dndCharacter.OtherEquipmentCount,
                WeaponCount = dndCharacter.WeaponCount,
                TreasureInGp = dndCharacter.TreasureInGp,
                AllItemsValueInGp = dndCharacter.AllItemsValueInGp,
                AllItemsWeight =  dndCharacter.AllItemsWeight,
                // Armor = dndCharacter.Armor,
                // MagicalItems = dndCharacter.MagicalItems,
                // Weapons = dndCharacter.Weapons,
                // OtherEquipment = dndCharacter.OtherEquipment
            };
            
            return Ok(characterDetails);
        }
        
        // // PUT: api/DndCharacters/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutDndCharacter(Guid id, DndCharacter dndCharacter)
        // {
        //     if (id != dndCharacter.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(dndCharacter).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!DndCharacterExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        // // POST: api/DndCharacters
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<DndCharacter>> PostDndCharacter(DndCharacter dndCharacter)
        // {
        //     _context.DndCharacters.Add(dndCharacter);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetDndCharacter", new { id = dndCharacter.Id }, dndCharacter);
        // }
        //
        // // DELETE: api/DndCharacters/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<DndCharacter>> DeleteDndCharacter(Guid id)
        // {
        //     var dndCharacter = await _context.DndCharacters.FindAsync(id);
        //     
        //     if (dndCharacter == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.DndCharacters.Remove(dndCharacter);
        //     await _context.SaveChangesAsync();
        //
        //     return dndCharacter;
        // }
        //
        // private bool DndCharacterExists(Guid id)
        // {
        //     return _context.DndCharacters.Any(e => e.Id == id);
        // }
    }
}
