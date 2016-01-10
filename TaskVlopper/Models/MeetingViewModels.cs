using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Models
{
    public class MeetingsViewModel
    {
        public MeetingsViewModel(IList<Meeting> meetings)
        {
            Meetings = new List<MeetingViewModel>();
            foreach(var meeting in meetings)
            {
                Meetings.Add(new MeetingViewModel(meeting));
            }
        }

        public MeetingsViewModel(IList<MeetingViewModel> meetings)
        {
            Meetings = meetings;
        }

        public IList<MeetingViewModel> Meetings { get; private set; }
    }

    public class MeetingViewModel
    {
        public MeetingViewModel(Meeting meeting)
        {
            Meeting = meeting;
            Stats = null;
        }

        public MeetingViewModel(Meeting meeting, MeetingStatisticsViewModel stats)
        {
            Meeting = meeting;
            Stats = stats;
        }

        public Meeting Meeting { get; private set; }
        public MeetingStatisticsViewModel Stats { get; private set; }
    }

    public class MeetingStatisticsViewModel
    {
        public MeetingStatisticsViewModel(int? participantsCount)
        {
            ParticipantsCount = participantsCount;
        }

        public int? ParticipantsCount { get; private set; }
    }
}