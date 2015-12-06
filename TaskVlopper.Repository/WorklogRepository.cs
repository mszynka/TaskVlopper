using System;
using System.Collections.Generic;
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
        public Worklog GetWorklogById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
