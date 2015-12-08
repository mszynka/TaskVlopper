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
        UserTaskAssignment GetTaskAssignmentByUserIdAndTaskId(string userId, int taskId);
    }
}
