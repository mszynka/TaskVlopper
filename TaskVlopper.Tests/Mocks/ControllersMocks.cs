using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskVlopper.Controllers;

namespace TaskVlopper.Tests.Mocks
{
    public class ControllersMocks
    {
        public static T GetControllerAsLoggedUser<T>(string userName = "Logged", bool isUserLoggedIn = true) where T : Controller, new()
        {
            return CreateControllerAs<T>(userName, isUserLoggedIn);
        }

        public static T GetControllerAsNotLoggedUser<T>(string userName = "NotLogged", bool isUserLoggedIn = false) where T : Controller, new()
        {
            return CreateControllerAs<T>(userName, isUserLoggedIn);
        }

        private static T CreateControllerAs<T>(string userName, bool isUserLoggedIn) where T : Controller, new()
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(isUserLoggedIn);

            var response = new Mock<HttpResponseBase>();
            mock.SetupGet(p => p.HttpContext.Response).Returns(response.Object);
            // other possibility of mocking user is:
            // mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            
            var controller = new T();
            controller.ControllerContext = mock.Object;

            return controller;
        }
    }
}
