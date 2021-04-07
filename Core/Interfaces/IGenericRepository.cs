using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
     public interface IGenericRepository<T> where T :class
    {
        IEnumerable<T> GetAll();
        T GetById(Object id);
        void Insert(T entity);
        void Upate(T entity);
        void Delete(object id);

    }
}
