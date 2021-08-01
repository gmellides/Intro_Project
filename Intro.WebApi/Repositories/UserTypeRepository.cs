using Intro.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public class UserTypeRepository : IRepository<UserType>
    {
        IntroProjectContext _context;

        public UserTypeRepository(IntroProjectContext context)
        {
            _context = context;
        }

        public List<UserType> GetAll()
        {
            return _context.UserTypes.ToList();
        }

        public UserType GetEntityByID(int id)
        {
            return _context.UserTypes.FirstOrDefault(x=>x.Id == id);
        }
    }
}
