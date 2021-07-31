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
        private readonly IntroProjectContext _context;

        public UsersController(ILogger<UsersController> logger, 
                               IRepository<User> usersRepository, 
                               IntroProjectContext context)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<UserDTO>> GetAllUsers()
        {


            return Ok(null);
        }

        [HttpPost]
        public void PostUser(UserDTO userDTO)
        {

        }

        [HttpPut]
        public void PutUser([FromQuery] string userId)
        {

        }

        [HttpDelete]
        public ActionResult DeleteUser([FromQuery]int userId)
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
