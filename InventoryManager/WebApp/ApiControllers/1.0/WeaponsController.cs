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
    public class WeaponsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public WeaponsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Weapons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weapon>>> GetWeapons()
        {
            var result = await _uow.Weapons.GetAllAsync(User.UserGuidId());
            return Ok(result);
        }

        // GET: api/Weapons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weapon>> GetWeapon(Guid id)
        {
            var weapon = await _uow.Weapons.FirstOrDefaultAsync(id, User.UserGuidId());

            if (weapon == null)
            {
                return NotFound();
            }

            return Ok(weapon);
        }

        // PUT: api/Weapons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeapon(Guid id, DAL.App.DTO.Weapon weapon)
        {
            if (id != weapon.Id)
                return BadRequest();

            await _uow.Weapons.UpdateAsync(weapon, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Weapons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Weapon>> PostWeapon(DAL.App.DTO.Weapon weapon)
        {
            _uow.Weapons.Add(weapon);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetWeapon", new {id = weapon.Id}, weapon);
        }

        // DELETE: api/Weapons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Weapon>> DeleteWeapon(Guid id)
        {
            var weapon = await _uow.Weapons.FirstOrDefaultAsync(id, User.UserGuidId());
            if (weapon == null)
            {
                return NotFound();
            }

            await _uow.Weapons.RemoveAsync(weapon, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return Ok(weapon);
        }
    }
}
