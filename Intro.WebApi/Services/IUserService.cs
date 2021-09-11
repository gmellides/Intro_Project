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
        void CreateUserAsync(UserDTO userDTO);
        void EditUserAsync(int userId,UserDTO userDTO);
        void DeleteUserAsync(int userId);
        Task<List<User>> GetActiveUsers();
        List<UserDTO> MapUserDTO(List<User> users);
        UserDTO MapUserDTO(User user);
    }
}
