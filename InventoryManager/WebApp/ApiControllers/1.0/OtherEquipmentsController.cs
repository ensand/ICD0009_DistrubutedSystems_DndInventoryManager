using System;
using System.Collections.Generic;
using System.Linq;
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
    public class OtherEquipmentsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public OtherEquipmentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/OtherEquipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OtherEquipment>>> GetOtherEquipments()
        {
            var result = await _uow.OtherEquipments.GetAllAsync(User.UserGuidId());
            Console.WriteLine("Api: " + result.Count());
            return Ok(result);
        }

        // GET: api/OtherEquipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OtherEquipment>> GetOtherEquipment(Guid id)
        {
            var otherEquipment = await _uow.OtherEquipments.FirstOrDefaultAsync(id, User.UserGuidId());

            if (otherEquipment == null)
            {
                return NotFound();
            }

            return Ok(otherEquipment);
        }

        // PUT: api/OtherEquipments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOtherEquipment(Guid id, DAL.App.DTO.OtherEquipment otherEquipment)
        {
            if (id != otherEquipment.Id)
                return BadRequest();

            await _uow.OtherEquipments.UpdateAsync(otherEquipment, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OtherEquipments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OtherEquipment>> PostOtherEquipment(DAL.App.DTO.OtherEquipment otherEquipment)
        {
            _uow.OtherEquipments.Add(otherEquipment);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetOtherEquipment", new { id = otherEquipment.Id }, otherEquipment);
        }

        // DELETE: api/OtherEquipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OtherEquipment>> DeleteOtherEquipment(Guid id)
        {
            var otherEquipment = await _uow.OtherEquipments.FirstOrDefaultAsync(id, User.UserGuidId());
            if (otherEquipment == null)
            {
                return NotFound();
            }

            await _uow.OtherEquipments.RemoveAsync(otherEquipment, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return Ok(otherEquipment);
        }
    }
}
