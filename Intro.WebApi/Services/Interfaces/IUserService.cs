using Intro.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <returns></returns>
        Task CreateUserAsync(CreateEditUserDTO userDTO);

        /// <summary>
        /// Edits the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userDTO">The user dto.</param>
        /// <returns></returns>
        Task EditUserAsync(int userId, CreateEditUserDTO userDTO);

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task DeleteUserAsync(int userId);

        /// <summary>
        /// Gets the active users asynchronous.
        /// </summary>
        /// <returns>all active users</returns>
        Task<List<UserDTO>> GetActiveUsersAsync();

        /// <summary>
        /// Gets the full name of the active users fitered by.
        /// </summary>
        /// <returns>List of Active user DTOs filtered by fullname</returns>
        Task<List<UserDTO>> GetActiveUsersFiteredByFullNameAsync(SearchUserDTO input);
    }
}