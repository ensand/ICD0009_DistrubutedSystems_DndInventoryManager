using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.ApiControllers._1._0.Identity;

namespace WebApp.ApiControllers._1._0.Functionality
{
    /// <summary>
    /// Future addition for sharing inventory between users.
    /// Currently can just grab JSON from API call.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ItemsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userManager"></param>
        /// <param name="configuration"></param>
        public ItemsController(ILogger<AccountController> logger, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Pass on inventory from one user to another
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<ActionResult<string>> PassOnInventory([FromBody] PasserDTO model)
        {
            // check if owner & target are real
            var owner = await _userManager.FindByEmailAsync(model.OwnerEmail);
            var target = await _userManager.FindByEmailAsync(model.TargetEmail);

            if (owner == null || target == null)
            {
                var nullUser = owner == null ? model.OwnerEmail : model.TargetEmail;
                
                _logger.LogInformation("Web-Api inventory passage. Non-existent user: " + nullUser);
                return StatusCode(404);
            }
            
            // check if owner actually owns items?
            // make copies to target
            
            // if 'DeleteFromOwner' == true => delete all items from owner
            if (model.DeleteFromOwner)
            {
                // do the deletes
            }
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// DTO for required inputs
        /// </summary>
        public class PasserDTO
        {
            [MinLength(5)]
            [MaxLength(1024)]
#pragma warning disable 1591
            public string OwnerEmail { get; set; } = default!;
            
            // How to make sure owner is logged in?
            
            [MinLength(5)]
            [MaxLength(1024)]
            public string TargetEmail { get; set; } = default!;

            public bool DeleteFromOwner { get; set; } // if true, will 'cut' items from owner, if false, 'copy'
            
            public ICollection<string>? ArmorIds { get; set; }
            public ICollection<string>? WeaponIds { get; set; }
            public ICollection<string>? MagicalItemIds { get; set; }
            public ICollection<string>? OtherEquipmentIds { get; set; }
#pragma warning restore 1591
        }
    }
}