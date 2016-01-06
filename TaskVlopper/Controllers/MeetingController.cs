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
    public class MeetingController : Controller
    {
        public IUnityContainer container = UnityConfig.GetConfiguredContainer();
        
        // GET: Meeting/ForCurrentUser
        [HttpGet]
        public ActionResult ForCurrentUser()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IMeetingLogic logic = container.Resolve<IMeetingLogic>();
                    var viewModel = logic.GetAllMeetingsForCurrentUser(User.Identity.Name);

                    return Json(new MeetingsViewModel(viewModel.ToList()), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Meeting
        [HttpGet]
        public ActionResult Index(int projectId, int? taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IMeetingLogic logic = container.Resolve<IMeetingLogic>();
                    if(taskId != null)
                    {
                        var viewModel =
                        logic.GetAllMeetingsForCurrentUserAndProjectAndTask(User.Identity.Name, projectId, taskId.Value);
                        return Json(new MeetingsViewModel(viewModel.ToList()), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var viewModel =
                                logic.GetAllMeetingsForCurrentUserAndProject(User.Identity.Name, projectId);
                        return Json(new MeetingsViewModel(viewModel.ToList()), JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Meeting/Details/5
        [HttpGet]
        public ActionResult Details(int projectId, int? taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IMeetingLogic logic = container.Resolve<IMeetingLogic>();
                    var viewModel = new MeetingViewModel(logic.HandleMeetingGet(projectId, taskId, id));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Meeting/Create
        [HttpGet]
        public ActionResult Create(int projectId, int? taskId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "User authenticated!").getInfo(), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getError(), JsonRequestBehavior.AllowGet);
        }

        // POST: Meeting/Create
        [HttpPost]
        public ActionResult Create(Meeting meeting, int projectId, int? taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IMeetingLogic logic = container.Resolve<IMeetingLogic>();
                    logic.HandleMeetingAdd(meeting, projectId, taskId, User.Identity.Name);

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Created, message: "Meeting successfully created!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Meeting/Edit/5
        [HttpGet]
        public ActionResult Edit(int projectId, int? taskId, int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                IMeetingLogic logic = container.Resolve<IMeetingLogic>();
                var viewmodel = new MeetingViewModel(logic.HandleMeetingGet(projectId, taskId, id));

                return Json(viewmodel, JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
        }

        // POST: Meeting/Edit/5
        [HttpPost]
        public ActionResult Edit(Meeting meeting, int projectId, int? taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IMeetingLogic logic = container.Resolve<IMeetingLogic>();
                    logic.HandleMeetingEdit(meeting, projectId, taskId, id);

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Accepted, message: "Meeting successfully updated!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Meeting/Delete/5
        [HttpGet]
        public ActionResult Delete(int projectId, int? taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IMeetingLogic logic = container.Resolve<IMeetingLogic>();
                    var viewmodel = new MeetingViewModel(logic.HandleMeetingGet(projectId, taskId, id));

                    return Json(viewmodel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Meeting/Delete/5
        [HttpPost]
        public ActionResult Delete(Meeting meeting, int projectId, int? taskId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IMeetingLogic logic = container.Resolve<IMeetingLogic>();
                    logic.HandleMeetingDelete(projectId, taskId, id, User.Identity.Name);

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "Meeting successfully removed!").getInfo(), JsonRequestBehavior.AllowGet);
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