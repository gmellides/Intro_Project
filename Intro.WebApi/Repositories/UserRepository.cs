using Intro.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// TODO cleanup imports

namespace Intro.WebApi.Repositories
{
    // TODO each repository should have it's own interface
    public class UserRepository : IRepository<User>
    {
        IntroProjectContext _context;

        public UserRepository(IntroProjectContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            // TODO you should use async calls to database
            // TODO .Include() can help you include type and title for users
            return _context.Users.ToList();
        }

        public User GetEntityByID(int id)
        {
            return _context.Users.FirstOrDefault(x=>x.Id == id);
        }
    }
}
