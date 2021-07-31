using Intro.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public class UserTitleRepository : IRepository<UserTitle>
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

        public UserTitle GetEntityByID(int id)
        {
            return _context.UserTitles.FirstOrDefault(x=>x.Id == id)
        }
    }
}
