using Intro.Models.DTO;
using Intro.Models.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Services
{
    public interface IUserService
    {
        User CreateUserEntity(UserDTO userDTO);
        User EditUserAction(User userId,UserDTO userDTO);
        User DeleteUser(User user);
        List<UserDTO> MapUserDTO(List<User> users);
    }
}
