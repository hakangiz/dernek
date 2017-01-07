using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dernek.Repository
{
    public interface IRepository<T> where T :class
    {
        T Get(int? id);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllLinq();
        void Add(T entity);
        void Delete(T entity);
    }
}
