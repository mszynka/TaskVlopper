using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Repository
{
    public interface IUserProjectAssignmentRepository : IBaseRepository<UserProjectAssignment>
    {
        IEnumerable<UserProjectAssignment> GetProjectAssignmentByUserId(string userId);
        IEnumerable<UserProjectAssignment> GetProjectAssignmentByProjectId(int projectId);
        UserProjectAssignment GetProjectAssignmentByUserIdAndProjectId(string userId, int projectId);
    }
}
