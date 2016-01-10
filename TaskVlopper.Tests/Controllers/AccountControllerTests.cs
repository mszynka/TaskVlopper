using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskVlopper.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskVlopper.Tests.Mocks;
using Moq;
using System.Web;
using Microsoft.Practices.Unity;
using TaskVlopper.ServiceLocator;
using TaskVlopper.Identity;
using Microsoft.Owin.Security;
using TaskVlopper.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using TaskVlopper.Helpers;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {

        [TestMethod()]
        public void UsersLoggedUserTest()
        {
            // Arrange
            AccountController controller = ControllersMocks.CreateAccountControllerAs(ControllersMocks.LoggedUser, true);

            // Act
            JsonResult action = controller.Users() as JsonResult;
            int count = ((UsersViewModel)action.Data).Users.Count();

            // Assert
            Assert.AreEqual(3, count);
        }

        [TestMethod()]
        public void UsersNotLoggedUserTest()
        {
            // Arrange
            AccountController controller = ControllersMocks.CreateAccountControllerAs(ControllersMocks.NotloggedUser, false);

            // Act
            JsonResult action = controller.Users() as JsonResult;
            int code = ((JsonHttpViewModel)action.Data).HttpCode;

            // Assert
            Assert.AreEqual(403, code);
        }

       

        

        //[TestMethod()]
        //public void UsersByMeetingLoggedUserTest()
        //{
        //    ModelsMocks.CleanUpBeforeTest();
        //    // Arrange
        //    AccountController controller = ControllersMocks.CreateAccountControllerAs(ControllersMocks.LoggedUser, true);

        //    ModelsMocks.AddTestProject(true);
        //    ModelsMocks.AddTestTask(true, ModelsMocks.ProjectModelFirst);
        //    ModelsMocks.AddTestMeeting(true, ModelsMocks.MeetingModelFirst, ModelsMocks.TaskModelFirst);
        //    ModelsMocks.AssignUserToMeeting(ModelsMocks.FirstUser, ModelsMocks.MeetingModelFirst);

        //    // Act
        //    JsonResult action = controller.UsersByMeeting(ModelsMocks.MeetingModelFirst.ID) as JsonResult;
        //    int count = ((UsersViewModel)action.Data).Users.Count();

        //    // Assert
        //    Assert.AreEqual(2, count);
        //}

        //[TestMethod()]
        //public void UsersByMeetingNotLoggedUserTest()
        //{
        //    ModelsMocks.CleanUpBeforeTest();
        //    // Arrange
        //    AccountController controller = ControllersMocks.CreateAccountControllerAs(ControllersMocks.NotloggedUser, false);

        //    // Act
        //    JsonResult action = controller.UsersByMeeting(ModelsMocks.MeetingModelFirst.ID) as JsonResult;
        //    int code = ((JsonHttpViewModel)action.Data).HttpCode;

        //    // Assert
        //    Assert.AreEqual(403, code);
        //}
    }
}