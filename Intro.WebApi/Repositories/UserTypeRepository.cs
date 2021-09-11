using Intro.Models.Model;
using Intro.WebApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Intro.WebApi.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        IntroProjectContext _context;

        public UserTypeRepository(IntroProjectContext context)
        {
            _context = context;
        }

        public async Task<List<UserType>> GetAll()
        {
            return await _context.UserTypes.ToListAsync();
        }

        public async Task<UserType> GetEntityByID(int id)
        {
            return await _context.UserTypes.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async void SaveEntity(UserType input)
        {
            _context.UserTypes.Add(input);
            await _context.SaveChangesAsync();
        }

        public async void UpdateEntity(UserType input)
        {
            _context.UserTypes.Update(input);
            await _context.SaveChangesAsync();
        }

    }
}
