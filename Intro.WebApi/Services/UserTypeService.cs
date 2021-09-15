using AutoMapper;
using Intro.Models.DTO;
using Intro.WebApi.Repositories.Interfaces;
using Intro.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IMapper _mapper;

        public UserTypeService(IUserTypeRepository userTypeRepository,
                                IMapper mapper)
        {
            _userTypeRepository = userTypeRepository ?? throw new ArgumentNullException(nameof(userTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets all user types asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UsertTypeDTO>> GetAllUserTypesAsync()
        {
            try
            {
                var userTypes = await _userTypeRepository.GetAll();
                var userTypeDtos = _mapper.Map<List<UsertTypeDTO>>(userTypes);
                return userTypeDtos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}