using Intro.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Services.Interfaces
{
    public interface IUserTitleService
    {
        /// <summary>
        /// Gets all user titles asynchronous.
        /// </summary>
        /// <returns>A list with all user titles</returns>
        Task<List<UserTitleDTO>> GetAllUserTitlesAsync();
    }
}