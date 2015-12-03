using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskVlopper.Base.Repository;
using TaskVlopper.Helpers;
using TaskVlopper.Models;
using TaskVlopper.ServiceLocator;

namespace TaskVlopper.Controllers
{
    public class WorklogController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IWorklogRepository>();
                    var viewModel = new WorklogsViewModel(repository.GetAll().ToList());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");
        }

        // GET: Worklog/Details/5
        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IWorklogRepository>();
                    var viewModel = new WorklogViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");
        }

        // GET: Worklog/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("ModelEditor");
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");
        }

        // POST: Worklog/Create
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

        // GET: Worklog/Edit/5
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IWorklogRepository>();
                    var viewModel = new WorklogViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return PartialView("ModelEditor", viewModel);
                }
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");
        }

        // POST: Worklog/Edit/5
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

        // GET: Worklog/Delete/5
        public ActionResult Delete(int id)
        {
            return Details(id);
        }

        // POST: Worklog/Delete/5
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