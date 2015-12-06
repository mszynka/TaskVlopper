using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Logic
{
    public class MeetingLogic : IMeetingLogic
    {
        public IEnumerable<Meeting> GetAllMeetingsForCurrentUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meeting> GetAllMeetingsForCurrentUserAndProject(string userId, int projectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meeting> GetAllMeetingsForCurrentUserAndProjectAndTask(string userId, int projectId, int taskId)
        {
            throw new NotImplementedException();
        }

        public void HandleMeetingAdd(NameValueCollection form, int projectId, int? taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public void HandleMeetingDelete(int projectId, int? taskId, int id, string userId)
        {
            throw new NotImplementedException();
        }

        public void HandleMeetingEdit(NameValueCollection form, int projectId, int? taskId, int id)
        {
            throw new NotImplementedException();
        }

        public Meeting HandleMeetingGet(int projectId, int? taskId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
