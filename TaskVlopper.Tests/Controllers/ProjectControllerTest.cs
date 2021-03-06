﻿using TaskVlopper.Controllers;
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

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class ProjectControllerTest
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

        [TestMethod]
        public void DeletePostLoggedUserTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<IProjectRepository>();
                repository.RemoveAll();



                // Arrange
                ProjectController controller = ControllersMocks.GetControllerAsLoggedUser<ProjectController>();

                JsonResult actionCreate = controller.Create(ModelsMocks.ProjectModelFirst) as JsonResult;
                // Act
                JsonResult actionDelete = controller.Delete(repository.GetAll().First().ID, ModelsMocks.Form) as JsonResult;

                // Assert
                Assert.AreEqual(0, repository.GetAll().Count());
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
    }
}