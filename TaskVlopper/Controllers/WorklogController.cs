﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Helpers;
using TaskVlopper.Models;
using TaskVlopper.Repository.Base;
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
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
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
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // GET: Worklog/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Worklog/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IWorklogRepository>();

                        BaseSerializer<Worklog> serializer = new BaseSerializer<Worklog>();
                        var model = serializer.Serialize(Request.Form);

                        repository.Add(model);
                    }

                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Worklog successfully created!"), JsonRequestBehavior.AllowGet);
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

        // GET: Worklog/Edit/5
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IWorklogRepository>();
                    var viewmodel = new WorklogViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return PartialView(viewmodel);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Worklog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IWorklogRepository>();
                        var model = repository.GetAll().ToList().Find(p => p.ID == id);

                        BaseSerializer<Worklog> serializer = new BaseSerializer<Worklog>();
                        serializer.Edit(model, Request.Form);

                        repository.Update(model);
                    }

                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Worklog successfully updated!"), JsonRequestBehavior.AllowGet);
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

        // GET: Worklog/Delete/5
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IWorklogRepository>();
                    var viewmodel = new WorklogViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return View(viewmodel);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Worklog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IWorklogRepository>();
                        var model = repository.GetAll().ToList().Find(p => p.ID == id);

                        repository.Remove(model);

                        return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Worklog successfully removed!"), JsonRequestBehavior.AllowGet);
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