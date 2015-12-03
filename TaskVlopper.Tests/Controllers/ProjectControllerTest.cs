using TaskVlopper.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web.Mvc;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class ProjectControllerTest
    {
        ProjectController CreateControllerAs(string userName, bool isUserLoggedIn)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(isUserLoggedIn);
            // other possibility of mocking user is:
            // mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new ProjectController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        [TestMethod()]
        public void IndexLoggedUserTest()
        {
            // Arrange
            ProjectController controller = CreateControllerAs("LoggedUser", isUserLoggedIn: true);

            // Act
            JsonResult action = controller.Index() as JsonResult;

            // Assert
            Assert.IsNotNull(action);
        }

        [TestMethod()]
        public void IndexNotLoggedUserTest()
        {
            // Arrange
            ProjectController controller = CreateControllerAs("NotLoggedUser", isUserLoggedIn: false);

            // Act
            // TODO: Use Moq to get ResponseCode

            // Assert
            // TODO: Assert ResponseCode is equal to 403
        }

        [TestMethod()]
        public void DetailsLoggedUserTest()
        {

        }

        [TestMethod()]
        public void DetailsNotLoggedUserTest()
        {

        }

        [TestMethod()]
        public void CreateGetLoggedUserTest()
        {

        }

        [TestMethod()]
        public void CreateGetNotLoggedUserTest()
        {

        }

        [TestMethod()]
        public void CreatePostLoggedUserTest()
        {

        }

        [TestMethod()]
        public void CreatePostNotLoggedUserTest()
        {

        }

        [TestMethod()]
        public void EditGetLoggedUserTest()
        {

        }

        [TestMethod()]
        public void EditGetNotLoggedUserTest()
        {

        }

        [TestMethod()]
        public void EditPostLoggedUserTest()
        {

        }

        [TestMethod()]
        public void EditPostNotLoggedUserTest()
        {

        }

        [TestMethod()]
        public void DeleteGetLoggedUserTest()
        {

        }

        [TestMethod()]
        public void DeleteGetNotLoggedUserTest()
        {

        }

        [TestMethod()]
        public void DeletePostLoggedUserTest()
        {

        }

        [TestMethod()]
        public void DeletePostNotLoggedUserTest()
        {

        }
    }
}