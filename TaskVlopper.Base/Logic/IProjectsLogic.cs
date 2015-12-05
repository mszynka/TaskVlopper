using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Base.Logic
{
    public interface IProjectsLogic
    {
        IEnumerable<Project> GetAllProjectsForCurrentUser(string userId);
        void HandleProjectEdit(Project project);
        void HandleProjectDelete(Project project);
        void HandleProjectAdd(Project project);
        void HandleProjectGet(int id);
    }
}
