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
        Task CreateUserAction(UserDTO userDTO);
        Task EditUserAction(int userId,UserDTO userDTO);
        Task DeleteUser(int userId);
        List<UserDTO> MapUserDTO(List<User> users);
    }
}
