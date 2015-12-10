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
        public Worklog GetWorklogByIdWithoutTracking(int id)
        {
            return this.GetAll().AsNoTracking().Where(x => x.ID == id).Single();
        }
        public Worklog GetWorklogByIdWithTracking(int id)
        {
            return this.GetAll().Where(x => x.ID == id).Single();
        }
    }
}
