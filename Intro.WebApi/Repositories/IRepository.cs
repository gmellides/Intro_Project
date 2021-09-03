using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    public interface IRepository<T> where T:class
    {
        List<T> GetAll();
        T GetEntityByID(int id);
    }
}
