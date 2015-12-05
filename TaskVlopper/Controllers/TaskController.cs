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
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Controllers
{
    public class TaskController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<ITaskRepository>();
                    var viewModel = new TasksViewModel(repository.GetAll().ToList());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<ITaskRepository>();
                    var viewModel = new TaskViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    return View();
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<ITaskRepository>();

                        BaseSerializer<Task> serializer = new BaseSerializer<Task>();
                        var model = serializer.Serialize(Request.Form);


                        repository.Add(model);
                    }

                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Task successfully created!"), JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch(Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
            }
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<ITaskRepository>();
                    var viewmodel = new TaskViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return PartialView(viewmodel);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<ITaskRepository>();
                        var model = repository.GetAll().ToList().Find(p => p.ID == id);

                        BaseSerializer<Task> serializer = new BaseSerializer<Task>();
                        serializer.Edit(model, Request.Form);

                        repository.Update(model);
                    }

                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Task successfully updated!"), JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch(Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
            }
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<ITaskRepository>();
                    var viewmodel = new TaskViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return View(viewmodel);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<ITaskRepository>();
                        var model = repository.GetAll().ToList().Find(p => p.ID == id);

                        repository.Remove(model);

                        return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Task successfully removed!"), JsonRequestBehavior.AllowGet);
                    }
                }
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch(Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
            }
        }
    }
}