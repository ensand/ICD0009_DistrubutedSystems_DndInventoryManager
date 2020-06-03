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
    /// Magical items API
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MagicalItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly MagicalItemMapper _mapper = new MagicalItemMapper();

        /// <summary>
        /// API controller constructor for initializing data source.
        /// </summary>
        /// <param name="bll"></param>
        public MagicalItemsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // PUT: api/MagicalItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the items details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="magicalItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagicalItem(Guid id, V1DTO.MagicalItemUpdate magicalItem)
        {
            if (id != magicalItem.Id)
                return BadRequest();
            
            if (!await _bll.MagicalItems.ExistsAsync(magicalItem.Id, User.UserGuidId()))
                return NotFound();

            var mappedItem = _mapper.MapMagicalItemUpdateToBll(magicalItem);

            await _bll.MagicalItems.UpdateAsync(mappedItem, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok();
        }

        // POST: api/MagicalItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new item
        /// </summary>
        /// <param name="magicalItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostMagicalItem(V1DTO.NewMagicalItem magicalItem)
        {
            var bllEntity = _mapper.MapNewMagicalItemToBll(magicalItem);
            _bll.MagicalItems.Add(bllEntity);
            await _bll.SaveChangesAsync();
        
            return Ok(bllEntity.Id);
        }

        // DELETE: api/MagicalItems/5
        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMagicalItem(Guid id)
        {
            var item = await _bll.MagicalItems.FirstOrDefaultAsync(id, User.UserGuidId());
            if (item == null)
                return NotFound();

            await _bll.MagicalItems.RemoveAsync(item, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok(item.Id);
        }
    }
}
