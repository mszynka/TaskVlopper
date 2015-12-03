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
using TaskVlopper.Tests.Mocks;

namespace TaskVlopper.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void IndexLoggedUser()
        {
            // Arrange
            HomeController controller = ControllersMocks.GetControllerAsLoggedUser<HomeController>();

            // Act
            RedirectToRouteResult action = controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(action);
        }

        [TestMethod]
        public void IndexNotLoggedUser()
        {
            // Arrange
            HomeController controller = ControllersMocks.GetControllerAsNotLoggedUser<HomeController>();

            // Act
            ViewResult action = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(action);
        }
    }
}
