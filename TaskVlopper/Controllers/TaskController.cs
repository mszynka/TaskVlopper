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
        public IUnityContainer container = UnityConfig.GetConfiguredContainer();
        

        [HttpGet]
        public ActionResult Index(int projectId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    var model = logic.GetAllTasksForGivenProjectAndCurrentUser(projectId, User.Identity.Name);
                    var viewModel = new TasksViewModel(model.ToList());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                JsonDataHandler handler = new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                JsonDataHandler handler = new JsonDataHandler(ex);
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
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    var viewModel = new TaskViewModel(logic.HandleTaskGet(projectId, id));
                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                JsonDataHandler handler = new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                JsonDataHandler handler = new JsonDataHandler(ex);
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
            JsonDataHandler handler = new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden);
            return View("Error", handler.handleError());
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(int projectId, Base.Model.Task task)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    logic.HandleTaskAdd(task, projectId, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Task successfully created!"), JsonRequestBehavior.AllowGet);
                }
                JsonDataHandler handler = new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                JsonDataHandler handler = new JsonDataHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int projectId, int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    var viewmodel = logic.HandleTaskGet(projectId, id);
                    return PartialView(viewmodel);
                }
                JsonDataHandler handler = new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                JsonDataHandler handler = new JsonDataHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int projectId, int id, Base.Model.Task task)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    logic.HandleTaskEdit(task, projectId, id);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Task successfully updated!"), JsonRequestBehavior.AllowGet);
                }
                JsonDataHandler handler = new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                JsonDataHandler handler = new JsonDataHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int projectId, int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    var viewmodel = logic.HandleTaskGet(projectId, id);
                    return View(viewmodel);
                }
                JsonDataHandler handler = new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                JsonDataHandler handler = new JsonDataHandler(ex);
                return View("Error", handler.handleError());
            }
        }

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int projectId, int id, Base.Model.Task task)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    logic.HandleTaskDelete(projectId, id, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Task successfully removed!"), JsonRequestBehavior.AllowGet);
                }
                JsonDataHandler handler = new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden);
                return View("Error", handler.handleError());
            }
            catch (Exception ex)
            {
                JsonDataHandler handler = new JsonDataHandler(ex);
                return View("Error", handler.handleError());
            }
        }
    }
}