using TaskVlopper.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using TaskVlopper.Tests.Mocks;
using Microsoft.Practices.Unity;
using TaskVlopper.ServiceLocator;
using TaskVlopper.Base.Repository;
using TaskVlopper.Base.Logic;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class TaskControllerTest
    {
        public static void CleanUpBeforeTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var projRepo = container.Resolve<IProjectRepository>();
                var projAssignmentRepo = container.Resolve<IUserProjectAssignmentRepository>();

                var taskRepo = container.Resolve<ITaskRepository>();
                var taskAssignmentRepo = container.Resolve<IUserTaskAssignmentRepository>();

                projRepo.RemoveAll();
                projAssignmentRepo.RemoveAll();
                taskRepo.RemoveAll();
                taskAssignmentRepo.RemoveAll();
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



        [TestMethod()]
        public void IndexLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(true);

            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Index(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            var tasks = (TaskVlopper.Models.TasksViewModel)action.Data;
            // Assert
            Assert.IsNotNull(tasks);
        }

        [TestMethod()]
        public void IndexNotLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(false);

            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsNotLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Index(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void CreateGetLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(true);

            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Create(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            var allowed = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            // Assert
            Assert.AreEqual(200, allowed.HttpCode);
        }

        [TestMethod]
        public void CreateGetNotLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(false);

            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsNotLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Create(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void CreatePostLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(true);

            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Create(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst) as JsonResult;
            // Assert
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var taskRepo = container.Resolve<ITaskRepository>();
                var taskAssignmentRepo = container.Resolve<IUserTaskAssignmentRepository>();
                var task = taskRepo.GetTaskByIdWithoutTracking(ModelsMocks.TaskModelFirst.ID);
                var taskAssignment = taskAssignmentRepo.GetTaskAssignmentByTaskId(ModelsMocks.TaskModelFirst.ID).Single();

                bool passed = true;
                if (taskAssignment.ProjectID != ModelsMocks.ProjectModelFirst.ID) passed = false;
                if (taskAssignment.TaskID != ModelsMocks.TaskModelFirst.ID) passed = false;
                Assert.AreEqual(true, passed);
            }


        }

        [TestMethod]
        public void CreatePostNotLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(false);

            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsNotLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Create(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);

        }

        [TestMethod]
        public void DetailsLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(true);
            AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Details(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var task = (TaskVlopper.Models.TaskViewModel)action.Data;
            // Assert
            Assert.IsNotNull(task);
        }

        [TestMethod]
        public void DetailsNotLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(false);
            AddTestTask(false, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsNotLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Details(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }


        [TestMethod]
        public void EditGetLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(true);
            AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var task = (TaskVlopper.Base.Model.Task)action.Data;
            // Assert

            Assert.IsNotNull(task);
        }

        [TestMethod]
        public void EditGetNotLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(false);
            AddTestTask(false, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsNotLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void EditPostLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(true);
            AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID, ModelsMocks.TaskModelSecond) as JsonResult;
            // Assert
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var taskRepo = container.Resolve<ITaskRepository>();
                var taskAssignmentRepo = container.Resolve<IUserTaskAssignmentRepository>();
                var task = taskRepo.GetTaskByIdWithoutTracking(ModelsMocks.TaskModelFirst.ID);
                var taskAssignment = taskAssignmentRepo.GetTaskAssignmentByTaskId(ModelsMocks.TaskModelFirst.ID).Single();

                bool passed = true;
                if (taskAssignment.ProjectID != ModelsMocks.ProjectModelFirst.ID && task.Name != "Task2") passed = false;
                if (taskAssignment.TaskID != ModelsMocks.TaskModelFirst.ID) passed = false;
                Assert.AreEqual(true, passed);
            }
        }

        [TestMethod]
        public void EditPostNotLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(false);
            AddTestTask(false, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsNotLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID, ModelsMocks.TaskModelSecond) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            // Assert
            
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void DeleteGetLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(true);
            AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var task = (TaskVlopper.Base.Model.Task)action.Data;

            // Assert
            Assert.IsNotNull(task);
        }

        [TestMethod]
        public void DeleteGetNotLoggedUserTest()
        {
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsNotLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            // Assert

            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void DeletePostLoggedUserTest()
        {
            CleanUpBeforeTest();
            AddTestProject(true);
            AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID, ModelsMocks.TaskModelFirst) as JsonResult;
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var taskRepo = container.Resolve<ITaskRepository>();
                var taskAssignmentRepo = container.Resolve<IUserTaskAssignmentRepository>();

                // Assert
                Assert.AreEqual(0, taskRepo.GetAll().Count() + taskAssignmentRepo.GetAll().Count());
            }


                
        }

        [TestMethod]
        public void DeletePostNotLoggedUserTest()
        {
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsNotLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID, ModelsMocks.TaskModelSecond) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            // Assert

            Assert.AreEqual(403, forbidden.HttpCode);
        }
    }
}