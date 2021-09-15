using Intro.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Gets all active users.
        /// </summary>
        /// <returns>A list of user entities where isActive Field is true</returns>
        Task<List<User>> GetAllActiveUsers();

        /// <summary>
        /// Gets the full name of the active users filtered by.
        /// </summary>
        /// <param name="fullname">The fullname.</param>
        /// <returns>a list of active user entities where name or surname includes parameter</returns>
        Task<List<User>> GetActiveUsersFilteredByFullName(string fullname);
    }
}