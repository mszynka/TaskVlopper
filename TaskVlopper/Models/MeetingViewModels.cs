using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Models
{
    public class MeetingsViewModel
    {
        public MeetingsViewModel(IList<Meeting> meeting)
        {
            Meeting = meeting;
        }

        public IList<Meeting> Meeting { get; private set; }
    }

    public class MeetingViewModel
    {
        public MeetingViewModel(Meeting meeting)
        {
            Meeting = meeting;
        }

        public Meeting Meeting { get; private set; }
    }
}
