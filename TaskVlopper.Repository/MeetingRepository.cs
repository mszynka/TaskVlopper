using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{
    public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
    {
        public Meeting GetMeetingById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
