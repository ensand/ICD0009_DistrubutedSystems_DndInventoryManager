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
    public class MagicalItemsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public MagicalItemsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/MagicalItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MagicalItem>>> GetMagicalItems()
        {
            var result = await _uow.MagicalItems.GetAllAsync(User.UserGuidId());
            return Ok(result);
        }

        // GET: api/MagicalItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MagicalItem>> GetMagicalItem(Guid id)
        {
            var magicalItem = await _uow.MagicalItems.FirstOrDefaultAsync(id, User.UserGuidId());

            if (magicalItem == null)
            {
                return NotFound();
            }

            return Ok(magicalItem);
        }

        // PUT: api/MagicalItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMagicalItem(Guid id, DAL.App.DTO.MagicalItem magicalItem)
        {
            if (id != magicalItem.Id)
                return BadRequest();

            await _uow.MagicalItems.UpdateAsync(magicalItem, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/MagicalItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MagicalItem>> PostMagicalItem(DAL.App.DTO.MagicalItem magicalItem)
        {
            _uow.MagicalItems.Add(magicalItem);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetMagicalItem", new { id = magicalItem.Id }, magicalItem);
        }

        // DELETE: api/MagicalItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MagicalItem>> DeleteMagicalItem(Guid id)
        {
            var magicalItem = await _uow.MagicalItems.FirstOrDefaultAsync(id, User.UserGuidId());
            if (magicalItem == null)
            {
                return NotFound();
            }

            await _uow.MagicalItems.RemoveAsync(magicalItem, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return Ok(magicalItem);
        }
    }
}
