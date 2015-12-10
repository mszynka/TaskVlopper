using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Repository
{
    public interface IMeetingRepository : IBaseRepository<Meeting>
    {
        Meeting GetMeetingByIdWithTracking(int id);
        Meeting GetMeetingByIdWithoutTracking(int id);
        IEnumerable<Meeting> GetMeetingByProjectId(int projectId);
        IEnumerable<Meeting> GetMeetingByProjectIdAndTaskId(int projectId, int taskId);
    }
}
