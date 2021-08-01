using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IntroProjectContext _context;

        public UsersController(ILogger<UsersController> logger,
                               IRepository<User> usersRepository,
                               IRepository<UserTitle> userTitleRepository,
                               IRepository<UserType> userTypeRepository,
                               IntroProjectContext context)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            List<User> users = _usersRepository.GetAll();
           

            return Ok(null);
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
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        [HttpPut]
        public void PutUser([FromQuery] string userId,UserDTO user)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
