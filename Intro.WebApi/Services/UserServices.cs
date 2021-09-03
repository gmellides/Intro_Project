using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Services
{
    public class UserServices : IUserService
    {
        private readonly IRepository<UserTitle> _userTitleRepository;
        private readonly IRepository<UserType> _userTypeRepository;
        private readonly ILogger<UserServices> _logger;

        public UserServices(IRepository<UserTitle> userTitleRepository,
                            IRepository<UserType> userTypeRepository)
        {
            _userTitleRepository = userTitleRepository;
            _userTypeRepository = userTypeRepository;
        }

        /// <summary>
        /// Deletes user. this method will set isActive to false
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public User DeleteUser(User user)
        {
            if (user != null)
            {
                user.IsActive = false;
                return user;
            }
            else
            {
                _logger.LogInformation($"User Not Found: There is no user with Id {user.Id}");
                return null;
            }
        }

        /// <summary>
        /// Edits the user action. 
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userDTO">The user dto.</param>
        /// <returns></returns>
        public User EditUserAction(User user,UserDTO userDTO)
        {

            if (user != null)
            {
                if (user.Name != userDTO.Name)
                    user.Name = userDTO.Name;

                if (user.Surname != userDTO.Surname)
                    user.Surname = userDTO.Surname;

                if (user.BirthDate.ToString() != userDTO.BirthDate.ToString())
                    user.BirthDate = userDTO.BirthDate;

                if (user.EmailAddress != userDTO.EmailAddress)
                    user.EmailAddress = userDTO.EmailAddress;

                if (user.UserType.Code != userDTO.UserTypeCode)
                    user.UserType.Code = userDTO.UserTypeCode;

                if (user.UserType.Description != userDTO.UserTypeDescription)
                    user.UserType.Description = userDTO.UserTypeDescription;

                if (user.UserTitle.Description != userDTO.UserTitleDescription)
                    user.UserTitle.Description = userDTO.UserTitleDescription;

                return user;
            }
            else
            {
                _logger.LogInformation($"Edit User: There is no user with Id {user.Id}");
                return null;
            }
        }

        /// <summary>
        /// Creates the user entity.
        /// </summary>
        /// <param name="userDTO">A user dto.</param>
        /// <returns>A User Entity based on User DTO.</returns>
        public User CreateUserEntity(UserDTO userDTO)
        {
            User user = new User
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                BirthDate = userDTO.BirthDate,
                EmailAddress = userDTO.EmailAddress,
                UserType = new UserType
                {
                    Code = userDTO.UserTypeCode,
                    Description = userDTO.UserTypeDescription
                },
                UserTitle = new UserTitle
                {
                    Description = userDTO.UserTitleDescription
                },
                IsActive = true
            };
            return user;
        }

        /// <summary>
        /// Maps the user dto.
        /// </summary>
        /// <param name="users">List of user entities</param>
        /// <returns>List of user DTOs</returns>
        public List<UserDTO> MapUserDTO(List<User> users)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            // Link User Title and type
            users.ForEach(x => x.UserTitle = _userTitleRepository.GetEntityByID(x.UserTitleId));
            users.ForEach(x => x.UserType = _userTypeRepository.GetEntityByID(x.UserTypeId));
            foreach (var user in users)
            {
                userDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    Surname = user.Surname,
                    Name = user.Name,
                    BirthDate = user.BirthDate,
                    EmailAddress = user.EmailAddress,
                    UserTitleDescription = user.UserTitle.Description,
                    UserTypeCode = user.UserType.Code,
                    UserTypeDescription = user.UserType.Description
                });
            }
            return userDTOs;
        }
        /// <summary>
        /// Maps the user dto.
        /// </summary>
        /// <param name="user">User Entity.</param>
        /// <returns>UserDTO</returns>
        public UserDTO MapUserDTO(User user)
        {
            // Link User Title and type
            user.UserTitle = _userTitleRepository.GetEntityByID(user.UserTitleId);
            user.UserType = _userTypeRepository.GetEntityByID(user.UserTypeId);
            UserDTO userDTO = new UserDTO 
            {
                Id = user.Id,
                Surname = user.Surname,
                Name = user.Name,
                BirthDate = user.BirthDate,
                EmailAddress = user.EmailAddress,
                UserTitleDescription = user.UserTitle.Description,
                UserTypeCode = user.UserType.Code,
                UserTypeDescription = user.UserType.Description
            };

            return userDTO;
        }
    }
}
