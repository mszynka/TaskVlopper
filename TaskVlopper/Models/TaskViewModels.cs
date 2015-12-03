using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Models
{
    public class TasksViewModel
    {
        public TasksViewModel(IList<Tasks> tasks)
        {
            Tasks = tasks;
        }

        public IList<Tasks> Tasks { get; private set; }
    }

    public class TaskViewModel
    {
        public TaskViewModel(Tasks tasks)
        {
            Tasks = tasks;
        }

        public Tasks Tasks { get; private set; }
    }
}