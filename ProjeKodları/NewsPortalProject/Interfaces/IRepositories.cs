using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortalProject.Interfaces
{
    internal interface IRepositories<T>
    {

        bool Add(T entity);

        bool Update(T entity);

        bool Delete(int id);

        T Get(int id);

        List<T> GetAll();


   

    }
}
