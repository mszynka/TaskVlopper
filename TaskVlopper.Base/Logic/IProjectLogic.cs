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
        IEnumerable<string> GetAllUsersForGivenProject(int projectId);
        void HandleProjectEdit(Project form, int projectId);
        void HandleProjectDelete(int projectId, string userId);
        void HandleProjectAdd(Project project, string userId);
        void AssignUserToProject(int projectId, string userId);
        Project HandleProjectGet(int id);

	#region Statistics
        
	int CountAllUsersForProject(int projectId);

	#endregion
    }
}
