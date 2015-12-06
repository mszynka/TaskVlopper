using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskVlopper.Base.Logic
{
    public interface IMeetingLogic
    {
        IEnumerable<TaskVlopper.Base.Model.Meeting> GetAllMeetingsForCurrentUser(string userId);
        IEnumerable<TaskVlopper.Base.Model.Meeting> GetAllMeetingsForCurrentUserAndProject(string userId, int projectId);
        IEnumerable<TaskVlopper.Base.Model.Meeting> GetAllMeetingsForCurrentUserAndProjectAndTask(string userId, int projectId, int taskId);
        void HandleMeetingEdit(NameValueCollection form, int projectId, int? taskId, int id);
        void HandleMeetingDelete(int projectId, int? taskId, int id, string userId);
        void HandleMeetingAdd(NameValueCollection form, int projectId, int? taskId, string userId);
        TaskVlopper.Base.Model.Meeting HandleMeetingGet(int projectId, int? taskId, int id);
    }
}
