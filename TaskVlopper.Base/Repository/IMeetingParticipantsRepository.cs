using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Repository
{
    public interface IMeetingParticipantsRepository : IBaseRepository<MeetingParticipants>
    {
        IEnumerable<MeetingParticipants> GetMeetingParticipantsByUserId(string userId);
        IEnumerable<MeetingParticipants> GetMeetingParticipantsByMeetingId(int meetingId);
        MeetingParticipants GetMeetingParticipantsByUserIdAndMeetingId(string userId, int meetingId);
    }
}
