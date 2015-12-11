using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Repository;
using TaskVlopper.Models;
using TaskVlopper.ServiceLocator;

namespace TaskVlopper.Controllers
{
    public class TestController : Controller
    {
        // GET: Default
        public ActionResult Test()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var testLogic = container.Resolve<ITestLogic>();
                var testRepository = container.Resolve<ITestRepository>();

                var testViewModel = new TestsViewModel(testRepository.GetAll().ToList(), testLogic.GetAverageResult());

                return View("Test", testViewModel);
            }
        }
    }
}