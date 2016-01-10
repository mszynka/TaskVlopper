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
        IEnumerable<string> GetAllUsersIDsForGivenTaskProject(int projectId, int taskId);

        IQueryable<UserTaskAssignment> GetTaskAssignmentByUserIdQueryable(string userId);
        IQueryable<UserTaskAssignment> GetTaskAssignmentByTaskIdQueryable(int taskId);
        IQueryable<UserTaskAssignment> GetTaskAssignmentByUserIdAndTaskIdQueryable(string userId, int taskId);
        IQueryable<UserTaskAssignment> GetTaskAssignmentByUserIdAndProjectIdQueryable(string userId, int porjectId);
        IQueryable<string> GetAllUsersIDsForGivenTaskProjectQueryable(int projectId, int taskId);
    }
}
