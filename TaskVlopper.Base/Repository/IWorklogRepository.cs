using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Repository
{
    public interface IWorklogRepository : IBaseRepository<Worklog>
    {
        Worklog GetWorklogByIdWithoutTracking(int id);
        Worklog GetWorklogByIdWithTracking(int id);

        IQueryable<Worklog> GetWorklogByIdWithoutTrackingQueryable(int id);
        IQueryable<Worklog> GetWorklogByIdWithTrackingQueryable(int id);
    }
}
