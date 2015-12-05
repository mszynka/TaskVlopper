﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Repository
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
       
        Project GetProjectById(int id);
    }
}
