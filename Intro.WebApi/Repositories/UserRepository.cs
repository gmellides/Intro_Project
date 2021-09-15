using Intro.Models.Model;
using Intro.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// TODO cleanup imports

namespace Intro.WebApi.Repositories
{
    // TODO each repository should have it's own interface
    public class UserRepository : IUserRepository
    {
        private IntroProjectContext _context;

        public UserRepository(IntroProjectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all entities from database
        /// </summary>
        /// <returns>
        /// List of models from Database
        /// </returns>
        public async Task<List<User>> GetAll()
        {
            // TODO you should use async calls to database
            // TODO .Include() can help you include type and title for users
            return await _context.Users.Include(x => x.UserTitle).Include(x => x.UserType).ToListAsync();
        }

        /// <summary>
        /// Returns an entity with
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>
        /// A single entity
        /// </returns>
        public async Task<User> GetEntityByID(int id)
        {
            return await _context.Users.Include(x => x.UserType).Include(x => x.UserTitle).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Gets all active users.
        /// </summary>
        /// <returns>
        /// A list of user entities where isActive Field is true
        /// </returns>
        public async Task<List<User>> GetAllActiveUsers()
        {
            return await _context.Users.Include(x => x.UserTitle).Include(x => x.UserType).Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<List<User>> GetActiveUsersFilteredByFullName(string fullname)
        {
            return await _context.Users.Include(x => x.UserTitle).Include(x => x.UserType).Where(x => x.IsActive == true && x.Name.Contains(fullname) || x.Surname.Contains(fullname)).ToListAsync();
        }

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="user">The user.</param>
        public async Task SaveEntity(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="user">The user.</param>
        public async Task UpdateEntity(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}