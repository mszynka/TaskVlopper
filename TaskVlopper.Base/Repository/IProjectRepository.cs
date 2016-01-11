using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Repository
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Project GetProjectByIdWithTracking(int id);
        Project GetProjectByIdWithoutTracking(int id);

        IQueryable<Project> GetProjectByIdWithTrackingQueryable(int id);
        IQueryable<Project> GetProjectByIdWithoutTrackingQueryable(int id);
    }
}
