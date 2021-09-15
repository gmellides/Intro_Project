using Intro.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Services.Interfaces
{
    public interface IUserTypeService
    {
        /// <summary>
        /// Gets all user types asynchronous.
        /// </summary>
        /// <returns>a list with all user types dtos</returns>
        Task<List<UsertTypeDTO>> GetAllUserTypesAsync();
    }
}