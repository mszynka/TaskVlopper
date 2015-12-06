using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{

    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {

        public Project GetProjectById(int id)
        {
            return this.GetAll().AsNoTracking().Where(x => x.ID == id).Single();
        }
    }
}
