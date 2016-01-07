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

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationSignInManager signInManager;
        private readonly IAuthenticationManager authenticationManager;
        [TestMethod()]
        public void UsersLoggedUserTest()
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(ControllersMocks.LoggedUser);
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            var response = new Mock<HttpResponseBase>();
            mock.SetupGet(p => p.HttpContext.Response).Returns(response.Object);
            ApplicationUser user = new ApplicationUser();
            UserStore<ApplicationUser> users = new UserStore<ApplicationUser>();
            users.Add
            userManager = new ApplicationUserManager(user);
            var controller = new AccountController(userManager, signInManager, authenticationManager);
            controller.ControllerContext = mock.Object;
            controller.Users();
            // Act
            JsonResult action = controller.Users() as JsonResult;


            // Assert
            Assert.IsNotNull(action);
        }

        [TestMethod()]
        public void UsersNotLoggedUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UsersByProjectLoggedUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UsersByProjectNotLoggedUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UsersByProjectTaskLoggedUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UsersByProjectTaskNotLoggedUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UsersByMeetingLoggedUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UsersByMeetingNotLoggedUserTest()
        {
            Assert.Fail();
        }
    }
}