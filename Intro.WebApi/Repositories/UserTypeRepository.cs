using Intro.Models.Model;
using Intro.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private IntroProjectContext _context;

        public UserTypeRepository(IntroProjectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>A list of all user type entities</returns>
        public async Task<List<UserType>> GetAll()
        {
            return await _context.UserTypes.ToListAsync();
        }

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>a single entity filtered by id.</returns>
        public async Task<UserType> GetEntityByID(int id)
        {
            return await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="input">The input.</param>
        public async Task SaveEntity(UserType input)
        {
            _context.UserTypes.Add(input);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="input">The input.</param>
        public async Task UpdateEntity(UserType input)
        {
            _context.UserTypes.Update(input);
            await _context.SaveChangesAsync();
        }
    }
}