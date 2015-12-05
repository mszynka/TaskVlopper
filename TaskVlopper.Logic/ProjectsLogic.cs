using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Repository;
using TaskVlopper.Base.Model;
using TaskVlopper.Base;

namespace TaskVlopper.Logic
{
    public class ProjectsLogic : IProjectsLogic
    {
        private readonly IProjectRepository ProjectRepository;

        private readonly IUserProjectAssignmentRepository UserProjectAssignmentRepository;

        private readonly IBaseSerializer<Project> Serializer;

        public ProjectsLogic(IProjectRepository projectRepository, IUserProjectAssignmentRepository userProjectAssignmentRepository,
            IBaseSerializer<Project> serializer)
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

        public void HandleProjectEdit(Project project)
        {
            ProjectRepository.Update(project);
        }

        public void HandleProjectDelete(Project project)
        {
            ProjectRepository.Remove(project);
            //UserProjectAssignmentRepository.Remove()
        }

        public void HandleProjectAdd(Project project)
        {
            ProjectRepository.Add(project);
            ///UserProjectAssignmentRepository.Add()
        }

        public void HandleProjectGet(int id)
        {
            ProjectRepository.GetByProjectId(id);
        }

    }
}
