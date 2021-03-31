using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Common
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(string id);

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
    
    
}
