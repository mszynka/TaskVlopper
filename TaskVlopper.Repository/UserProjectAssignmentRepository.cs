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
        public IEnumerable<UserProjectAssignment> GetProjectAssignmentByUserId(string userId)
        {
            return this.GetAll().Where(x => x.UserID == userId);
        }

        public IEnumerable<UserProjectAssignment> GetProjectAssignmentByProjectId(int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId);
        }

        public UserProjectAssignment GetProjectAssignmentByUserIdAndProjectId(string userId, int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId && x.UserID == userId ).Single();
        }
    }
}
