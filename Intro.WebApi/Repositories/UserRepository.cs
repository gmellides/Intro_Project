using Intro.Models.Model;
using Intro.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// TODO cleanup imports

namespace Intro.WebApi.Repositories
{
    // TODO each repository should have it's own interface
    public class UserRepository : IUserRepository
    {
        IntroProjectContext _context;

        public UserRepository(IntroProjectContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            // TODO you should use async calls to database
            // TODO .Include() can help you include type and title for users
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetEntityByID(int id)
        {
            return await _context.Users.Include(x => x.UserType).Include(x => x.UserTitle).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async void SaveEntity(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async void UpdateEntity(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllActiveUsers()
        {
            return await _context.Users.Include(x => x.UserTitle).Include(x => x.UserType).Where(x => x.IsActive == true).ToListAsync();
        }
    }
}
