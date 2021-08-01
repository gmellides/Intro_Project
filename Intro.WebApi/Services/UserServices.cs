using Intro.Models.DTO;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Services
{
    public class UserServices
    {
        private readonly IRepository<UserTitle> _userTitleRepository;
        private readonly IRepository<UserType> _userTypeRepository;
        private readonly IntroProjectContext _context;

        public UserServices(IRepository<UserTitle> userTitleRepository,
                IRepository<UserType> userTypeRepository, IntroProjectContext context)
        {
            _userTitleRepository = userTitleRepository;
            _userTypeRepository = userTypeRepository;
            _context = context;
        }

        public List<UserDTO> MapUsersToUsersDTO(List<User> users)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            // Link User Title and type
            users.ForEach(x => x.UserTitle = _userTitleRepository.GetEntityByID(x.UserTitleId));
            users.ForEach(x => x.UserType = _userTypeRepository.GetEntityByID(x.UserTypeId));
            foreach(var user in users)
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
                        Desctiption = user.UserType.Description
                    }
                });
            }
            return userDTOs;
        }
    }
}
