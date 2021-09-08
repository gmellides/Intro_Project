using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public interface IRepository<T> where T:class
    {
        // TODO xml documentation missing
        // TODO this can be used as your base repository which the other repository interfaces can extend
        List<T> GetAll();
        T GetEntityByID(int id);
    }
}
