using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.ApiControllers._1._0.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
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
                return StatusCode(404);
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
        public async Task<ActionResult<string>> LoginWithToken([FromBody] string token)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser != null)
            {
                _logger.LogInformation("Web-Api register. User already exists: " + model.Email);
                return StatusCode(403);
            }
            
            var newUser = new AppUser {Email = model.Email, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName};
            var result = _userManager.CreateAsync(newUser, model.Password).Result;
            
            if (!result.Succeeded)
            {
                throw new ApplicationException("User creation failed: " + model.Email);
            }
            
            var roleResult = _userManager.AddToRoleAsync(newUser, "user").Result;
            
            _logger.LogInformation("New user registered: " + model.Email);
            return Ok(new {status = "Registration successful"});
        }

        public class LoginDTO // add required, min/max length
        {
            [MinLength(5)] 
            [MaxLength(1024)] 
            public string Email { get; set; } = default!;
            
            [MaxLength(1024)]
            public string Password { get; set; } = default!;
        }

        public class TokenLoginDTO
        {
            [MinLength(5)] 
            [MaxLength(1024)] 
            public string Email { get; set; } = default!;
            
            [MinLength(5)] 
            [MaxLength(4096)] 
            public string Token { get; set; } = default!;
        }

        public class RegisterDTO // add required, min/max length
        {
            [MinLength(5)]
            [MaxLength(1024)]
            public string Email { get; set; } = default!;
            
            [MaxLength(1024)]
            public string Password { get; set; } = default!;
            
            [MinLength(1)] 
            [MaxLength(64)]
            public string FirstName { get; set; } = default!;
            
            [MinLength(1)] 
            [MaxLength(64)] 
            public string LastName { get; set; } = default!;
        }
    }
}