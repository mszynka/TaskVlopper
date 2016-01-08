using Moq;
using System.Web.Mvc;
using TaskVlopper.Tests.Mocks;
using Microsoft.Practices.Unity;
using TaskVlopper.ServiceLocator;
using TaskVlopper.Base.Repository;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class DashboardControllerTests
    {

        [TestMethod]
        public void IndexLoggedUserAngularTest()
        {
            // Arrange
            DashboardController controller = ControllersMocks.GetControllerAsLoggedUser<DashboardController>();

            // Act
            ViewResult action = controller.Index() as ViewResult;


            // Assert
            Assert.IsTrue(action.ViewBag.HasAngular);
        }

        [TestMethod]
        public void IndexNotLoggedUserAngularTest()
        {
            // Arrange
            DashboardController controller = ControllersMocks.GetControllerAsNotLoggedUser<DashboardController>();

            // Act
            ViewResult action = controller.Index() as ViewResult;


            // Assert
            Assert.IsFalse(action.ViewBag.HasAngular);
        }
    }
}