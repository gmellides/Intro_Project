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
            throw new NotImplementedException();
        }

        public UserType GetEntityByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
