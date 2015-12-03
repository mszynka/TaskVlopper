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
using TaskVlopper.Base.Model;

namespace TaskVlopper.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IProjectRepository>();
                    var viewModel = new ProjectsViewModel(repository.GetAll().ToList());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IProjectRepository>();
                    var viewModel = new ProjectViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IProjectRepository>();

                        Project p = new Project();
                        p.Name = Request.Form["Name"];
                        p.Description = Request.Form["Description"];
                        if (!string.IsNullOrWhiteSpace(Request.Form["StartDate"]))
                            p.StartDate = DateTime.Parse(Request.Form["StartDate"]);
                        if (!string.IsNullOrWhiteSpace(Request.Form["Deadline"]))
                            p.Deadline = DateTime.Parse(Request.Form["Deadline"]);
                        p.EstimatedTimeInHours = int.Parse(Request.Form["EstimatedTimeInHours"]);

                        repository.Add(p);
                    }

                    return Json(JsonHelpers.HttpMessage(HttpCode.Created, "Project successfully created!"), JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = (int)HttpCode.Forbidden;
                return View("Error");
            }
            catch
            {
                Response.StatusCode = (int)HttpCode.InternalServerError;
                return View("Error");
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IProjectRepository>();
                    var model = repository.GetAll().ToList().Find(p => p.ID == id);

                    return PartialView("Edit", model);
                }
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");

        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IProjectRepository>();
                        var model = repository.GetAll().ToList().Find(p => p.ID == id);

                        if (Request.Form["Name"] != model.Name)
                            model.Name = Request.Form["Name"];
                        if (Request.Form["Description"] != model.Description)
                            model.Description = Request.Form["Description"];
                        if (!string.IsNullOrWhiteSpace(Request.Form["StartDate"]))
                        {
                            if (DateTime.Parse(Request.Form["StartDate"]) != model.StartDate)
                                model.StartDate = DateTime.Parse(Request.Form["StartDate"]);
                        }
                        if (!string.IsNullOrWhiteSpace(Request.Form["Deadline"]))
                        {
                            if (DateTime.Parse(Request.Form["Deadline"]) != model.Deadline)
                                model.Deadline = DateTime.Parse(Request.Form["Deadline"]);
                        }
                        if (int.Parse(Request.Form["EstimatedTimeInHours"]) != model.EstimatedTimeInHours)
                            model.EstimatedTimeInHours = int.Parse(Request.Form["EstimatedTimeInHours"]);

                        repository.Update(model);
                    }

                    return Json(JsonHelpers.HttpMessage(HttpCode.Accepted, "Project successfully updated!"), JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = (int)HttpCode.Forbidden;
                return View("Error");
            }
            catch
            {
                Response.StatusCode = (int)HttpCode.InternalServerError;
                return View("Error");
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IProjectRepository>();
                    var model = repository.GetAll().ToList().Find(p => p.ID == id);

                    return View(model);
                }
            }
            Response.StatusCode = (int)HttpCode.Forbidden;
            return View("Error");
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IProjectRepository>();
                        var model = repository.GetAll().ToList().Find(p => p.ID == id);

                        repository.Remove(model);

                        return Json(JsonHelpers.HttpMessage(HttpCode.OK, "Project successfully removed!"), JsonRequestBehavior.AllowGet);
                    }
                }
                Response.StatusCode = (int)HttpCode.Forbidden;
                return View("Error");
            }
            catch
            {
                Response.StatusCode = (int)HttpCode.InternalServerError;
                return View("Error");
            }
        }
    }
}
