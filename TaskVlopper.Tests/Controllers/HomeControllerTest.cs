using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskVlopper;
using TaskVlopper.Controllers;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace TaskVlopper.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        HomeController CreateControllerAs(string userName, bool isUserLoggedIn)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(isUserLoggedIn);
            // other possibility of mocking user is:
            // mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new HomeController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        [TestMethod]
        public void IndexLoggedUser()
        {
            // Arrange
            HomeController controller = CreateControllerAs("LoggedUser", isUserLoggedIn: true);

            // Act
            RedirectToRouteResult action = controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(action);
        }

        [TestMethod]
        public void IndexNotLoggedUser()
        {
            // Arrange
            HomeController controller = CreateControllerAs("NotLoggedUser", isUserLoggedIn: false);

            // Act
            ViewResult viewResult = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(viewResult);
        }
    }
}
