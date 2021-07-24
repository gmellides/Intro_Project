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
        public UserRepository()
        {

        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetEntityByID(string id)
        {
            throw new NotImplementedException();
        }
    }
}
