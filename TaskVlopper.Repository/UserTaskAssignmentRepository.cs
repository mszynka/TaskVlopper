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
            return GetTaskAssignmentByTaskIdQueryable(taskId).AsEnumerable();
        }

        public IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserId(string userId)
        {
            return GetTaskAssignmentByUserIdQueryable(userId).AsEnumerable();
        }

        public IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserIdAndTaskId(string userId, int taskId)
        {
            return GetTaskAssignmentByUserIdAndTaskId(userId, taskId).AsEnumerable();
        }

        public UserTaskAssignment GetTaskAssignmentByUserIdAndProjectIdAndTaskId(string userId, int projectId, int taskId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId && x.TaskID == taskId && x.UserID == userId).Single();
        }

        public IEnumerable<UserTaskAssignment> GetTaskAssignmentByUserIdAndProjectId(string userId, int projectId)
        {
            return GetTaskAssignmentByUserIdAndProjectIdQueryable(userId, projectId).AsEnumerable();
        }

        public IEnumerable<string> GetAllUsersIDsForGivenTaskProject(int projectId, int taskId)
        {
            return GetAllUsersIDsForGivenTaskProjectQueryable(projectId, taskId).AsEnumerable();
        }

        //Queryables
        public IQueryable<UserTaskAssignment> GetTaskAssignmentByTaskIdQueryable(int taskId)
        {
            return this.GetAll().Where(x => x.TaskID == taskId);
        }

        public IQueryable<UserTaskAssignment> GetTaskAssignmentByUserIdQueryable(string userId)
        {
            return this.GetAll().Where(x => x.UserID == userId);
        }

        public IQueryable<UserTaskAssignment> GetTaskAssignmentByUserIdAndTaskIdQueryable(string userId, int taskId)
        {
            return this.GetAll().Where(x => x.TaskID == taskId && x.UserID == userId);
        }

        public IQueryable<UserTaskAssignment> GetTaskAssignmentByUserIdAndProjectIdQueryable(string userId, int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId && x.UserID == userId);
        }

        public IQueryable<string> GetAllUsersIDsForGivenTaskProjectQueryable(int projectId, int taskId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId && x.TaskID == taskId).Select(x => x.UserID);
        }
    }
}
