using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public interface ICarGoRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> GetAll();
        T GetByID(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}