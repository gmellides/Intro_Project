using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using Intro.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Intro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        // TODO controllers should only know of services and services should know of repositories. Repositories should know of context
        // TODO unused repositories
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger,
                               IRepository<User> usersRepository,
                               IRepository<UserTitle> userTitleRepository,
                               IRepository<UserType> userTypeRepository,
                               IUserService userService,
                               IntroProjectContext context)
        {
            // TODO argument checks
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService;
        }

        /// <summary>
        /// Get all Active registered users
        /// METHOD: GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                //_userService.
            }catch(Exception e)
            {

            }
        }

        /// <summary>
        /// Users controller Post action
        /// </summary>
        /// <param name="userDTO">User information that needs to be added.</param>
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDTO userDTO)
        {
            try
            {
                // TODO this should not be part of controller, should be put in repository
                User user = _userService.CreateUserEntity(userDTO);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"PostUser - An error occured with message {e.Message} and stackTrace {e.StackTrace}");
                return StatusCode(500);
            }

            return Ok();
        }

        /// <summary>
        /// Edit User
        /// Method:PUT
        /// </summary>
        /// <param name="userId">TODO missing descriptions wrong param</param>
        [HttpPut]
        // TODO [Route("{userId}")]
        public async Task<ActionResult> PutUser(UserDTO userDTO)
        {
            try
            {
                User user = _context.Users.Include(x=>x.UserTitle).Include(x=>x.UserType).FirstOrDefault(x => x.Id == userDTO.Id);

                // TODO unused variables
                user = _userService.EditUserAction(user, userDTO);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"PutUser - An error occured with message {e.Message} and stackTrace {e.StackTrace}");
                return StatusCode(500);
            }
            return Ok();
        }

        /// <summary>
        /// Sets isActive to false.
        /// Method:DELETE
        /// </summary>
        /// <param name="userId">user id From querystring</param>
        /// <returns></returns>
        [HttpDelete]
        // TODO [Route("{userId}")]
        public async Task<ActionResult> DeleteUser([FromQuery] int userId)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user = _userService.DeleteUser(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogError($"DeleteUser - user with id {userId} not found.");
                return StatusCode(400);
            }
            return Ok();
        }

    }
}
