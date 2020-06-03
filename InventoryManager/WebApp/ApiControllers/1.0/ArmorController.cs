using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.V1.Mappers;
using V1DTO = PublicApi.DTO.V1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Armor API
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArmorController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ArmorMapper _mapper = new ArmorMapper();

        /// <summary>
        /// API controller constructor for initializing data source.
        /// </summary>
        /// <param name="bll"></param>
        public ArmorController(IAppBLL bll)
        {
            _bll = bll;
        }

        // PUT: api/Armor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the armor details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="armor"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArmor(Guid id, V1DTO.ArmorUpdate armor)
        {
            if (id != armor.Id)
                return BadRequest();
            
            if (!await _bll.Armors.ExistsAsync(armor.Id, User.UserGuidId()))
                return NotFound();

            var mappedArmor = _mapper.MapArmorUpdateToBll(armor);
            mappedArmor.AppUserId = User.UserGuidId();

            await _bll.Armors.UpdateAsync(mappedArmor, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Armor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new armor
        /// </summary>
        /// <param name="armor"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostArmor(V1DTO.NewArmor armor)
        {
            var bllEntity = _mapper.MapNewArmorToBll(armor);
            bllEntity.AppUserId = User.UserGuidId();
            _bll.Armors.Add(bllEntity);
            await _bll.SaveChangesAsync();
        
            return Ok(bllEntity.Id);
        }

        // DELETE: api/Armor/5
        /// <summary>
        /// Delete an armor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArmor(Guid id)
        {
            var armor = await _bll.Armors.FirstOrDefaultAsync(id, User.UserGuidId());
            if (armor == null)
                return NotFound();

            await _bll.Armors.RemoveAsync(armor, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok(armor.Id);
        }
    }
}
