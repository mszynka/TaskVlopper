using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskVlopper.Base
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Add(T element);
        void AddMany(IEnumerable<T> element);
        void Update(T element);
        void UpdateMany(IEnumerable<T> elements);
        void Remove(T element);
        void RemoveAll();
        void RemoveMany(IEnumerable<T> elements);
    }
}
