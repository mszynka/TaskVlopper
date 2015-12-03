using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TaskVlopper.Models
{
    public class TasksViewModel
    {
        public TasksViewModel(IList<Task> tasks)
        {
            Tasks = tasks;
        }

        public IList<Task> Tasks { get; private set; }
    }

    public class TaskViewModel
    {
        public TaskViewModel(Task tasks)
        {
            Tasks = tasks;
        }

        public Task Tasks { get; private set; }
    }
}