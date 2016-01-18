using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{
    public class UserProjectAssignmentRepository : BaseRepository<UserProjectAssignment>, IUserProjectAssignmentRepository
    {
        #region Enumerables
        public IEnumerable<UserProjectAssignment> GetProjectAssignmentByUserId(string userId)
        {
            return GetProjectAssignmentByUserIdQueryable(userId).AsEnumerable();
        }

        public IEnumerable<UserProjectAssignment> GetProjectAssignmentByProjectId(int projectId)
        {
            return GetProjectAssignmentByProjectIdQueryable(projectId).AsEnumerable();
        }

        public IEnumerable<string> GetAllUsersIDsForGivenProject(int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId).Select(x => x.UserID);
        }
        #endregion

        #region Queryables

        public IQueryable<UserProjectAssignment> GetProjectAssignmentByUserIdQueryable(string userId)
        {
            return this.GetAll().Where(x => x.UserID == userId);
        }

        public IQueryable<UserProjectAssignment> GetProjectAssignmentByProjectIdQueryable(int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId);
        }
        public IQueryable<UserProjectAssignment> GetProjectAssignmentByUserIdAndProjectIdQueryable(string userId, int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId && x.UserID == userId);
        }

        #endregion
        public UserProjectAssignment GetProjectAssignmentByUserIdAndProjectId(string userId, int projectId)
        {
            return this.GetProjectAssignmentByUserIdAndProjectIdQueryable(userId, projectId).Single();
        }    
    }
}
