using Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Common
{
    class Repository<T> : IRepository<T> where T : class
    {
        private Context _context;
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
           
        }

        public List<T> GetAll()
        {
            return null;
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
