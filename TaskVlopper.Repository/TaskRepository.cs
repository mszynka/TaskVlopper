using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Repository
{
    public class TaskRepository : BaseRepository<TaskVlopper.Base.Model.Task>, ITaskRepository
    {
        public Task GetTaskByIdWithoutTracking(int id)
        {
            return this.GetTaskByIdWithoutTrackingQueryable(id).Single();
        }

        public Task GetTaskByIdWithTracking(int id)
        {
            return GetTaskByIdWithTrackingQueryable(id).Single();
        }
        #region Enumerables
        public IEnumerable<Task> GetTasksForGivenProjectId(int projectId)
        {
            return this.GetAll().Where(x => x.ProjectID == projectId);
        }
        #endregion

        #region Queryables
        public IQueryable<Task> GetTaskByIdWithoutTrackingQueryable(int id)
        {
            return this.GetAll().AsNoTracking().Where(x => x.ID == id);
        }

        public IQueryable<Task> GetTaskByIdWithTrackingQueryable(int id)
        {
            return this.GetAll().Where(x => x.ID == id);
        }
        #endregion

        
    }
}
