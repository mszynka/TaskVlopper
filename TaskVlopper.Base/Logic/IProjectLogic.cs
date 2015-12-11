using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Logic
{
    public interface IProjectLogic
    {
        IEnumerable<Project> GetAllProjectsForCurrentUser(string userId);
        void HandleProjectEdit(NameValueCollection form, int projectId);
        void HandleProjectDelete(int projectId, string userId);
        void HandleProjectAdd(NameValueCollection form, string userId);
        Project HandleProjectGet(int id);
    }
}
