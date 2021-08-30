﻿using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using Intro.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            List<UserDTO> users = _userService.MapUserDTO(_usersRepository.GetAll());

            return Ok(users);
        }

        /// <summary>
        /// Users controller Post action 
        /// End Point: POST https://localhost:44362/api/Users
        /// This method will check the user input and it will save a user in database.
        /// </summary>
        /// <param name="userDTO">User information that needs to be added.</param>
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDTO userDTO)
        {
            if (ValidateUserDTO(userDTO))
            {
                _logger.LogError($"PostUser - Invalid Input error");
                return StatusCode(400);
            }

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
        /// 
        /// </summary>
        /// <param name="userId"></param>
        [HttpPut]
        public async Task<ActionResult> PutUser([FromQuery] int userId,UserDTO userDTO)
        {
            if (ValidateUserDTO(userDTO))
            {
                _logger.LogError($"PutUser - Invalid Input error");
                return StatusCode(400);
            }

            try
            {
                User user = _context.Users.FirstOrDefault(x => x.Id == userId);
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
        /// 
        /// </summary>
        /// <param name="userId"></param>
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

        #region Private Helpers        
        /// <summary>
        /// Validates the user dto.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>TRUE if input is Valid and FALSE if input is Invalid</returns>
        private bool ValidateUserDTO(UserDTO userDTO)
        {
            #region Check for null properties
            foreach(PropertyInfo info in userDTO.GetType().GetProperties())
            {
                if(info.Name != "BirthDate")
                {

                }
            }
            #endregion
            
            if (userDTO.Name.Any(char.IsDigit) || string.IsNullOrWhiteSpace(userDTO.Name) ||
                userDTO.Surname.Any(char.IsDigit) || string.IsNullOrWhiteSpace(userDTO.Surname) ||
                string.IsNullOrWhiteSpace(userDTO.EmailAddress) )
                return false;
           

            return true;
        }

        #endregion
    }
}
