using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskVlopper.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("Private");
            }

            return View("Public");
        }
    }
}