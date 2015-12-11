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
        public IUnityContainer container = UnityConfig.GetConfiguredContainer();
        

        [HttpGet]
        public ActionResult Index(int projectId, int taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IWorklogLogic logic = container.Resolve<IWorklogLogic>();
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
                    IWorklogLogic logic = container.Resolve<IWorklogLogic>();
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
        public ActionResult Create(Worklog worklog, int projectId, int taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IWorklogLogic logic = container.Resolve<IWorklogLogic>();
                    logic.HandleWorklogAdd(worklog, projectId, taskId, User.Identity.Name);
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
                    IWorklogLogic logic = container.Resolve<IWorklogLogic>();
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
        public ActionResult Edit(Worklog worklog,int projectId, int taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IWorklogLogic logic = container.Resolve<IWorklogLogic>();
                    logic.HandleWorklogEdit(worklog, projectId, taskId, id);
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
        public ActionResult Delete(int projectId, int taskId, int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        IWorklogLogic logic = container.Resolve<IWorklogLogic>();
                        var viewmodel = new WorklogViewModel(logic.HandleWorklogGet(projectId, taskId, id));

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
        public ActionResult Delete(Worklog worklog, int projectId, int taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IWorklogLogic logic = container.Resolve<IWorklogLogic>();
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