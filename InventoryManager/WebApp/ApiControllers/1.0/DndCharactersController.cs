using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using V1DTO = PublicApi.DTO.V1;
using PublicApi.DTO.V1.Mappers;

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
        private readonly DndCharacterMapper _mapper = new DndCharacterMapper();
        
        /// <summary>
        /// API controller constructor for initializing data source.
        /// </summary>
        /// <param name="bll"></param>
        public DndCharactersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/DndCharacters
        /// <summary>
        /// Get all DnDCharacters, does not include the details of the equipment.
        /// </summary>
        /// <returns>All the characters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.DndCharacterSummary>>> GetDndCharacters()
        {
            var dndCharacters = await _bll.DndCharacters.CustomGetAllAsync(User.UserGuidId());
            var mappedCharacters = dndCharacters.Select(c => _mapper.MapDndCharacterSummary(c));
            
            return Ok(mappedCharacters);
        }

        // GET: api/DndCharacters/5
        /// <summary>
        /// Get the specific DnDCharacter with all the details of the equipment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Character by ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.DndCharacter>> GetDndCharacter(Guid id)
        {
            var character = await _bll.DndCharacters.CustomFirstOrDefaultAsync(id, User.UserGuidId());

            if (character == null)
                return NotFound("Character with id '" + id + "' was not found.");

            var mappedCharacter = _mapper.MapDndCharacter(character);
            
            return Ok(mappedCharacter);
        }
        
        // PUT: api/DndCharacters/5
        // To protect from over posting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the character details (not for the equipment details)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dndCharacter"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDndCharacter(Guid id, V1DTO.DndCharacterUpdate dndCharacter)
        {
            if (id != dndCharacter.Id)
                return BadRequest();

            if (!await _bll.DndCharacters.ExistsAsync(dndCharacter.Id, User.UserGuidId()))
                return NotFound();

            dndCharacter.AppUserId = User.UserGuidId();
            var mappedCharacter = _mapper.MapDndCharacterUpdateToBll(dndCharacter);

            await _bll.DndCharacters.UpdateAsync(mappedCharacter, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok();

        }
        
        // POST: api/DndCharacters
        // To protect from over posting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new character
        /// </summary>
        /// <param name="dndCharacter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostDndCharacter(V1DTO.NewDndCharacter dndCharacter)
        {
            var bllChar = _mapper.MapDndCharacterNewToBll(dndCharacter);
            _bll.DndCharacters.Add(bllChar);
            await _bll.SaveChangesAsync();
        
            return Ok(bllChar.Id);
        }
        
        // DELETE: api/DndCharacters/5
        /// <summary>
        /// Delete a character, uses cascade delete to delete the items as well.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.DndCharacter>> DeleteDndCharacter(Guid id)
        {
            var dndCharacter = await _bll.DndCharacters.CustomFirstOrDefaultAsync(id, User.UserGuidId());
            if (dndCharacter == null)
                return NotFound();

            await _bll.DndCharacters.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok(id);
        }
    }
}
