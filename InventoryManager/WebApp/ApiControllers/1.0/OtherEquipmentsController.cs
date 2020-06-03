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
    /// Other Equipment API
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OtherEquipmentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OtherEquipmentMapper _mapper = new OtherEquipmentMapper();

        /// <summary>
        /// API controller constructor for initializing data source.
        /// </summary>
        /// <param name="bll"></param>
        public OtherEquipmentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // PUT: api/OtherEquipments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the equipments details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="otherEquipment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOtherEquipment(Guid id, V1DTO.OtherEquipmentUpdate otherEquipment)
        {
            if (id != otherEquipment.Id)
                return BadRequest();
            
            if (!await _bll.OtherEquipments.ExistsAsync(otherEquipment.Id, User.UserGuidId()))
                return NotFound();

            var mappedEq = _mapper.MapOtherEquipmentUpdateToBll(otherEquipment);

            await _bll.OtherEquipments.UpdateAsync(mappedEq, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok();
        }

        // POST: api/OtherEquipments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new equipment
        /// </summary>
        /// <param name="otherEquipment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostOtherEquipment(V1DTO.NewOtherEquipment otherEquipment)
        {
            var bllEntity = _mapper.MapNewOtherEquipmentToBll(otherEquipment);
            bllEntity.AppUserId = User.UserGuidId();
            _bll.OtherEquipments.Add(bllEntity);
            await _bll.SaveChangesAsync();
        
            return Ok(bllEntity.Id);
        }

        // DELETE: api/OtherEquipments/5
        /// <summary>
        /// Delete equipment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOtherEquipment(Guid id)
        {
            var eq = await _bll.OtherEquipments.FirstOrDefaultAsync(id, User.UserGuidId());
            if (eq == null)
                return NotFound();

            await _bll.OtherEquipments.RemoveAsync(eq, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok(eq.Id);
        }
    }
}
