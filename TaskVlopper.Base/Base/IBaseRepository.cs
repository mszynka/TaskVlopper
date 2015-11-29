using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskVlopper.Base.Base
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T element);
        void Update(T element);
        void Remove(T element);
        void RemoveAll();
    }
}
