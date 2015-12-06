using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Repository;
using TaskVlopper.Base.Model;
using TaskVlopper.Base;
using TaskVlopper.Base.Repository.Serialize;
using System.Web;
using System.Collections.Specialized;

namespace TaskVlopper.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository ProjectRepository;

        private readonly IUserProjectAssignmentRepository UserProjectAssignmentRepository;

        private readonly IProjectSerialize Serializer;

        public ProjectLogic(IProjectRepository projectRepository, IUserProjectAssignmentRepository userProjectAssignmentRepository,
            IProjectSerialize serializer)
        {
            ProjectRepository = projectRepository;
            UserProjectAssignmentRepository = userProjectAssignmentRepository;
            Serializer = serializer;
        }

        public IEnumerable<Project> GetAllProjectsForCurrentUser(string userId)
        {
            var assignment = UserProjectAssignmentRepository.GetAll().Where(x => x.UserID == userId);

            List<Project> projectsForCurrentUser = new List<Project>();
            foreach (var ass in assignment)
            {
                projectsForCurrentUser.Add(ProjectRepository.GetAll().Where(x => x.ID == ass.ProjectID).Single());
            }

            return projectsForCurrentUser.AsEnumerable();
        }

        public void HandleProjectEdit(NameValueCollection form, int projectId)
        {
            Project project = Serializer.Serialize(form);
            project.ID = projectId;

            ProjectRepository.Update(project);
        }

        public void HandleProjectDelete(int projectId, string userId)
        {
            
            ProjectRepository.Remove(ProjectRepository.GetProjectById(projectId));

            UserProjectAssignment userProjectAssignment = UserProjectAssignmentRepository.
                GetProjectAssignmentByUserIdAndProjectId(userId, projectId);
            userProjectAssignment.UserID = userId;
            userProjectAssignment.ProjectID = projectId;
            UserProjectAssignmentRepository.Remove(userProjectAssignment);
        }

        public void HandleProjectAdd(NameValueCollection form, string userId)
        {
            Project project = Serializer.Serialize(form);
            ProjectRepository.Add(project);

            UserProjectAssignment userProjectAssignment = new UserProjectAssignment();
            userProjectAssignment.UserID = userId;
            userProjectAssignment.ProjectID = project.ID;

            UserProjectAssignmentRepository.Add(userProjectAssignment);
        }

        public Project HandleProjectGet(int id)
        {
            return ProjectRepository.GetProjectById(id);
        }

    }
}
