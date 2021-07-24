using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intro.WebApi.Repositories
{
    interface IRepository<T> where T:class
    {
        List<T> GetAll();
        T GetEntityByID(string id);

    }
}
