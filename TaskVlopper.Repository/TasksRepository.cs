﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Base;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {
    }
}
