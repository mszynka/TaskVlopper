﻿using Microsoft.Practices.Unity;
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
    public class ProjectController : Controller
    {
        public IUnityContainer container = UnityConfig.GetConfiguredContainer();

        // GET: Project
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    var model = logic.GetAllProjectsForCurrentUser(User.Identity.Name);
                    var viewModel = new ProjectsViewModel(model.ToList());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return Json(handler.handleError(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return Json(handler.handleError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    var viewModel = new ProjectViewModel(logic.HandleProjectGet(id));
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

        // GET: Project/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
            return View("Error", handler.handleError());
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(Project collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    logic.HandleProjectAdd(collection, User.Identity.Name);

                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Project successfully created!"), JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return Json(handler.handleError(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return Json(handler.handleError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    var viewmodel = new ProjectViewModel(logic.HandleProjectGet(id));
                    return PartialView(viewmodel);
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

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Project collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    logic.HandleProjectEdit(collection, id);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Project successfully updated!"), JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return Json(handler.handleError(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return Json(handler.handleError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    var viewmodel = new ProjectViewModel(logic.HandleProjectGet(id));
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

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    logic.HandleProjectDelete(id, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Project successfully removed!"), JsonRequestBehavior.AllowGet);
                }
                ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
                return Json(handler.handleError(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionHandler handler = new ExceptionHandler(ex);
                return Json(handler.handleError(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
