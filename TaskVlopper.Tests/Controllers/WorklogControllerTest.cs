using TaskVlopper.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web.Mvc;
using TaskVlopper.Tests.Mocks;
using System.Web;
using System.Net;
using Microsoft.Practices.Unity;
using TaskVlopper.ServiceLocator;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Repository;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class WorklogControllerTest
    {

        [TestMethod()]
        public void IndexLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();

            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Index(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var worklogs = (Models.WorklogsViewModel)action.Data;
            // Assert
            Assert.IsNotNull(worklogs);
        }

        [TestMethod()]
        public void IndexNotLoggedUserTest()
        {
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Index(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
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
            ModelsMocks.AddTestWorklog(true, ModelsMocks.TaskModelFirst);

            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Details(ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID, ModelsMocks.WorklogModelFirst.ID) as JsonResult;
            var worklog = (TaskVlopper.Models.WorklogViewModel)action.Data;
            Assert.IsNotNull(worklog);
        }

        [TestMethod]
        public void DetailsNotLoggedUserTest()
        {
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Details(ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.WorklogModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void CreateGetLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Create(1, 1) as JsonResult;
            var ok = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(200, ok.HttpCode);
        }

        [TestMethod]
        public void CreateGetNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Create(1, 1) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void CreatePostLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();

            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Create(ModelsMocks.WorklogModelFirst, ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repo = container.Resolve<IWorklogRepository>();
                var worklog = repo.GetWorklogByIdWithoutTracking(ModelsMocks.WorklogModelFirst.ID);
                // Assert
                Assert.IsNotNull(worklog);
            }
        }

        [TestMethod]
        public void CreatePostNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Create(ModelsMocks.WorklogModelFirst, ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod()]
        public void EditGetLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestWorklog(true, ModelsMocks.TaskModelFirst);

            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.WorklogModelSecond, ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID, ModelsMocks.WorklogModelFirst.ID) as JsonResult;

            var ok = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(202, ok.HttpCode);
        }

        [TestMethod]
        public void EditGetNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.WorklogModelFirst.ID) as JsonResult;

            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void EditPostLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestWorklog(true, ModelsMocks.TaskModelFirst);

            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.WorklogModelSecond, ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID, ModelsMocks.WorklogModelFirst.ID) as JsonResult;
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repo = container.Resolve<IWorklogRepository>();
                var worklog = repo.GetWorklogByIdWithoutTracking(ModelsMocks.WorklogModelFirst.ID);
                if (worklog.Hours != 115) Assert.Fail();
                Assert.IsNotNull(worklog);
            }
        }

        [TestMethod]
        public void EditPostNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.WorklogModelFirst,
                ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.WorklogModelFirst.ID) as JsonResult;

            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void DeleteGetLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestWorklog(true, ModelsMocks.TaskModelFirst);

            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID, ModelsMocks.WorklogModelFirst.ID) as JsonResult;

            var worklog = (Models.WorklogViewModel)action.Data;
            Assert.IsNotNull(worklog);
        }

        [TestMethod]
        public void DeleteGetNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Delete(
                ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.WorklogModelFirst.ID) as JsonResult;

            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void DeletePostLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestWorklog(true, ModelsMocks.TaskModelFirst);

            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Delete(ModelsMocks.WorklogModelSecond, ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID, ModelsMocks.WorklogModelFirst.ID) as JsonResult;
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repo = container.Resolve<IWorklogRepository>();
                try {
                    var worklog = repo.GetWorklogByIdWithoutTracking(ModelsMocks.WorklogModelFirst.ID);
                    Assert.Fail();
                }
                #pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
                #pragma warning restore CS0168 // Variable is declared but never used
                {
                    //Passed
                }
            }
        }

        [TestMethod]
        public void DeletePostNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.WorklogModelFirst,
                ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.WorklogModelFirst.ID) as JsonResult;

            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }
    }
}