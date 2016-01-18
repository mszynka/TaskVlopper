using System;
using System.Collections.Generic;
using System.Linq;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Models
{
    public class TasksViewModel
    {
        public TasksViewModel(IList<Base.Model.Task> tasks, IEnumerable<string> statuses)
        {
            Tasks = new List<TaskViewModel>();
            foreach(var task in tasks)
            {
                Tasks.Add(new TaskViewModel(task));
            }

            Statuses = statuses;
        }

        public TasksViewModel(IList<Base.Model.Task> tasks)
        {
            Tasks = new List<TaskViewModel>();
            foreach (var task in tasks)
            {
                Tasks.Add(new TaskViewModel(task));
            }
        }

        public TasksViewModel(IList<TaskViewModel> tasks, IEnumerable<string> statuses)
        {
            Tasks = tasks;
            Statuses = statuses;
        }

        public TasksViewModel(IList<TaskViewModel> tasks)
        {
            Tasks = tasks;
        }

        public IList<TaskViewModel> Tasks { get; private set; }
        public IEnumerable<string> Statuses { get; private set; }

    }

    public class TaskViewModel
    {
        public TaskViewModel(Base.Model.Task task)
        {
            Task = task;
            Stats = null;
        }

        public TaskViewModel(Base.Model.Task task, TaskStatisticsViewModel stats)
        {
            Task = task;
            Stats = stats;
        }

        public Task Task { get; private set; }
        public TaskStatisticsViewModel Stats { get; private set; }
    }

    public class TaskStatisticsViewModel
    {
        public TaskStatisticsViewModel(int? boundUsers, int? loggedHours, int? watchers)
        {
            BoundUsers = boundUsers;
            LoggedHours = loggedHours;
            Watchers = watchers;
        }

        public int? BoundUsers { get; private set; }
        public int? LoggedHours { get; private set; }
        public int? Watchers { get; private set; }
    }
}