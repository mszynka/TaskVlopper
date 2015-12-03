using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskVlopper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskVlopper.Helpers.Tests
{
    [TestClass()]
    public class JsonErrorHelpersTests
    {
        [TestMethod()]
        public void HttpErrorTest()
        {
            Assert.IsNotNull(JsonErrorHelpers.HttpError());
        }
    }
}