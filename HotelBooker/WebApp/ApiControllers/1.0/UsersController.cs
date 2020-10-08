using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;
using V1DTOIdentity = PublicApi.DTO.v1.Identity;

namespace HotelBooker.ApiControllers._1._0
{
    /// <summary>
    /// Users
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class UsersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserMapper _mapper = new UserMapper();
        private readonly PersonMapper _personMapper = new PersonMapper();
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public UsersController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTOIdentity.AppUser>))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<V1DTOIdentity.AppUser>>> GetUsers()
        {
            return Ok((await _bll.Users.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get user's details
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTOIdentity.AppUser))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
        public async Task<ActionResult<V1DTOIdentity.AppUser>> GetUser(Guid id)
        {
            var user = await _bll.Users.FirstOrDefaultAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var detailedUser = await _userManager.FindByIdAsync(id.ToString());
            var roles = await _userManager.GetRolesAsync(detailedUser);
            var dtoUser = _mapper.Map(user);
            dtoUser.Roles = roles;
            return Ok(dtoUser);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="user">User object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
        public async Task<IActionResult> PutUser(Guid id, V1DTOIdentity.AppUser user)
        {
            if (id != user.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and User.id do not match!"));
            }

            var identityUser = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == id);
            identityUser.Email = user.Email;
            identityUser.DisplayName = user.DisplayName;

            await _userManager.UpdateAsync(identityUser);

            await _bll.Persons.UpdateAsync(_personMapper.Map(user.Person!));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>Created user object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTOIdentity.AppUser))]
        public async Task<ActionResult<V1DTOIdentity.AppUser>> PostUser(V1DTOIdentity.AppUser user)
        {
            var bllEntity = _mapper.Map(user);
            _bll.Users.Add(bllEntity);
            await _bll.SaveChangesAsync();
            user.Id = bllEntity.Id;

            return CreatedAtAction("GetUser", new {id = user.Id}, user);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Deleted user object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user,admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTOIdentity.AppUser))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTOIdentity.AppUser>> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var person = (await _bll.Users.FirstOrDefaultAsync(id)).Person!;
            if (user == null)
            {
                return NotFound(new V1DTO.MessageDTO("User not found"));
            }
            
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{user.Id}'.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager.DeleteAsync(user);

            // await _bll.Persons.RemoveAsync(person);
            await _bll.SaveChangesAsync();

            return Ok(user);
        }
    }
}
