using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Models
{
    public class WorklogsViewModel
    {
        public WorklogsViewModel(IList<Worklog> worklog)
        {
            Worklog = worklog;
        }

        public IList<Worklog> Worklog { get; private set; }
    }

    public class WorklogViewModel
    {
        public WorklogViewModel(Worklog worklog)
        {
            Worklog = worklog;
        }

        public Worklog Worklog { get; private set; }
    }
}
