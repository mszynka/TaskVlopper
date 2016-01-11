using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Models;
using TaskVlopper.ServiceLocator;

namespace TaskVlopper.Tests.Mocks
{
    public static class ModelsMocks
    {
        public static void CleanUpBeforeTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var projRepo = container.Resolve<IProjectRepository>();
                var projAssignmentRepo = container.Resolve<IUserProjectAssignmentRepository>();

                var taskRepo = container.Resolve<ITaskRepository>();
                var taskAssignmentRepo = container.Resolve<IUserTaskAssignmentRepository>();

                var worklogRepo = container.Resolve<IWorklogRepository>();

                var meetingRepo = container.Resolve<IMeetingRepository>();
                var meetingAssignmentRepo = container.Resolve<IMeetingParticipantsRepository>();

                meetingRepo.RemoveAll();
                meetingAssignmentRepo.RemoveAll();
                projRepo.RemoveAll();
                projAssignmentRepo.RemoveAll();
                taskRepo.RemoveAll();
                taskAssignmentRepo.RemoveAll();
                worklogRepo.RemoveAll();
            }
        }

        public static List<ApplicationUser> GetApplicationUserList()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            users.Add(FirstUser);
            users.Add(LoggedUser);
            users.Add(NotLoggedUser);
            return users;
        }

        public static string FirstUserEmail = "one@x.pl";
        public static ApplicationUser FirstUser = new ApplicationUser
        {
            Email = FirstUserEmail
        };

        public static ApplicationUser LoggedUser = new ApplicationUser
        {
            Email = ControllersMocks.LoggedUser
        };

        public static ApplicationUser NotLoggedUser = new ApplicationUser
        {
            Email = ControllersMocks.NotloggedUser
        };

public static void AssignUserToProject(ApplicationUser user, TaskVlopper.Base.Model.Project project)
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var projLogic = container.Resolve<IProjectLogic>();
                projLogic.AssignUserToProject(project.ID, user.Email);
            }
        }

        public static void AssignUserToProjectTask(ApplicationUser user, TaskVlopper.Base.Model.Task task)
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var taskLogic = container.Resolve<ITaskLogic>();
                taskLogic.AssignUserToProjectTask(task.ProjectID, task.ID, user.Email);
            }
        }

        public static void AssignUserToMeeting(ApplicationUser user, TaskVlopper.Base.Model.Meeting meeting)
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var taskLogic = container.Resolve<IMeetingLogic>();
                taskLogic.AssignUserToMeeting(meeting.ID, user.Email);
            }
        }


        public static void AddTestProject(bool isUserLogged)
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var projLogic = container.Resolve<IProjectLogic>();
                projLogic.HandleProjectAdd(ModelsMocks.ProjectModelFirst,
                    isUserLogged ? ControllersMocks.LoggedUser : ControllersMocks.NotloggedUser);
            }
        }

        public static void AddTestTask(bool isUserLogged, TaskVlopper.Base.Model.Project project)
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var taskLogic = container.Resolve<ITaskLogic>();
                taskLogic.HandleTaskAdd(ModelsMocks.TaskModelFirst, project.ID,
                    isUserLogged ? ControllersMocks.LoggedUser : ControllersMocks.NotloggedUser);
            }
        }

        public static void AddTestWorklog(bool isUserLogged, TaskVlopper.Base.Model.Task task)
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var worklogLogic = container.Resolve<IWorklogLogic>();
                worklogLogic.HandleWorklogAdd(ModelsMocks.WorklogModelFirst, task.ProjectID, task.ID,
                    isUserLogged ? ControllersMocks.LoggedUser : ControllersMocks.NotloggedUser);
            }
        }

        public static void AddTestMeeting(bool isUserLogged,
            TaskVlopper.Base.Model.Meeting meeting,
            TaskVlopper.Base.Model.Task task)
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var meetingLogic = container.Resolve<IMeetingLogic>();
                meetingLogic.HandleMeetingAdd(ModelsMocks.MeetingModelFirst, task.ProjectID, task.ID,
                    isUserLogged ? ControllersMocks.LoggedUser : ControllersMocks.NotloggedUser);
            }
        }

        public static Meeting MeetingModelFirst = new Meeting()
        {
            ID = 1,
            DateAndTime = new DateTime(2015, 07, 07),
            Description = "wololo",
            Title = "SomeMeetingTitle"
        };

        public static Meeting MeetingModelSecond = new Meeting()
        {
            ID = 1,
            DateAndTime = new DateTime(2015, 08, 08),
            Description = "1",
            Title = "2"
        };

        public static Worklog WorklogModelFirst = new Worklog()
        {
            ID = 1,
            Date = new DateTime(2015, 07, 01),
            Description = "Worklog description",
            Hours = 15,
            TaskID = 1,
            UserID = ControllersMocks.LoggedUser
        };

        public static Worklog WorklogModelSecond = new Worklog()
        {
            ID = 2,
            Date = new DateTime(2015, 07, 02),
            Description = "Worklog description 2",
            Hours = 115,
            TaskID = 2,
            UserID = ControllersMocks.NotloggedUser

        };

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
