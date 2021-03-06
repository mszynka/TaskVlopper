﻿using System;
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
            return UserProjectAssignmentRepository
                    .GetAll()
                    .Where(x => x.UserID == userId).AsEnumerable()
                    .Select(x => ProjectRepository.GetProjectByIdWithoutTrackingQueryable(x.ProjectID).Single());
        }

        public void HandleProjectEdit(Project form, int projectId)
        {
            //Project project = Serializer.Serialize(form);
            form.ID = projectId;

            ProjectRepository.Update(form);
        }

        public void HandleProjectDelete(int projectId, string userId)
        {
            Project proj = ProjectRepository.GetProjectByIdWithTracking(projectId);
            ProjectRepository.Remove(proj);

            UserProjectAssignment userProjectAssignment = UserProjectAssignmentRepository.
                GetProjectAssignmentByUserIdAndProjectId(userId, projectId);
            userProjectAssignment.UserID = userId;
            userProjectAssignment.ProjectID = projectId;
            UserProjectAssignmentRepository.Remove(userProjectAssignment);
        }

        public void HandleProjectAdd(Project project, string userId)
        {
            //Project project = Serializer.Serialize(form);
            ProjectRepository.Add(project);

            UserProjectAssignment userProjectAssignment = new UserProjectAssignment();
            userProjectAssignment.UserID = userId;
            userProjectAssignment.ProjectID = project.ID;

            UserProjectAssignmentRepository.Add(userProjectAssignment);
        }

        public Project HandleProjectGet(int id)
        {
            return ProjectRepository.GetProjectByIdWithoutTracking(id);
        }

        public IEnumerable<string> GetProjectUsers(int projectId)
        {
            return UserProjectAssignmentRepository.GetAllUsersIDsForGivenProject(projectId);
        }

        public void AssignUserToProject(int projectId, string userId)
        {
            UserProjectAssignment assignment = new UserProjectAssignment();
            assignment.ProjectID = projectId;
            assignment.UserID = userId;
            UserProjectAssignmentRepository.Add(assignment);
        }

    }
}
