using Intro.Models.Model;
using Intro.WebApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public class UserTitleRepository : IUserTitleRepository
    {
        IntroProjectContext _context;
        public UserTitleRepository(IntroProjectContext context)
        {
            _context = context;
        }

        public List<UserTitle> GetAll()
        {
            return _context.UserTitles.ToList();
        }

        public async void SaveEntity(UserTitle input)
        {
            _context.UserTitles.Add(input);
            await _context.SaveChangesAsync();
        }

        public async void UpdateEntity(UserTitle input)
        {
            _context.UserTitles.Update(input);
            await _context.SaveChangesAsync();
        }

        public UserTitle GetEntityByID(int id)
        {
            return _context.UserTitles.FirstOrDefault(x => x.Id == id);
        }
    }
}
