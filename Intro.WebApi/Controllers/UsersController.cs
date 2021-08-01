using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IntroProjectContext _context;

        public UsersController(ILogger<UsersController> logger,
                               IRepository<User> usersRepository,
                               IRepository<UserTitle> userTitleRepository,
                               IRepository<UserType> userTypeRepository,
                               IntroProjectContext context)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _userTitleRepository = userTitleRepository;
            _userTypeRepository = userTypeRepository;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            List<User> users = _usersRepository.GetAll();
            users.ForEach(x => x.UserTitle = _userTitleRepository.GetEntityByID(x.UserTitleId));
            users.ForEach(x => x.UserType = _userTypeRepository.GetEntityByID(x.UserTypeId));
            return Ok(users);
        }

        [HttpPost]
        public void PostUser(UserDTO userDTO)
        {
            User user = new User
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                BirthDate = userDTO.BirthDate,
                EmailAddress = userDTO.EmailAddress,
                UserType = new UserType { 
                    Code = userDTO.UserType.Code, 
                    Description = userDTO.UserType.Desctiption 
                },
                UserTitle = new UserTitle
                {
                    Description = userDTO.UserTitle.Description
                },
                IsActive = true
            };
            _context.Users.Add(user);
            _context.SaveChanges();

        }

        [HttpPut]
        public void PutUser([FromQuery] string userId)
        {

        }

        [HttpDelete]
        public ActionResult DeleteUser([FromQuery] int userId)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.IsActive = false;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
