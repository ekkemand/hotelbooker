using System.Linq;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Identity;
using Person = Domain.App.Person;

namespace HotelBooker.ApiControllers._1._0.Identity
{
    /// <summary>
    /// Api endpoint for registering a new user and user log-in (jwt token generation)
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<Domain.App.Identity.AppUser> _signInManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        /// <param name="signInManager"></param>
        public AccountController(
            IConfiguration configuration,
            UserManager<Domain.App.Identity.AppUser> userManager,
            ILogger<AccountController> logger,
            SignInManager<Domain.App.Identity.AppUser> signInManager
        )
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Endpoint for user log-in (jwt generation)
        /// </summary>
        /// <param name="dto">login data</param>
        /// <returns>Ok, NotFound or BadRequest</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {dto.Email} not found");
                return NotFound(new MessageDTO("User not found"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); // get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Web-api login. User {appUser.Email} logged in.");
                return Ok(new JwtResponseDTO {Token = jwt, Status = $"User {appUser.Email} logged in"});
            }

            _logger.LogInformation($"Web-Api login. User {appUser.Email} attempted to log-in with wrong password");
            return BadRequest(new MessageDTO("Invalid password"));
        }


        /// <summary>
        /// Endpoint for user registration and immediate log-in (jwt generation)
        /// </summary>
        /// <param name="model">user data</param>
        /// <returns>Ok, NotFound or BadRequest</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser != null)
            {
                _logger.LogInformation(
                    $"Web-Api registration. User with e-mail address {model.Email} already exists!");

                return BadRequest(new MessageDTO("An account with this e-mail already exists"));
            }

            appUser = new Domain.App.Identity.AppUser
            {
                Email = model.Email,
                UserName = model.Email,
                DisplayName = model.DisplayName,
                Person = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NationalIdNumber = model.NationalIdNumber,
                    BirthDate = model.BirthDate,
                    PhoneNumber = model.PhoneNumber
                }
            };

            var result = await _signInManager.UserManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation(
                    $"Web-Api registration. User {model.Email} successfully registered with password.");
                var roleResult = _userManager.AddToRoleAsync(appUser, "user").Result;

                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user); // get the User analog
                    var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                        _configuration["JWT:SigningKey"],
                        _configuration["JWT:Issuer"],
                        _configuration.GetValue<int>("JWT:ExpirationInDays")
                    );
                    _logger.LogInformation($"Web-api login. User {user.Email} logged in.");
                    return Ok(new JwtResponseDTO {Token = jwt, Status = $"User {user.Email} created and logged in"});
                }

                _logger.LogInformation(
                    $"Web-Api registration. User {model.Email} not found after creation!.");
                return BadRequest(new MessageDTO("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();

            _logger.LogInformation("Web-Api registration. User registration failed.");
            return BadRequest(new MessageDTO {Messages = errors});
        }

        /// <summary>
        /// Make a user an admin
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Ok</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        public async Task<ActionResult<string>> MakeAdmin([FromBody] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roleResult = await _userManager.AddToRoleAsync(user, "admin");
            return Ok(roleResult);
        }
        
        /// <summary>
        /// Remove user's admin role making it a regular
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Ok</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        public async Task<ActionResult<string>> MakeRegular([FromBody] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roleResult = await _userManager.RemoveFromRoleAsync(user, "admin");
            return Ok(roleResult);
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Ok or NotFound</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        public async Task<ActionResult<string>> DeleteUser([FromBody] string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager.DeleteAsync(user);
            
            

            return Ok(user);
        }
    }
}