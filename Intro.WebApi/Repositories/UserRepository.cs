using Intro.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public class UserRepository : IRepository<User>
    {
        IntroProjectContext _context;

        public UserRepository(IntroProjectContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetEntityByID(int id)
        {
            return _context.Users.FirstOrDefault(x=>x.Id == id);
        }
    }
}
