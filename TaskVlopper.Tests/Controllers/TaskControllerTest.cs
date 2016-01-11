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
using TaskVlopper.Models;
using TaskVlopper.Tests;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class TaskControllerTest : TestsBase
    {

        [TestMethod()]
        public void IndexLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);

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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(false);

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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);

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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(false);

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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);

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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(false);

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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(false);
            ModelsMocks.AddTestTask(false, ModelsMocks.ProjectModelFirst);
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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var task = (Models.TaskViewModel)action.Data;
            // Assert

            Assert.IsNotNull(task);
        }

        [TestMethod]
        public void EditGetNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(false);
            ModelsMocks.AddTestTask(false, ModelsMocks.ProjectModelFirst);
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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(false);
            ModelsMocks.AddTestTask(false, ModelsMocks.ProjectModelFirst);
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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>();

            // Act 
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var task = (Models.TaskViewModel)action.Data;

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
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
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

        [TestMethod()]
        public void UsersGetLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>(ControllersMocks.LoggedUser, true);

            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AssignUserToProjectTask(ModelsMocks.FirstUser, ModelsMocks.TaskModelFirst);

            //ModelsMocks.RegisterUser();
            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            int count = ((UsersViewModel)action.Data).Users.Count();

            // Assert
            Assert.AreEqual(2, count);
        }

        [TestMethod()]
        public void UsersGetNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>(ControllersMocks.NotloggedUser, false);

            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            int code = ((JsonHttpViewModel)action.Data).HttpCode;

            // Assert
            Assert.AreEqual(403, code);
        }

        [TestMethod()]
        public void UsersPostLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>(ControllersMocks.LoggedUser, true);

            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AssignUserToProject(ModelsMocks.FirstUser, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AssignUserToProjectTask(ModelsMocks.FirstUser, ModelsMocks.TaskModelFirst);

            ModelsMocks.RegisterUser();
            // Act
            JsonResult action = controller.Users(ModelsMocks.TaskModelFirst.ID, ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.RegisterTestUser.Email) as JsonResult;
            var data = ((JsonHttpViewModel)action.Data);

            // Assert
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var projectRepo = container.Resolve<IUserTaskAssignmentRepository>();
                var users = projectRepo.GetAllUsersIDsForGivenTaskProject(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID);
                var ourUser = users.Single(x => x == ModelsMocks.RegisterTestUser.Email);
                Assert.AreEqual(3, projectRepo.GetAll().Count());
            }

            Assert.AreEqual(200, data.HttpCode);
            Assert.AreEqual(1, ModelsMocks.CountReigsteredUsers());
        }

        [TestMethod()]
        public void UsersPostLoggedUserTest_wrongUserIdInjection()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            TaskController controller = ControllersMocks.GetControllerAsLoggedUser<TaskController>(ControllersMocks.LoggedUser, true);

            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                "dsadsa123@dcxzczx@sdaas123.pl") as JsonResult;
            var data = ((JsonHttpViewModel)action.Data);

            // Assert
            Assert.AreEqual(500, data.HttpCode);
        }

        [TestMethod()]
        public void UsersPostNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            TaskController controller =
                ControllersMocks.GetControllerAsLoggedUser<TaskController>(ControllersMocks.NotloggedUser, false);

            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                "anything") as JsonResult;
            int code = ((JsonHttpViewModel)action.Data).HttpCode;

            // Assert
            Assert.AreEqual(403, code);
        }

    }
}