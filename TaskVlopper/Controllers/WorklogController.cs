using Microsoft.Practices.Unity;
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
    public class WorklogController : Controller
    {
        static IUnityContainer container = UnityConfig.GetConfiguredContainer();
        static IWorklogLogic logic = container.Resolve<IWorklogLogic>();

        [HttpGet]
        public ActionResult Index(int projectId, int taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewModel = logic.GetAllWorklogForGivenProjectAndTaskAndUser(projectId, taskId, User.Identity.Name);
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

        // GET: Worklog/Details/5
        public ActionResult Details(int projectId, int taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewModel = logic.HandleWorklogGet(projectId, taskId, id);
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

        // GET: Worklog/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            ExceptionHandler handler = new ExceptionHandler(errorCode: HttpCodeEnum.Forbidden);
            return View("Error", handler.handleError());
        }

        // POST: Worklog/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, int projectId, int taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleWorklogAdd(collection, projectId, taskId, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Worklog successfully created!"), JsonRequestBehavior.AllowGet);
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

        // GET: Worklog/Edit/5
        public ActionResult Edit(int projectId, int taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewmodel = logic.HandleWorklogGet(projectId, taskId, id);
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

        // POST: Worklog/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection, int id, int projectId, int taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleWorklogEdit(collection, projectId, taskId, id);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Worklog successfully updated!"), JsonRequestBehavior.AllowGet);
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

        // GET: Worklog/Delete/5
        public ActionResult Delete(int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        var repository = container.Resolve<IWorklogRepository>();
                        var viewmodel = new WorklogViewModel(repository.GetAll().ToList().Find(p => p.ID == id));

                        return View(viewmodel);
                    }
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

        // POST: Worklog/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection, int projectId, int taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleWorklogDelete(projectId, taskId, id, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Worklog successfully removed!"), JsonRequestBehavior.AllowGet);
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