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

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class MeetingControllerTest
    {

        [TestMethod()]
        public void IndexWithParametersLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestMeeting(true, ModelsMocks.MeetingModelFirst, ModelsMocks.TaskModelFirst);
            // Arrange
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Index(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var data = (Models.MeetingsViewModel)action.Data;
            // Assert
            Assert.AreEqual(1, data.Meeting.Count());

            // Act
            action = controller.Index(ModelsMocks.ProjectModelFirst.ID) as JsonResult;
            data = (Models.MeetingsViewModel)action.Data;
            // Assert
            Assert.AreEqual(1, data.Meeting.Count());
        }

        [TestMethod()]
        public void IndexWithoutParametersLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestMeeting(true, ModelsMocks.MeetingModelFirst, ModelsMocks.TaskModelFirst);
            // Arrange
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Index() as JsonResult;
            var data = (Models.MeetingsViewModel)action.Data;
            // Assert
            Assert.AreEqual(1, data.Meeting.Count());
        }

        [TestMethod()]
        public void IndexWithParametersNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Index(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            // Assert
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod()]
        public void IndexWithoutParametersNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Index() as JsonResult;
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
            ModelsMocks.AddTestMeeting(true, ModelsMocks.MeetingModelFirst, ModelsMocks.TaskModelFirst);

            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Details(ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.MeetingModelFirst.ID) as JsonResult;
            var meeting = (TaskVlopper.Models.MeetingViewModel)action.Data;
            Assert.IsNotNull(meeting);

            action = controller.Details(ModelsMocks.ProjectModelFirst.ID,
                null,
                ModelsMocks.MeetingModelFirst.ID) as JsonResult;
            meeting = (TaskVlopper.Models.MeetingViewModel)action.Data;
            Assert.IsNotNull(meeting);
        }

        [TestMethod]
        public void DetailsNotLoggedUserTest()
        {
            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

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
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

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
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

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
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Create(ModelsMocks.MeetingModelFirst, ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repo = container.Resolve<IMeetingRepository>();
                var participantsRepo = container.Resolve<IMeetingParticipantsRepository>();
                var meeting = repo.GetMeetingByProjectIdAndTaskId(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID).Single();
                var meetingParticipants = participantsRepo.GetMeetingParticipantsByMeetingId(meeting.ID).SingleOrDefault();
                // Assert
                Assert.IsNotNull(meeting);
                Assert.IsNotNull(meetingParticipants);
            }
        }

        [TestMethod]
        public void CreatePostNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Create(ModelsMocks.MeetingModelFirst, ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID) as JsonResult;
            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void EditGetLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestMeeting(true, ModelsMocks.MeetingModelFirst, ModelsMocks.TaskModelFirst);

            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.MeetingModelFirst.ID) as JsonResult;

            var model = (TaskVlopper.Models.MeetingViewModel)action.Data;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void EditGetNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.MeetingModelFirst.ID) as JsonResult;

            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void EditPostLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestMeeting(true, ModelsMocks.MeetingModelFirst, ModelsMocks.TaskModelFirst);

            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.MeetingModelSecond, ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID, ModelsMocks.MeetingModelFirst.ID) as JsonResult;
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repo = container.Resolve<IMeetingRepository>();
                var meeting = repo.GetMeetingByIdWithoutTracking(ModelsMocks.MeetingModelFirst.ID);
                if (meeting.Description != "1") Assert.Fail();
                if (meeting.Title != "2") Assert.Fail();
                Assert.IsNotNull(meeting);
            }
        }

        [TestMethod]
        public void EditPostNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.MeetingModelSecond,
                ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.MeetingModelFirst.ID) as JsonResult;

            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void DeleteGetLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestMeeting(true, ModelsMocks.MeetingModelFirst, ModelsMocks.TaskModelFirst);

            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Delete(ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID, ModelsMocks.MeetingModelFirst.ID) as JsonResult;

            var meeting = (Models.MeetingViewModel)action.Data;
            Assert.IsNotNull(meeting);
        }

        [TestMethod]
        public void DeleteGetNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Delete(
                ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.MeetingModelFirst.ID) as JsonResult;

            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }

        [TestMethod]
        public void DeletePostLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            ModelsMocks.AddTestProject(true);
            ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
            ModelsMocks.AddTestMeeting(true, ModelsMocks.MeetingModelFirst, ModelsMocks.TaskModelFirst);

            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Delete(ModelsMocks.MeetingModelFirst, ModelsMocks.ProjectModelFirst.ID,
                ModelsMocks.TaskModelFirst.ID, ModelsMocks.MeetingModelFirst.ID) as JsonResult;
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repo = container.Resolve<IMeetingRepository>();
                var participantsRepo = container.Resolve<IMeetingParticipantsRepository>();
                bool passed = false;
                try
                {
                    var meeting = repo.GetMeetingByIdWithoutTracking(ModelsMocks.MeetingModelFirst.ID);
                }
                catch (Exception)
                {
                    passed = true;
                }
                finally
                {
                    if (!passed) Assert.Fail();
                }

                try
                {
                    var participants = participantsRepo.GetMeetingParticipantsByMeetingId(ModelsMocks.MeetingModelFirst.ID).SingleOrDefault();
                    if (participants != null) throw new Exception("Assert fails");
                }
                catch (Exception)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void DeletePostNotLoggedUserTest()
        {
            ModelsMocks.CleanUpBeforeTest();
            // Arrange 
            MeetingController controller = ControllersMocks.GetControllerAsNotLoggedUser<MeetingController>();

            // Act
            JsonResult action = controller.Edit(ModelsMocks.MeetingModelFirst,
                ModelsMocks.ProjectModelFirst.ID, ModelsMocks.TaskModelFirst.ID,
                ModelsMocks.MeetingModelFirst.ID) as JsonResult;

            var forbidden = (TaskVlopper.Models.JsonHttpViewModel)action.Data;
            Assert.AreEqual(403, forbidden.HttpCode);
        }
    }
}