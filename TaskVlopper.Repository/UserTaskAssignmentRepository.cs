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

        public UserTaskAssignment GetTaskAssignmentByUserIdAndTaskId(string userId, int taskId)
        {
            return this.GetAll().Where(x => x.TaskID == taskId && x.UserID == userId).Single();
        }
    }
}
