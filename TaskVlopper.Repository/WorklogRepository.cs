using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{
    public class WorklogRepository : BaseRepository<Worklog>, IWorklogRepository
    {
        #region Queryables
        public IQueryable<Worklog> GetWorklogByIdWithTrackingQueryable(int id)
        {
            return this.GetAll().Where(x => x.ID == id);
        }

        public IQueryable<Worklog> GetWorklogByIdWithoutTrackingQueryable(int id)
        {
            return this.GetAll().AsNoTracking().Where(x => x.ID == id);
        }
        #endregion

        public Worklog GetWorklogByIdWithoutTracking(int id)
        {
            return this.GetWorklogByIdWithoutTrackingQueryable(id).Single();
        }

        public Worklog GetWorklogByIdWithTracking(int id)
        {
            return this.GetWorklogByIdWithTrackingQueryable(id).Single();
        }

        
    }
}
