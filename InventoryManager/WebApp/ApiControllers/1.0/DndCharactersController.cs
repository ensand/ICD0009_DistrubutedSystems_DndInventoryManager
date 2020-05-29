using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
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
        // private readonly IAppBLL _bll;
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// API controller constructor for initializing data source.
        /// </summary>
        /// <param name="uow"></param>
        public DndCharactersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/DndCharacters
        /// <summary>
        /// Get all DnDCharacters with no details
        /// </summary>
        /// <returns>All the characters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DndCharacter>>> GetDndCharacters()
        {
            var dndCharacters = await _uow.DndCharacters.GetAllAsync(User.UserGuidId());
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
            var characterDetails = await _uow.DndCharacters.FirstOrDefaultAsync(id, User.UserGuidId());

            if (characterDetails == null)
                return NotFound("Character with id '" + id + "' was not found.");
            
            return Ok(characterDetails);
        }
        
        // PUT: api/DndCharacters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDndCharacter(Guid id, DAL.App.DTO.DndCharacter dndCharacter)
        {
            if (id != dndCharacter.Id)
            {
                return BadRequest("Id and dndCharacter.id do not match");
            }

            if (!await _uow.DndCharacters.ExistsAsync(dndCharacter.Id, User.UserGuidId()))
            {
                return NotFound("Current user does not have a character with this id: "+ dndCharacter.Id);
            }

            dndCharacter.AppUserId = User.UserGuidId();
            await _uow.DndCharacters.UpdateAsync(dndCharacter);
            await _uow.SaveChangesAsync();
            
            return NoContent();

        }
        
        // POST: api/DndCharacters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DndCharacter>> PostDndCharacter(DAL.App.DTO.DndCharacter dndCharacter)
        // public async void PostDndCharacter(DAL.App.DTO.DndCharacter dndCharacter)
        {
            _uow.DndCharacters.Add(dndCharacter);
            await _uow.SaveChangesAsync();
        
            return CreatedAtAction("GetDndCharacter", new { id = dndCharacter.Id }, dndCharacter);
        }
        
        // DELETE: api/DndCharacters/5
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
    }
}
