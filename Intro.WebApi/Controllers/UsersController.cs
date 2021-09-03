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
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<UserTitle> _userTitleRepository;
        private readonly IRepository<UserType> _userTypeRepository;
        private readonly IUserService _userService;
        private readonly IntroProjectContext _context;

        public UsersController(ILogger<UsersController> logger,
                               IRepository<User> usersRepository,
                               IRepository<UserTitle> userTitleRepository,
                               IRepository<UserType> userTypeRepository,
                               IUserService userService,
                               IntroProjectContext context)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _context = context;
            _userService = userService;
            _userTitleRepository = userTitleRepository;
            _userTypeRepository = userTypeRepository;
        }

        /// <summary>
        /// Get all Active registered users
        /// METHOD: GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var x = _usersRepository.GetAll().Where(x => x.IsActive == true).ToList();
            List<UserDTO> users = _userService.MapUserDTO(x);
            return Ok(users);
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
        /// <param name="userId"></param>
        [HttpPut]
        public async Task<ActionResult> PutUser(UserDTO userDTO)
        {
            try
            {
                User user = _context.Users.Include(x=>x.UserTitle).Include(x=>x.UserType).FirstOrDefault(x => x.Id == userDTO.Id);

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
