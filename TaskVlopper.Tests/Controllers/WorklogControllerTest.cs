using TaskVlopper.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web.Mvc;
using TaskVlopper.Tests.Mocks;
using System.Web;
using System.Net;

namespace TaskVlopper.Controllers.Tests
{
    [TestClass()]
    public class WorklogControllerTest
    {

        [TestMethod()]
        public void IndexLoggedUserTest()
        {
            //ToDo
            //// Arrange 
            //WorklogController controller = ControllersMocks.GetControllerAsLoggedUser<WorklogController>();

            //// Act
            //JsonResult action = controller.Index() as JsonResult;

            //// Assert
            //Assert.IsNotNull(action);
        }

        [TestMethod()]
        public void IndexNotLoggedUserTest()
        {
            //ToDo
            //// Arrange
            //WorklogController controller = ControllersMocks.GetControllerAsNotLoggedUser<WorklogController>();

            //// Act
            //JsonResult action = controller.Index() as JsonResult;

            //// Assert
            //Assert.IsNull(action);           
        }

        [TestMethod, Ignore]
        public void DetailsLoggedUserTest()
        {
            
        }

        [TestMethod, Ignore]
        public void DetailsNotLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void CreateGetLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void CreateGetNotLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void CreatePostLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void CreatePostNotLoggedUserTest()
        {

        }

        [TestMethod()]
        public void EditGetLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void EditGetNotLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void EditPostLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void EditPostNotLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void DeleteGetLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void DeleteGetNotLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void DeletePostLoggedUserTest()
        {

        }

        [TestMethod, Ignore]
        public void DeletePostNotLoggedUserTest()
        {

        }
    }
}