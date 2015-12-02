using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(IList<Projects> projects)
        {
            Projects = projects;
        }

        public IList<Projects> Projects { get; private set; }
    }
}