using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskVlopper.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web.Mvc;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class DashboardControllerTests
    {
        DashboardController CreateControllerAs(string userName, bool isUserLoggedIn)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(isUserLoggedIn);
            // other possibility of mocking user is:
            // mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new DashboardController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        [TestMethod()]
        public void IndexTest()
        {

        }
    }
}