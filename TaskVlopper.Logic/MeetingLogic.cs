using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;

namespace TaskVlopper.Logic
{
    public class MeetingLogic : IMeetingLogic
    {
        private readonly IMeetingRepository MeetingRepository;
        private readonly IMeetingParticipantsRepository MeetingParticipantsRepository;

        public MeetingLogic(IMeetingRepository meetingRepository, IMeetingParticipantsRepository meetingParticipantsRepository)
        {
            MeetingRepository = meetingRepository;
            MeetingParticipantsRepository = meetingParticipantsRepository;
        }

        public IEnumerable<Meeting> GetAllMeetingsForCurrentUser(string userId)
        {
            return MeetingParticipantsRepository.GetMeetingParticipantsByUserId(userId)
                    .Select(meetingParticipants => 
                        MeetingRepository.GetMeetingByIdWithoutTrackingQueryable(meetingParticipants.MeetingID).Single())
                    .ToList();
        }

        public IEnumerable<Meeting> GetAllMeetingsForCurrentUserAndProject(string userId, int projectId)
        {
            return MeetingRepository.GetMeetingByProjectId(projectId)
                .Where(meetingByProject => 
                    MeetingParticipantsRepository.GetMeetingParticipantsByUserId(userId)
                        .Any(meetingParticipants => meetingParticipants.MeetingID == meetingByProject.ID)
                    )
                .ToList();
        }

        public IEnumerable<Meeting> GetAllMeetingsForCurrentUserAndProjectAndTask(string userId, int projectId, int taskId)
        {
            return MeetingRepository.GetMeetingByProjectIdAndTaskId(projectId, taskId)
                .Where(meetingByProject => 
                    MeetingParticipantsRepository.GetMeetingParticipantsByUserId(userId)
                        .Any(meetingParticipants => meetingParticipants.MeetingID == meetingByProject.ID)
                    )
                .ToList();
        }

        public void HandleMeetingAdd(Meeting meeting, int projectId, int? taskId, string userId)
        {
            meeting.ProjectID = projectId;
            meeting.TaskID = taskId;
            MeetingRepository.Add(meeting);

            MeetingParticipants participant = new MeetingParticipants();
            participant.MeetingID = meeting.ID;
            participant.UserID = userId;
            MeetingParticipantsRepository.Add(participant);
        }

        public void HandleMeetingDelete(int projectId, int? taskId, int id, string userId)
        {
            var meeting = MeetingRepository.GetMeetingByIdWithTracking(id);
            MeetingParticipantsRepository.Remove(MeetingParticipantsRepository.GetMeetingParticipantsByUserIdAndMeetingId(userId, meeting.ID));
            MeetingRepository.Remove(meeting);
        }

        public void HandleMeetingEdit(Meeting meeting, int projectId, int? taskId, int id)
        {
            meeting.ID = id;
            meeting.ProjectID = projectId;
            if (taskId != null) meeting.TaskID = taskId;
            MeetingRepository.Update(meeting);
        }

        public Meeting HandleMeetingGet(int projectId, int? taskId, int id)
        {
            return MeetingRepository.GetMeetingByIdWithoutTracking(id);
        }

        public void AssignUserToMeeting(int meetingId, string userId)
        {
            MeetingParticipants participant = new MeetingParticipants();
            participant.MeetingID = meetingId;
            participant.UserID = userId;

            MeetingParticipantsRepository.Add(participant);
        }

        public IEnumerable<string> GetMeetingUsers(int meetingId)
        {
            return MeetingParticipantsRepository.GetAllUsersIDsByMeeting(meetingId);
        }
    }
}
