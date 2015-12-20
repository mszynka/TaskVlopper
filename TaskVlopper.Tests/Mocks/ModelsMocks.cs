using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Tests.Mocks
{
    public static class ModelsMocks
    {
        public static TaskVlopper.Base.Model.Task TaskModelFirst = new TaskVlopper.Base.Model.Task()
        {
            ID = 1,
            Description = "TaskDescription",
            EndDate = new DateTime(2015, 01, 01),
            EstimatedTimeInHours = 100,
            ExecutiveUserID = "Logged",
            Name = "Task1",
            ProjectID = 1,
            StartDate = new DateTime(2014, 01, 01),
            Status = Base.Enums.TaskStatusEnum.Started,
            Storypoints = 123
        };

        public static TaskVlopper.Base.Model.Task TaskModelSecond = new TaskVlopper.Base.Model.Task()
        {
            ID = 2,
            Description = "TaskDescription2",
            EndDate = new DateTime(2015, 02, 02),
            EstimatedTimeInHours = 102,
            ExecutiveUserID = "Logged",
            Name = "Task2",
            ProjectID = 2,
            StartDate = new DateTime(2014, 01, 01),
            Status = Base.Enums.TaskStatusEnum.Active,
            Storypoints = 123
        };

        public static Project ProjectModelFirst = new Project()
        {
            ID = 1,
            Deadline = new DateTime(2015, 12, 12),
            Description = "Description",
            EstimatedTimeInHours = 50,
            Name = "Name",
            StartDate = new DateTime(2015, 01, 01),
        };

        public static Project ProjectModelSecond = new Project()
        {
            ID = 2,
            Deadline = new DateTime(2015, 12, 12),
            Description = "DescriptionSecond",
            EstimatedTimeInHours = 50,
            Name = "NameSecond",
            StartDate = new DateTime(2015, 01, 01),
        };

        public static FormCollection Form = new FormCollection()
        {
            {"Project.Deadline", new DateTime(2015, 12, 12).ToString() },
            {"Project.Description", "Description" },
            {"Project.EstimatedTimeInHours", "" + 50 },
            {"Project.Name", "Name"},    
            {"Project.StartDate", new DateTime(2015, 01, 01).ToString() }        
        };
        
    }
}
