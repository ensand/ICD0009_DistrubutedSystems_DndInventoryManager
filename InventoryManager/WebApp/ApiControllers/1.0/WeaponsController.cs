using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.V1.Mappers;
using V1DTO = PublicApi.DTO.V1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Weapons API
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeaponsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WeaponMapper _mapper = new WeaponMapper();

        /// <summary>
        /// API controller constructor for initializing data source.
        /// </summary>
        /// <param name="bll"></param>
        public WeaponsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // PUT: api/Weapons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the weapons details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeapon(Guid id, V1DTO.WeaponUpdate weapon)
        {
            if (id != weapon.Id)
                return BadRequest();
            
            if (!await _bll.Weapons.ExistsAsync(weapon.Id, User.UserGuidId()))
                return NotFound();

            var mappedItem = _mapper.MapWeaponUpdateToBll(weapon);

            await _bll.Weapons.UpdateAsync(mappedItem, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Weapons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new weapon
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        
        [HttpPost]
        public async Task<ActionResult> PostWeapon(V1DTO.NewWeapon weapon)
        {
            var bllEntity = _mapper.MapNewWeaponToBll(weapon);
            _bll.Weapons.Add(bllEntity);
            await _bll.SaveChangesAsync();
        
            return Ok(bllEntity.Id);
        }

        // DELETE: api/Weapons/5
        /// <summary>
        /// Delete weapon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWeapon(Guid id)
        {
            var weapon = await _bll.Weapons.FirstOrDefaultAsync(id, User.UserGuidId());
            if (weapon == null)
                return NotFound();

            await _bll.Weapons.RemoveAsync(weapon, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok(weapon.Id);
        }
    }
}
