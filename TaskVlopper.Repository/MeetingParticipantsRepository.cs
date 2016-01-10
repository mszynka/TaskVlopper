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
        #region Enumerables
        public IEnumerable<MeetingParticipants> GetMeetingParticipantsByMeetingId(int meetingId)
        {
            return GetMeetingParticipantsByMeetingIdQueryable(meetingId).AsEnumerable();
        }

        public IEnumerable<MeetingParticipants> GetMeetingParticipantsByUserId(string userId)
        {
            return GetMeetingParticipantsByUserIdQueryable(userId).AsEnumerable();
        }
        public IEnumerable<string> GetAllUsersIDsByMeeting(int meetingId)
        {
            return this.GetAll().Where(x => x.MeetingID == meetingId).Select(x => x.UserID);
        }
        #endregion
        #region Queryables
        public IQueryable<MeetingParticipants> GetMeetingParticipantsByMeetingIdQueryable(int meetingId)
        {
            return this.GetAll().Where(x => x.MeetingID == meetingId);
        }

        public IQueryable<MeetingParticipants> GetMeetingParticipantsByUserIdQueryable(string userId)
        {
            return this.GetAll().Where(x => x.UserID == userId);
        }

        public IQueryable<MeetingParticipants> GetMeetingParticipantsByUserIdAndMeetingIdQueryable(string userId, int meetingId)
        {
            return this.GetAll().Where(x => x.UserID == userId && x.MeetingID == meetingId);
        }
        #endregion

        public MeetingParticipants GetMeetingParticipantsByUserIdAndMeetingId(string userId, int meetingId)
        {
            return GetMeetingParticipantsByUserIdAndMeetingIdQueryable(userId, meetingId).Single();
        }

        
        
    }
}
