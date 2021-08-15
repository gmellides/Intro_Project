﻿using Intro.Models.DTO;
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
        private readonly IntroProjectContext _context;

        public UserServices(IRepository<UserTitle> userTitleRepository,
                            IRepository<UserType> userTypeRepository,
                            IntroProjectContext context)
        {
            _userTitleRepository = userTitleRepository;
            _userTypeRepository = userTypeRepository;
            _context = context;
        }
             
        public async Task DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
            if (user != null)
            {
                user.IsActive = false;

                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation($"There is no user with Id {userId}");
            }
        }

        public async Task EditUserAction(int userId,UserDTO userDTO)
        {
            User user = _context.Users.FirstOrDefault(x=>x.Id == userId);
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

                if (user.UserType.Code != userDTO.UserType.Code)
                    user.UserType.Code = userDTO.UserType.Code;

                if (user.UserType.Description != userDTO.UserType.Description)
                    user.UserType.Description = userDTO.UserType.Description;

                if (user.UserTitle.Description != userDTO.UserTitle.Description)
                    user.UserTitle.Description = userDTO.UserTitle.Description;

            }
            else
            {

            }
        }

        public async Task CreateUserAction(UserDTO userDTO)
        {
            User user = new User
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                BirthDate = userDTO.BirthDate,
                EmailAddress = userDTO.EmailAddress,
                UserType = new UserType
                {
                    Code = userDTO.UserType.Code,
                    Description = userDTO.UserType.Description
                },
                UserTitle = new UserTitle
                {
                    Description = userDTO.UserTitle.Description
                },
                IsActive = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        #region Private Mapping Helpers
        private List<UserDTO> MapUserDTO(List<User> users)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            // Link User Title and type
            users.ForEach(x => x.UserTitle = _userTitleRepository.GetEntityByID(x.UserTitleId));
            users.ForEach(x => x.UserType = _userTypeRepository.GetEntityByID(x.UserTypeId));
            foreach (var user in users)
            {
                userDTOs.Add(new UserDTO
                {
                    Surname = user.Surname,
                    Name = user.Name,
                    BirthDate = user.BirthDate,
                    EmailAddress = user.EmailAddress,
                    Id = user.Id,
                    UserTitle = new UserTitleDTO
                    {
                        Description = user.UserTitle.Description
                    },
                    UserType = new UsertTypeDTO
                    {
                        Code = user.UserType.Code,
                        Description = user.UserType.Description
                    }
                });
            }
            return userDTOs;
        }
        private UserDTO MapUserDTO(User user)
        {
            // Link User Title and type
            user.UserTitle = _userTitleRepository.GetEntityByID(user.UserTitleId);
            user.UserType = _userTypeRepository.GetEntityByID(user.UserTypeId);

            UserDTO userDTO = new UserDTO
            {
                Surname = user.Surname,
                Name = user.Name,
                BirthDate = user.BirthDate,
                EmailAddress = user.EmailAddress,
                Id = user.Id,
                UserTitle = new UserTitleDTO
                {
                    Description = user.UserTitle.Description
                },
                UserType = new UsertTypeDTO
                {
                    Code = user.UserType.Code,
                    Description = user.UserType.Description
                }
            };

            return userDTO;
        }
        #endregion
    }
}