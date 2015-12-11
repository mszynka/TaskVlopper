using System;
using System.Collections.Generic;
using System.Linq;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Models
{
    public class TasksViewModel
    {
        public TasksViewModel(IList<Base.Model.Task> tasks)
        {
            Tasks = tasks;
        }

        public IList<Base.Model.Task> Tasks { get; private set; }
    }

    public class TaskViewModel
    {
        public TaskViewModel(Base.Model.Task tasks)
        {
            Task = tasks;
        }

        public Task Task { get; private set; }
    }
}