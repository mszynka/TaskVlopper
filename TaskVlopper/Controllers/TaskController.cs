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
        static IUnityContainer container = UnityConfig.GetConfiguredContainer();
        static ITaskLogic logic = container.Resolve<ITaskLogic>();

        [HttpGet]
        public ActionResult Index(int projectId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var model = logic.GetAllTasksForGivenProjectAndCurrentUser(projectId, User.Identity.Name);
                    var viewModel = new TasksViewModel(model.ToList());

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

        // GET: Task/Details/5
        public ActionResult Details(int projectId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewModel = new TaskViewModel(logic.HandleTaskGet(projectId, id));
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

        // GET: Task/Create
        public ActionResult Create(int projectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
            return View("Error", handler.handleError());
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(int projectId, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleTaskAdd(collection, projectId, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Task successfully created!"), JsonRequestBehavior.AllowGet);
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

        // GET: Task/Edit/5
        public ActionResult Edit(int projectId, int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    var viewmodel = logic.HandleTaskGet(projectId, id);
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

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int projectId, int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleTaskEdit(collection, projectId, id);

                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Task successfully updated!"), JsonRequestBehavior.AllowGet);
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

        // GET: Task/Delete/5
        public ActionResult Delete(int projectId, int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    var viewmodel = logic.HandleTaskGet(projectId, id);
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

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int projectId, int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleTaskDelete(projectId, id, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Task successfully removed!"), JsonRequestBehavior.AllowGet);
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