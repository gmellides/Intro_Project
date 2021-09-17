using AutoMapper;
using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories.Interfaces;
using Intro.WebApi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Intro.Tests")] // InternalsVisibleTo Attribute is used to enable visib

namespace Intro.WebApi.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserServices> _logger;

        public UserServices(ILogger<UserServices> logger,
                            IUserRepository usersRepository,
                            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        public async Task CreateUserAsync(CreateEditUserDTO userDTO)
        {
            if (userDTO == null)
                throw new ArgumentNullException(nameof(UserDTO));

            try
            {
                var user = CreateUserAction(userDTO);
                await _usersRepository.SaveEntity(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Edits the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userDTO">The user dto.</param>
        /// <exception cref="System.NullReferenceException">Edit User Async -</exception>
        public async Task EditUserAsync(int userId, CreateEditUserDTO userDTO)
        {
            try
            {
                var user = await _usersRepository.GetEntityByID(userId);
                if (user != null)
                {
                    var editedUser = EditUserAction(user, userDTO);
                    await _usersRepository.UpdateEntity(editedUser);
                }
                else
                {
                    throw new NullReferenceException($"Edit User Async - No user found with {userId} user ID");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets active users asynchronous.
        /// </summary>
        /// <returns>A List of active users DTO</returns>
        public async Task<List<UserDTO>> GetActiveUsersAsync()
        {
            try
            {
                var users = await _usersRepository.GetAllActiveUsers();
                var usersDto = _mapper.Map<List<UserDTO>>(users);
                return usersDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the full name of the active users fitered by.
        /// </summary>
        /// <param name="searchUserDTO"></param>
        /// <returns>
        /// List of Active user DTOs filtered by fullname
        /// </returns>
        public async Task<List<UserDTO>> GetActiveUsersFiteredByFullNameAsync(SearchUserDTO searchUserDTO)
        {
            try
            {
                var filteredUsers = await _usersRepository.GetActiveUsersFilteredByFullName(searchUserDTO.FullName);
                var filteredUsersDTOs = _mapper.Map<List<UserDTO>>(filteredUsers);
                return filteredUsersDTOs;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _usersRepository.GetEntityByID(userId);
                if (user != null)
                {
                    var updatedEntity = DeleteUserAction(user);
                    await _usersRepository.UpdateEntity(updatedEntity);
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

        // ---- Internal Methods (Access Identifier is internal to be able to test them from Unit Tests)

        /// <summary>
        /// Deletes the user action.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>user entity with isActive field setted to false</returns>
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
        /// Edits the user action.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>A user entity with edited fields</returns>
        internal User EditUserAction(User user, CreateEditUserDTO userDTO)
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

                if (user.UserTypeId != userDTO.UserTypeId)
                    user.UserTypeId = userDTO.UserTypeId;

                if (user.UserTitleId != userDTO.UserTitleId)
                    user.UserTitleId = userDTO.UserTitleId;

                return user;
            }

            _logger.LogInformation($"Edit User: There is no user with Id {user.Id}");
            return null;
        }

        /// <summary>
        /// Creates the user action.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>A single entity with values based in userDTO</returns>
        internal User CreateUserAction(CreateEditUserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            return user;
        }
    }
}