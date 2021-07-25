using Intro.Models.DTO;
using Intro.Models.Model;
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

        public UsersController()
        {

        }

        [HttpGet]
        public List<User> GetAllUsers()
        {
            return null;
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
        public void DeleteUser()
        {

        }
    }
}
