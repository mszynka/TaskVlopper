using Microsoft.Practices.Unity;
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
    public class MeetingController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IMeetingRepository>();
                    var viewModel = new MeetingsViewModel(repository.GetAll().ToList());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // GET: Meeting/Details/5
        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IMeetingRepository>();
                    var viewModel = new MeetingViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // GET: Meeting/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Meeting/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IMeetingRepository>();
                        BaseSerializer<Meeting> serialize = new BaseSerializer<Meeting>();
                        var model = serialize.Serialize(Request.Form);

                        repository.Add(model);
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

        // GET: Meeting/Edit/5
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IMeetingRepository>();
                    var viewmodel = new MeetingViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return PartialView(viewmodel);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Meeting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using(IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IMeetingRepository>();
                        var model = repository.GetAll().ToList().Find(p => p.ID == id);

                        BaseSerializer<Meeting> serialize = new BaseSerializer<Meeting>();
                        serialize.Edit(model, Request.Form);

                        repository.Update(model);
                    }

                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Meeting successfully updated!"), JsonRequestBehavior.AllowGet);
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

        // GET: Meeting/Delete/5
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var repository = container.Resolve<IMeetingRepository>();
                    var viewmodel = new MeetingViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                    return View(viewmodel);
                }
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Meeting/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IMeetingRepository>();
                        var model = repository.GetAll().ToList().Find(p => p.ID == id);

                        repository.Remove(model);

                        return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Meeting successfully removed!"), JsonRequestBehavior.AllowGet);
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