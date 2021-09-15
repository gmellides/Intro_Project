using Intro.Models.Model;
using Intro.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public class UserTitleRepository : IUserTitleRepository
    {
        private IntroProjectContext _context;

        public UserTitleRepository(IntroProjectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of User Titles</returns>
        public async Task<List<UserTitle>> GetAll()
        {
            return await _context.UserTitles.ToListAsync();
        }

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>User title entity</returns>
        public async Task<UserTitle> GetEntityByID(int id)
        {
            return await _context.UserTitles.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="input">The input.</param>
        public async Task SaveEntity(UserTitle input)
        {
            _context.UserTitles.Add(input);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="input">The input.</param>
        public async Task UpdateEntity(UserTitle input)
        {
            _context.UserTitles.Update(input);
            await _context.SaveChangesAsync();
        }
    }
}