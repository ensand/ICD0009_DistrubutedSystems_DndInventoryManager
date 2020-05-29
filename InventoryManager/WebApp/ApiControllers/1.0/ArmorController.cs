using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArmorController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ArmorController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Armor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Armor>>> GetArmors()
        {
            var result = await _uow.Armors.GetAllAsync(User.UserGuidId());
            return Ok(result);
        }

        // GET: api/Armor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Armor>> GetArmor(Guid id)
        {
            var armor = await _uow.Armors.FirstOrDefaultAsync(id, User.UserGuidId());

            if (armor == null)
            {
                return NotFound();
            }

            return Ok(armor);
        }

        // PUT: api/Armor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArmor(Guid id, DAL.App.DTO.Armor armor)
        {
            if (id != armor.Id)
                return BadRequest();

            await _uow.Armors.UpdateAsync(armor, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Armor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Armor>> PostArmor(DAL.App.DTO.Armor armor)
        {
            _uow.Armors.Add(armor);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetArmor", new { id = armor.Id }, armor);
        }

        // DELETE: api/Armor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Armor>> DeleteArmor(Guid id)
        {
            var armor = await _uow.Armors.FirstOrDefaultAsync(id, User.UserGuidId());
            if (armor == null)
            {
                return NotFound();
            }

            await _uow.Armors.RemoveAsync(armor, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return Ok(armor);
        }
    }
}
