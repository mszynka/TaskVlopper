using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{
    public class UserTaskAssignmentRepository : BaseRepository<UserTaskAssignment>, IUserTaskAssignmentRepository
    {
        public IEnumerable<UserTaskAssignment> GetTaskAssignmentByTaskId(int taskId)
        {
            return this.GetAll().Where(x => x.TaskID == taskId);
        }

        public IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserId(string userId)
        {
            return this.GetAll().Where(x => x.UserID == userId);
        }

        public IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserIdAndTaskId(string userId, int taskId)
        {
            return this.GetAll().Where(x => x.TaskID == taskId && x.UserID == userId);
        }

        public UserTaskAssignment GetTaskAssignmentByUserIdAndProjectIdAndTaskId(string userId, int projectId, int taskId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId && x.TaskID == taskId && x.UserID == userId).Single();
        }

        public IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserIdAndProjectId(string userId, int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId && x.UserID == userId);
        }
    }
}
