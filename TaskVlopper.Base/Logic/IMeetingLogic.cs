using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Logic
{
    public interface IMeetingLogic
    {
        IEnumerable<Meeting> GetAllMeetingsForCurrentUser(string userId);
        IEnumerable<Meeting> GetAllMeetingsForCurrentUserAndProject(string userId, int projectId);
        IEnumerable<Meeting> GetAllMeetingsForCurrentUserAndProjectAndTask(string userId, int projectId, int taskId);
        IEnumerable<string> GetAllUsersForGivenMeeting(int meetingId);
        void HandleMeetingEdit(Meeting meeting, int projectId, int? taskId, int id);
        void HandleMeetingDelete(int projectId, int? taskId, int id, string userId);
        void HandleMeetingAdd(Meeting meeting, int projectId, int? taskId, string userId);
        Meeting HandleMeetingGet(int projectId, int? taskId, int id);
        void AssignUserToMeeting(int meetingId, string userId);
        
    }
}
