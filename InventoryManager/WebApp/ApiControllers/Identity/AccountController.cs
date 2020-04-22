using System;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.ApiControllers.Identity
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        
        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager, ILogger<AccountController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model) // will return json web token (JWT)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation("Web-Api login. User not found: " + model.Email);
                return StatusCode(403);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);

            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); // Get the user analog
                var jwt = Extensions.IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims, 
                    _configuration["JWT:signingKey"],
                    _configuration["JWT:issuer"], 
                    _configuration.GetValue<int>("JWT:expirationInDays"));
                
                _logger.LogInformation("Token generated for user: " + model.Email);
                return Ok(new {token = jwt, status = "Login successful"});
            }
            
            _logger.LogInformation("Web-Api login. User attempted login with bad password: " + model.Email);
            return StatusCode(403);
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            throw new NotImplementedException();
        }

        public class LoginDTO // add required, min/max length
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterDTO // add required, min/max length
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}