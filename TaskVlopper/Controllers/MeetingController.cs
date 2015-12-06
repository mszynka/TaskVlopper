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
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
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
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
            }
        }

        // GET: Meeting/Create
        public ActionResult Create(int projectId, int? taskId)
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
        public ActionResult Create(FormCollection collection, int projectId, int? taskId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleMeetingAdd(collection, projectId, taskId, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Meeting successfully created!"), JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
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
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
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
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
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
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
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
                Response.StatusCode = (int)HttpCodeEnum.Forbidden;
                return View("Error");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.Message);
                Response.StatusCode = (int)HttpCodeEnum.InternalServerError;
                return View("Error");
            }
        }
    }
}