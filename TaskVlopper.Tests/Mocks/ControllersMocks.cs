using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskVlopper.Controllers;
using TaskVlopper.Identity;
using TaskVlopper.Models;

namespace TaskVlopper.Tests.Mocks
{
    public class ControllersMocks
    {
        public const string LoggedUser = "Logged";
        public const string NotloggedUser = "NotLogged";

        public static T GetControllerAsLoggedUser<T>(string userName = LoggedUser, bool isUserLoggedIn = true) where T : Controller, new()
        {
            return CreateControllerAs<T>(userName, isUserLoggedIn);
        }

        public static T GetControllerAsNotLoggedUser<T>(string userName = NotloggedUser, bool isUserLoggedIn = false) where T : Controller, new()
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

        public static AccountController CreateAccountControllerAs(string userName, bool isUserLoggedIn)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(isUserLoggedIn);
            var response = new Mock<HttpResponseBase>();
            mock.SetupGet(p => p.HttpContext.Response).Returns(response.Object);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore.Object);
            var authenticationManager = new Mock<IAuthenticationManager>();
            var signInManager = new Mock<ApplicationSignInManager>(userManager.Object, authenticationManager.Object);

            userManager.Setup(x => x.Users)
                .Returns(ModelsMocks.GetApplicationUserList().AsQueryable());

            var accountController = new AccountController(
                userManager.Object, signInManager.Object, authenticationManager.Object);

            accountController.ControllerContext = mock.Object;
            return accountController;
        }
    }
}
