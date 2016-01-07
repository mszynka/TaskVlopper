using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{
    public class MeetingParticipantsRepository : BaseRepository<MeetingParticipants>, IMeetingParticipantsRepository
    {
        public IEnumerable<MeetingParticipants> GetMeetingParticipantsByMeetingId(int meetingId)
        {
            return this.GetAll().Where(x => x.MeetingID == meetingId);
        }

        public IEnumerable<MeetingParticipants> GetMeetingParticipantsByUserId(string userId)
        {
            return this.GetAll().Where(x => x.UserID == userId);
        }

        public MeetingParticipants GetMeetingParticipantsByUserIdAndMeetingId(string userId , int meetingId)
        {
            return this.GetAll().Where(x => x.UserID == userId && x.MeetingID == meetingId).Single();
        }

        public IEnumerable<string> GetAllUsersIDsByMeeting(int meetingId)
        {
            return this.GetAll().Where(x => x.MeetingID == meetingId).Select(x => x.UserID);
        }
    }
}
