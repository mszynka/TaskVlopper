﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskVlopper.Base.Logic;
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
        static IUnityContainer container = UnityConfig.GetConfiguredContainer();
        static IMeetingLogic logic = container.Resolve<IMeetingLogic>();

        [HttpGet]
        public ActionResult Index(int projectId, int? taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewModel = logic.GetAllMeetingsForCurrentUser(User.Identity.Name);
                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // GET: Meeting/Details/5
        public ActionResult Details(int projectId, int? taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewModel = new MeetingViewModel(logic.HandleMeetingGet(projectId, taskId, id));
                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // GET: Meeting/Create
        public ActionResult Create(int projectId, int? taskId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
            return View("Error", handler.handleError());
        }

        // POST: Meeting/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, int projectId, int? taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleMeetingAdd(collection, projectId, taskId, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Meeting successfully created!"), JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(int projectId, int? taskId, int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var viewmodel = logic.HandleMeetingGet(projectId, taskId, id);
                return PartialView(viewmodel);
            }
            ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
            return View("Error", handler.handleError());
        }

        // POST: Meeting/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection, int projectId, int? taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleMeetingEdit(collection, projectId, taskId, id);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Meeting successfully updated!"), JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // GET: Meeting/Delete/5
        public ActionResult Delete(int projectId, int? taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewmodel = logic.HandleMeetingGet(projectId, taskId, id);
                    return View(viewmodel);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // POST: Meeting/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection, int projectId, int? taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleMeetingDelete(projectId, taskId, id, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Meeting successfully removed!"), JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return View("Error", handler.handleError());
            }
        }
    }
}