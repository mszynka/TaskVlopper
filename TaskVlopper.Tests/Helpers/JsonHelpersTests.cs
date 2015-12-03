using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskVlopper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskVlopper.Helpers.Tests
{
    [TestClass()]
    public class JsonHelpersTests
    {
        [TestMethod()]
        public void HttpErrorTest()
        {
            Assert.IsNotNull(JsonHelpers.HttpMessage());
        }
    }
}