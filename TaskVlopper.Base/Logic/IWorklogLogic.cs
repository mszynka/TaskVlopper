using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Logic
{
    public interface IWorklogLogic
    {
        IEnumerable<TaskVlopper.Base.Model.Worklog> GetAllWorklogForGivenProjectAndTaskAndUser(int projectId, int taskId, string userId);
        void HandleWorklogEdit(Worklog worklog, int projectId, int taskId, int id);
        void HandleWorklogDelete(int projectId, int taskId, int id, string userId);
        void HandleWorklogAdd(Worklog worklog, int projectId,  int taskId,  string userId);
        TaskVlopper.Base.Model.Worklog HandleWorklogGet(int projectId, int taskId,  int id);
    }
}
