using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Logic
{
    public class TaskLogic : ITaskLogic
    {
        public IEnumerable<Base.Model.Task> GetAllTasksForGivenProjectAndCurrentUser(int projectId, string userId)
        {
            throw new NotImplementedException();
        }

        public void HandleTaskAdd(NameValueCollection form, int projectId, string userId)
        {
            throw new NotImplementedException();
        }

        public void HandleTaskDelete(int projectId, int id, string userId)
        {
            throw new NotImplementedException();
        }

        public void HandleTaskEdit(NameValueCollection form, int projectId, int id)
        {
            throw new NotImplementedException();
        }

        public Base.Model.Task HandleTaskGet(int projectId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
