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
        void HandleTaskEdit(NameValueCollection form, int projectId, int id);
        void HandleTaskDelete(int projectId, int id, string userId);
        void HandleTaskAdd(NameValueCollection form, int projectId, string userId);
        TaskVlopper.Base.Model.Task HandleTaskGet(int projectId, int id);
    }
}
