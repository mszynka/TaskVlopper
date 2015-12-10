using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Repository
{
    public interface IUserTaskAssignmentRepository : IBaseRepository<UserTaskAssignment>
    {
        IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserId(string userId);
        IEnumerable<UserTaskAssignment> GetTaskAssignmentByTaskId(int taskId);
        IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserIdAndTaskId(string userId, int taskId);
        UserTaskAssignment GetTaskAssignmentByUserIdAndProjectIdAndTaskId(string userId, int projectId, int taskId);
        IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserIdAndProjectId(string userId, int porjectId);
    }
}
