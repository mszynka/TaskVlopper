using TaskVlopper.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web.Mvc;
using TaskVlopper.Tests.Mocks;
using Microsoft.Practices.Unity;
using TaskVlopper.ServiceLocator;
using TaskVlopper.Base.Repository;
using System.Data.Entity;
using TaskVlopper.Models;
using TaskVlopper.Helpers;
using TaskVlopper.Repository;
using TaskVlopper.Base.Model;
using TaskVlopper.Tests;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class ProjectControllerTest : TestsBase
    {

        [TestMethod()]
        public void IndexLoggedUserTest()
        {
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Index() as JsonResult;


            // Assert
            Assert.IsNotNull(action);
        }

        [TestMethod()]
        public void IndexNotLoggedUserTest()
        {
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsNotLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Index() as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void DetailsLoggedUserTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                repository.Add(ModelsMocks.ProjectModelFirst);
            }


            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Details(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            var project = (TaskVlopper.Models.ProjectViewModel)action.Data;
            // Assert
            Assert.IsNotNull(project);
        }

        [TestMethod]
        public void DetailsNotLoggedUserTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                repository.RemoveAll();

                repository.Add(ModelsMocks.ProjectModelFirst);
            }

            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsNotLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Details(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void CreateGetLoggedUserTest()
        {
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Create() as JsonResult;

            // Assert
            Assert.IsNotNull(action);
        }

        [TestMethod]
        public void CreateGetNotLoggedUserTest()
        {
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsNotLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Create() as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void CreatePostLoggedUserTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                repository.RemoveAll();

                // Arrange
                ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();


                // Act
                JsonResult action = controller.Create(ModelsMocks.ProjectModelFirst) as JsonResult;

                // Assert
                Assert.AreEqual(repository.GetAll().
                    Where(x => x.Deadline == ModelsMocks.ProjectModelFirst.Deadline
                    && x.Description == ModelsMocks.ProjectModelFirst.Description
                    && x.EstimatedTimeInHours == ModelsMocks.ProjectModelFirst.EstimatedTimeInHours
                    && x.Name == ModelsMocks.ProjectModelFirst.Name
                    && x.StartDate == ModelsMocks.ProjectModelFirst.StartDate)
                    .Count(), 1);
            }
        }

        [TestMethod]
        public void CreatePostNotLoggedUserTest()
        {
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsNotLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Create(ModelsMocks.ProjectModelFirst) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void EditGetLoggedUserTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                repository.RemoveAll();
                repository.Add(ModelsMocks.ProjectModelSecond);
            }
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelSecond.ID) as JsonResult;
            var project = (TaskVlopper.Models.ProjectViewModel)action.Data;

            // Assert
            Assert.IsNotNull(project);
        }

        [TestMethod]
        public void EditGetNotLoggedUserTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                repository.RemoveAll();
                repository.Add(ModelsMocks.ProjectModelSecond);
            }
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsNotLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelSecond.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void EditPostLoggedUserTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                repository.RemoveAll();
                repository.Add(ModelsMocks.ProjectModelSecond);

                // Arrange
                ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();

                // Act
                JsonResult action = controller.Edit(ModelsMocks.ProjectModelSecond.ID, ModelsMocks.ProjectModelFirst) as JsonResult;

                // Assert
                Assert.AreEqual(ModelsMocks.ProjectModelFirst.Name,
                    repository.GetProjectByIdWithoutTracking(ModelsMocks.ProjectModelSecond.ID).Name);

            }
        }

        [TestMethod]
        public void EditPostNotLoggedUserTest()
        {
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsNotLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelSecond.ID, ModelsMocks.ProjectModelSecond) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);

        }

        [TestMethod]
        public void DeleteGetLoggedUserTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                repository.RemoveAll();
                repository.Add(ModelsMocks.ProjectModelSecond);

                // Arrange
                ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();

                // Act
                JsonResult action = controller.Delete(ModelsMocks.ProjectModelSecond.ID) as JsonResult;
                var project = (Models.ProjectViewModel)action.Data;
                // Assert
                Assert.IsNotNull(project);
            }
        }

        [TestMethod]
        public void DeleteGetNotLoggedUserTest()
        {
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsNotLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelSecond.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }


        //Its fucked up
        [TestMethod]
        public void DeletePostLoggedUserTest()
         {
            ModelsMocks.CleanUpBeforeTest();
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                var assignmentRepo = container.Resolve<IUserProjectAssignmentRepository>();
                repository.RemoveAll();

                // Arrange
                ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();

                JsonResult actionCreate = controller.Create(ModelsMocks.ProjectModelFirst) as JsonResult;

                UserProjectAssignment test = new UserProjectAssignment();
                test.ProjectID = ModelsMocks.ProjectModelFirst.ID;
                test.UserID = "w@w.pl";
                assignmentRepo.Add(test);
                // Act
                JsonResult actionDelete = controller.Delete(repository.GetAll().First().ID, ModelsMocks.Form) as JsonResult;

                // Assert
                Assert.AreEqual(0, repository.GetAll().Count());
                Assert.AreEqual(0, assignmentRepo.GetAll().Count());
            }
        }

        [TestMethod]
        public void DeletePostNotLoggedUserTest()
        {
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsNotLoggedUser<ProjectController>();

            // Act
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelSecond.ID, ModelsMocks.Form) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;

            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void CleanUpTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var projRepository = container.Resolve<IProjectRepository>();
                var projAssignmentRepo = container.Resolve<IUserProjectAssignmentRepository>();
                projRepository.RemoveAll();
                projAssignmentRepo.RemoveAll();

                Assert.AreEqual(0, projRepository.GetAll().Count() + projAssignmentRepo.GetAll().Count());

            }
        }

        [TestMethod()]
        public void UsersGetLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>(ControllersMocks.LoggedUser, true);

            ModelsMocks.AddTestProject(true);
            ModelsMocks.AssignUserToProject(ModelsMocks.FirstUser, ModelsMocks.ProjectModelFirst);

            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            int count = ((UsersViewModel)action.Data).Users.Count();

            // Assert
            Assert.AreEqual(2, count);
        }

        [TestMethod()]
        public void UsersGetNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            ProjectController controller =
                ControllersMocks.GetControllerAsLoggedUser<ProjectController>(ControllersMocks.NotloggedUser, false);

            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            int code = ((JsonHttpViewModel)action.Data).HttpCode;

            // Assert
            Assert.AreEqual(403, code);
        }

        [TestMethod()]
        public void UsersPostLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>(ControllersMocks.LoggedUser, true);

            ModelsMocks.AddTestProject(true);
            ModelsMocks.AssignUserToProject(ModelsMocks.FirstUser, ModelsMocks.ProjectModelFirst);

            ModelsMocks.RegisterUser();
            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.RegisterTestUser.Email) as JsonResult;
            var data = ((JsonHttpViewModel)action.Data);

            // Assert
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var projectRepo = container.Resolve<IUserProjectAssignmentRepository>();
                var users = projectRepo.GetAllUsersIDsForGivenProject(ModelsMocks.ProjectModelFirst.ID);
                var ourUser = users.Single(x => x == ModelsMocks.RegisterTestUser.Email);
            }

            Assert.AreEqual(200, data.HttpCode);
            Assert.AreEqual(1, ModelsMocks.CountReigsteredUsers());
        }

        [TestMethod()]
        public void UsersPostLoggedUserTest_wrongUserIdInjection()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange
            ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>(ControllersMocks.LoggedUser, true);

            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID,
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
            ProjectController controller =
                ControllersMocks.GetControllerAsLoggedUser<ProjectController>(ControllersMocks.NotloggedUser, false);

            // Act
            JsonResult action = controller.Users(ModelsMocks.ProjectModelFirst.ID, "anything") as JsonResult;
            int code = ((JsonHttpViewModel)action.Data).HttpCode;

            // Assert
            Assert.AreEqual(403, code);
        }

        [TestMethod(), Ignore]
        public void GetAllWithStatsLoggedUserTest()
        {

        }

        [TestMethod(), Ignore]
        public void GetAllWithStatsNotLoggedUserTest()
        {

        }

        [TestMethod(), Ignore]
        public void DetailsWithStatsLoggedUserTest()
        {

        }

        [TestMethod(), Ignore]
        public void DetailsWithStatsNotLoggedUserTest()
        {

        }

        [TestMethod(), Ignore]
        public void UnbindUserLoggedUserTest()
        {

        }

        [TestMethod(), Ignore]
        public void UnbindUserNotLoggedUserTest()
        {

        }
    }

}