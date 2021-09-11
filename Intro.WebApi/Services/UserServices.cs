using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using Intro.WebApi.Repositories.Interfaces;
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
        private readonly IUserRepository _usersRepository;
        private readonly IUserTitleRepository _userTitleRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        // TODO you have not included this in constructor so it won't be injected
        private readonly ILogger<UserServices> _logger;

        public UserServices(ILogger<UserServices> logger,
                            IUserRepository usersRepository,
                            IUserTitleRepository userTitleRepository,
                            IUserTypeRepository userTypeRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _userTitleRepository = userTitleRepository ?? throw new ArgumentNullException(nameof(userTitleRepository));
            _userTypeRepository = userTypeRepository ?? throw new ArgumentNullException(nameof(userTypeRepository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        public async void DeleteUserAsync(int userId)
        {
            try
            {
                var user =await _usersRepository.GetEntityByID(userId);
                if (user != null)
                {
                    var updatedEntity = DeleteUserAction(user);
                    // update item
                    _usersRepository.UpdateEntity(updatedEntity);
                }
                else
                {
                    _logger.LogInformation("DeleteUserAsync - user is null");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("");
                throw e;
            }
        }

        public async Task<List<User>> GetActiveUsers()
        {
            try
            {
                var users = await _usersRepository.GetAllActiveUsers();
                return users;
            }catch(Exception e)
            {
                _logger.LogError("");
                throw e;
            }
        }

        public async void EditUserAsync(int userId, UserDTO userDTO)
        {
            try
            {
                var user = await _usersRepository.GetEntityByID(userId);
                if (user != null)
                {
                    var editedUser = EditUserAction(user, userDTO);
                    // update item
                }
                else
                {
                    throw new NullReferenceException(@"Edit User Async - ");
                }
            }catch(Exception e)
            {
                throw e;
            }
        }

        public async void CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                var user = CreateUserAction(userDTO);
                // save
                _usersRepository.SaveEntity(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal User DeleteUserAction(User user)
        {
            if (user != null)
            {
                // TODO this should be part of repository method
                user.IsActive = false;
                return user;
            }
            // TODO redundant else since you return above
            else
            {
                _logger.LogInformation($"User Not Found: There is no user with Id {user.Id}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        internal User EditUserAction(User user, UserDTO userDTO)
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

            _logger.LogInformation($"Edit User: There is no user with Id {user.Id}");
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        internal User CreateUserAction(UserDTO userDTO)
        {
            // TODO argument check missing
            // TODO use a mapper for this
            // TODO service methods should call repository methods to manipulate database
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


        public List<UserDTO> MapUserDTO(List<User> users)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            // Link User Title and type
            // TODO this is not efficient. you are making a query for each user, many of which you already queried for
            users.ForEach(x => x.UserTitle = _userTitleRepository.GetEntityByID(x.UserTitleId));
            users.ForEach(x => x.UserType = _userTypeRepository.GetEntityByID(x.UserTypeId));
            // TODO use an Automapper for this
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
