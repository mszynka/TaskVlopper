using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Models
{
    public class ProjectsViewModel
    {
        public ProjectsViewModel(IList<Project> projects)
        {
            Projects = new List<ProjectViewModel>();
            foreach(var project in projects)
            {
                Projects.Add(new ProjectViewModel(project));
            }
        }

        public ProjectsViewModel(IList<ProjectViewModel> projects)
        {
            Projects = projects;
        }

        public IList<ProjectViewModel> Projects { get; private set; }
    }

    public class ProjectViewModel
    {
        public ProjectViewModel(Project project)
        {
            Project = project;
            Stats = null;
        }

        public ProjectViewModel(Project project, ProjectStatisticsViewModel stats)
        {
            Project = project;
            Stats = stats;
        }

        public Project Project { get; private set; }
        public ProjectStatisticsViewModel Stats { get; private set; }
    }

    public class ProjectStatisticsViewModel
    {
        public ProjectStatisticsViewModel(int? taskCount, int? futureMeetingCount, int? boundUsers)
        {
            TaskCount = taskCount;
            FutureMeetingCount = futureMeetingCount;
            BoundUsers = boundUsers;
        }

        public int? TaskCount { get; private set; }
        public int? FutureMeetingCount { get; private set; }
        public int? BoundUsers { get; private set; }
    }
}