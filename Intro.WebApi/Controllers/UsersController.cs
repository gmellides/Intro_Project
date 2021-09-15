using Intro.Models.DTO;
using Intro.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                               IUserService userService)
        {
            // TODO argument checks
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Get all Active registered users
        /// </summary>
        /// <returns>A list of all active users</returns>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                List<UserDTO> users = await _userService.GetActiveUsersAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError($"GetAllUsers - Error occured with the exception {e.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets the full name of the users filtered by.
        /// </summary>
        /// <param name="input">The fullname.</param>
        /// <returns>A list of active Users filtered by full name</returns>
        [HttpPost]
        [Route("filterUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetUsersFilteredByFullName([FromBody] SearchUserDTO input)
        {
            try
            {
                var filteredUsers = await _userService.GetActiveUsersFiteredByFullNameAsync(input);
                return Ok(filteredUsers);
            }
            catch (Exception e)
            {
                _logger.LogError($"GetUsersFilteredByFullName - Error occured with the exception {e.Message}");
                return StatusCode(500);
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
                await _userService.CreateUserAsync(userDTO);
            }
            catch (Exception e)
            {
                _logger.LogError($"PostUser - An error occured with message {e.Message} and stackTrace {e.StackTrace}");
                return StatusCode(500);
            }

            return Ok();
        }

        /// <summary>
        /// Puts the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>200 - User is edited, 500 - in case of exception</returns>
        [HttpPut]
        [Route("{userId}")]
        public async Task<ActionResult> PutUser(int userId, UserDTO userDTO)
        {
            try
            {
                // TODO unused variables
                await _userService.EditUserAsync(userId, userDTO);
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
        /// </summary>
        /// <param name="userId">user id From querystring</param>
        /// <returns>200 if user is deleted,500 - in case of error</returns>
        [HttpDelete]
        [Route("{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
            }
            catch (Exception e)
            {
                _logger.LogError($"DeleteUser - An error occured with message {e.Message} and stackTrace {e.StackTrace}");
                return StatusCode(500);
            }
            return Ok();
        }
    }
}