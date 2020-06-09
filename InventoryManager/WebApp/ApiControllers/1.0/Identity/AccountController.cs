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
    /// <summary>
    /// API controller for log-in and registration actions
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        /// <param name="signInManager"></param>
        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager, ILogger<AccountController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Action that will allow users to log in.
        /// Will not generate a new token if user just refresh a page and a new log-in is requested.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                if (model.Refresh)
                {
                    _logger.LogInformation("Web-Api refresh login. User: " + model.Email);
                    return Ok(new {token = model.Token, status = "Login successful", userFirstName = appUser.FirstName});
                }
                
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); // Get the user analog
                var jwt = Extensions.IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims, 
                    _configuration["JWT:signingKey"],
                    _configuration["JWT:issuer"], 
                    _configuration.GetValue<int>("JWT:expirationInDays"));
                
                _logger.LogInformation("Token generated for user: " + model.Email);
                return Ok(new {token = jwt, status = "Login successful", userFirstName = appUser.FirstName});
            }
            
            _logger.LogInformation("Web-Api login. User attempted login with bad password: " + model.Email);
            return StatusCode(403);
        }

        /// <summary>
        /// Prototype for signing in by checking if given token is valid an belongs to given user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public Task<ActionResult<string>> LoginWithToken([FromBody] TokenLoginDTO model)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
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

        /// <summary>
        /// DTO for required inputs for a successful log-in
        /// </summary>
        public class LoginDTO
        {
#pragma warning disable 1591
            [MinLength(5)] 
            [MaxLength(1024)] 
            public string Email { get; set; } = default!;
            
            [MaxLength(1024)]
            public string Password { get; set; } = default!;

            public bool Refresh { get; set; }

            public string? Token { get; set; }
        }

        /// <summary>
        /// DTO for required inputs for a successful log-in with a token
        /// </summary>
        public class TokenLoginDTO
        {
            [MinLength(5)]
            [MaxLength(1024)]
            public string Email { get; set; } = default!;

            [Required] 
            public string Token { get; set; } = default!;
        }

        /// <summary>
        /// DTO for required inputs for a successful registration
        /// </summary>
        public class RegisterDTO
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
#pragma warning restore 1591
    }
}