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
            Projects = projects;
        }

        public IList<Project> Projects { get; private set; }
    }

    public class ProjectViewModel
    {
        public ProjectViewModel(Project project)
        {
            Project = project;
        }

        public Project Project { get; private set; }
    }
}