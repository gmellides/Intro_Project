using AutoMapper;
using Intro.Models.DTO;
using Intro.WebApi.Repositories.Interfaces;
using Intro.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Services
{
    public class UserTitleService : IUserTitleService
    {
        private readonly IUserTitleRepository _userTitleRepository;
        private readonly IMapper _mapper;

        public UserTitleService(IUserTitleRepository userTitleRepository, IMapper mapper)
        {
            _userTitleRepository = userTitleRepository ?? throw new ArgumentNullException(nameof(userTitleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets all user titles asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTitleDTO>> GetAllUserTitlesAsync()
        {
            try
            {
                var userTitles = await _userTitleRepository.GetAll();
                var userTitleDtos = _mapper.Map<List<UserTitleDTO>>(userTitles);
                return userTitleDtos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}