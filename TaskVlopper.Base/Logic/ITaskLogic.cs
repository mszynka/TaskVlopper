using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskVlopper.Base.Logic
{
    public interface ITaskLogic
    {
        IEnumerable<TaskVlopper.Base.Model.Task> GetAllTasksForGivenProjectAndCurrentUser(int projectId, string userId);
        void HandleTaskEdit(Model.Task task, int projectId, int id);
        void HandleTaskDelete(int projectId, int id, string userId);
        void HandleTaskAdd(Model.Task task, int projectId, string userId);
        TaskVlopper.Base.Model.Task HandleTaskGet(int projectId, int id);
        IEnumerable<string> GetTaskUsers(int projectId, int taskId);
        void AssignUserToProjectTask(int projectId, int taskId, string userId);

        // Statistic methods
        int CountAllTasksForGivenProjectAndCurrentUser(int projectId, string userId);
    }
}
