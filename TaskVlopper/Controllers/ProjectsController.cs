using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskVlopper.Helpers;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Repository;
using TaskVlopper.Models;
using TaskVlopper.ServiceLocator;

namespace TaskVlopper.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using(IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IProjectsRepository>();
                    var viewModel = new ProjectViewModel(repository.GetAll().ToList());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = 403;
            return View("Error");
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            return Json(HttpNotFound());
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return Json(HttpNotFound());
        }

        // POST: Projects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return Json(HttpNotFound());
            }
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {
            return Json(HttpNotFound());
        }

        // POST: Projects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return Json(HttpNotFound());
            }
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
            return Json(HttpNotFound());
        }

        // POST: Projects/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return Json(HttpNotFound());
            }
        }
    }
}
