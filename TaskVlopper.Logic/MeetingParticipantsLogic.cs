using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Repository;

namespace TaskVlopper.Logic
{
    public class MeetingParticipantsLogic : IMeetingParticipantsLogic
    {
        private readonly IMeetingParticipantsRepository MeetingParticipantsRepository;

        public MeetingParticipantsLogic(IMeetingParticipantsRepository meetingParticipantsRepository)
        {
            MeetingParticipantsRepository = meetingParticipantsRepository;
        }
        public IEnumerable<string> GetMeetingUsers(int meetingId)
        {
           return MeetingParticipantsRepository.GetAllUsersIDsByMeeting(meetingId);
        }
    }
}
