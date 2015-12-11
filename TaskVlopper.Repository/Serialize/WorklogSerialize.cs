using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository.Serialize;

namespace TaskVlopper.Repository.Base.Serialize
{
    public class WorklogSerialize : BaseSerializer<Worklog>, IWorklogSerialize
    {
    }
}
