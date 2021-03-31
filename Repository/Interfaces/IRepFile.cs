using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface IRepFile<T>
    {
        public ICollection<T> GetAll();

        public void Update(T _object);
        public void Delete(T _object);
        public T GetById(int Id);

    }
}
