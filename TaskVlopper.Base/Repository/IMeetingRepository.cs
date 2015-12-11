using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Repository
{
    public interface IMeetingRepository : IBaseRepository<Meeting>
    {
        Meeting GetMeetingById(int id);
    }
}
