using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskVlopper.Base.Logic
{
    public interface IWorklogLogic
    {
        IEnumerable<TaskVlopper.Base.Model.Worklog> GetAllWorklogForGivenProjectAndUser(int projectId, string userId);
        IEnumerable<TaskVlopper.Base.Model.Worklog> GetAllWorklogForGivenProjectAndTaskAndUser(int projectId, int taskId, string userId);
        void HandleWorklogEdit(NameValueCollection form, int projectId, int taskId, int id);
        void HandleWorklogDelete(int projectId, int taskId, int id, string userId);
        void HandleWorklogAdd(NameValueCollection form, int projectId,  int taskId,  string userId);
        TaskVlopper.Base.Model.Worklog HandleWorklogGet(int projectId, int taskId,  int id);
    }
}
