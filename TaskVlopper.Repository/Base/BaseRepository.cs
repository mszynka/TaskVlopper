using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Transactions;
using TaskVlopper.Base;
using TaskVlopper.Base.Repository;

namespace TaskVlopper.Repository.Base
{
    public abstract class BaseRepository<T> : TaskVlopper.Base.IBaseRepository<T>, IDisposable
        where T : class
    {

        protected DbContext Ctx { get; set; }
        public BaseRepository()
        {
            Ctx = new TaskVlopperDatabase<T>();
        }

        public void Add(T element)
        {
            using (var transaction = new TransactionScope())
            {
                Ctx.Entry<T>(element).State = EntityState.Added;
                Ctx.SaveChanges();

                transaction.Complete();
            }
        }
        public void Dispose()
        {
            if (Ctx != null)
            {
                Ctx.Dispose();
                Ctx = null;
            }
        }

        public IQueryable<T> GetAll()
        {
            return Ctx.Set<T>().AsQueryable();
        }

        public void Remove(T element)
        {
            using (var transaction = new TransactionScope())
            {
                Ctx.Entry<T>(element).State = System.Data.Entity.EntityState.Deleted;
                Ctx.SaveChanges();

                transaction.Complete();
            }
        }

        public void RemoveAll()
        {
            using (var transaction = new TransactionScope())
            {
                IEnumerable<T> Entities = this.GetAll();
                Ctx.Set<T>().RemoveRange(Entities);
                Ctx.SaveChanges();

                transaction.Complete();
            }
        }

        public void Update(T element)
        {
            using (var transaction = new TransactionScope())
            {
                Ctx.Entry<T>(element).State = System.Data.Entity.EntityState.Modified;
                Ctx.SaveChanges();

                transaction.Complete();
            }
        }
    }
}
