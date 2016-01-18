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
                    var viewModel = new WorklogsViewModel(
                        logic.GetAllWorklogForGivenProjectAndTaskAndUser(projectId, taskId, User.Identity.Name).ToList());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Worklog/Details/5
        [HttpGet]
        public ActionResult Details(int projectId, int taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IWorklogLogic logic = container.Resolve<IWorklogLogic>();
                    var viewModel = new WorklogViewModel(logic.HandleWorklogGet(projectId, taskId, id));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Worklog/Create
        [HttpGet]
        public ActionResult Create(int projectId, int taskId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "User authenticated!").getInfo(), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
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

                    return Json(new JsonDataHandler(
                        httpCode: HttpCodeEnum.Created, 
                        message: "Worklog successfully created!",
                        id: worklog.ID.ToString()).getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Worklog/Edit/5
        [HttpGet]
        public ActionResult Edit(int projectId, int taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IWorklogLogic logic = container.Resolve<IWorklogLogic>();
                    var viewmodel = new WorklogViewModel(logic.HandleWorklogGet(projectId, taskId, id));

                    return Json(viewmodel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
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

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Accepted, message: "Worklog successfully updated!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Worklog/Delete/5
        [HttpGet]
        public ActionResult Delete(int projectId, int taskId, int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                    {
                        IWorklogLogic logic = container.Resolve<IWorklogLogic>();
                        var viewmodel = new WorklogViewModel(logic.HandleWorklogGet(projectId, taskId, id));

                        return Json(viewmodel, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
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
                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "Worklog successfully removed!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}