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
        private IWorklogRepository WorklogicRepository { get; }

        public IEnumerable<Worklog> GetAllWorklogForGivenProjectAndTaskAndUser(int projectId, int taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Worklog> GetAllWorklogForGivenProjectAndUser(int projectId, string userId)
        {
            throw new NotImplementedException();
        }

        public void HandleWorklogAdd(NameValueCollection form, int projectId, int taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public Worklog HandleWorklogGet(int projectId, int taskId, int id)
        {
            throw new NotImplementedException();
        }

        public void HandleWorklogDelete(int projectId, int taskId, int id, string userId)
        {
            throw new NotImplementedException();
        }

        public void HandleWorklogEdit(NameValueCollection form, int projectId, int taskId, int id)
        {
            throw new NotImplementedException();
        }
    }
}
