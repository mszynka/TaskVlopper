using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{
    public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
    {

        public Meeting GetMeetingByIdWithTracking(int id)
        {
            return this.GetMeetingByIdWithTrackingQueryable(id).Single();
        }

        public Meeting GetMeetingByIdWithoutTracking(int id)
        {
            return this.GetMeetingByIdWithoutTrackingQueryable(id).Single();
        }

        #region Enumerables
        public IEnumerable<Meeting> GetMeetingByProjectId(int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId);
        }

        public IEnumerable<Meeting> GetMeetingByProjectIdAndTaskId(int projectId, int taskId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId && x.TaskID == taskId);
        }
        #endregion

        #region Queryables
        public IQueryable<Meeting> GetMeetingByIdWithTrackingQueryable(int id)
        {
            return this.GetAll().Where(x => x.ID == id);
        }

        public IQueryable<Meeting> GetMeetingByIdWithoutTrackingQueryable(int id)
        {
            return this.GetAll().AsNoTracking().Where(x => x.ID == id);
        }
        #endregion
    }
}
