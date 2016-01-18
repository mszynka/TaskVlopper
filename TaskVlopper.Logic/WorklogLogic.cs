using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;

namespace TaskVlopper.Logic
{
    
    public class WorklogLogic : IWorklogLogic
    {
        private readonly IProjectRepository ProjectRepository;
        private readonly ITaskRepository TaskRepository;
        private readonly IWorklogRepository WorklogRepository;

        public WorklogLogic(IProjectRepository projectRepository, ITaskRepository taskRepository, IWorklogRepository worklogRepository)
        {
            ProjectRepository = projectRepository;
            TaskRepository = taskRepository;
            WorklogRepository = worklogRepository;
        }

        public IEnumerable<Worklog> GetAllWorklogForGivenProjectAndTaskAndUser(int projectId, int taskId, string userId)
        {
            return WorklogRepository
                .GetAll()
                .Where(x => x.TaskID == taskId && x.UserID == userId);
        }

        public void HandleWorklogAdd(Worklog worklog, int projectId, int taskId, string userId)
        {
            worklog.TaskID = taskId;
            worklog.UserID = userId;
            WorklogRepository.Add(worklog);
        }

        public Worklog HandleWorklogGet(int projectId, int taskId, int id)
        {
            return WorklogRepository.GetWorklogByIdWithoutTracking(id);
        }

        public void HandleWorklogDelete(int projectId, int taskId, int id, string userId)
        {
            var worklog = WorklogRepository.GetWorklogByIdWithoutTracking(id);
            WorklogRepository.Remove(worklog);
        }

        public void HandleWorklogEdit(Worklog worklog, int projectId, int taskId, int id)
        {
            worklog.TaskID = taskId;
            worklog.ID = id;
            WorklogRepository.Update(worklog);
        }

        public int GetHoursCountForGivenTaskId(int taskId)
        {
            if (!WorklogRepository.GetAll().Any())
                return 0;

            return WorklogRepository
                .GetAll()
                .Where(x => x.TaskID == taskId)
                .Sum(x => x.Hours);
        }
    }
}