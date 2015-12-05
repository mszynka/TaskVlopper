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
    public class ProjectController : Controller
    {
        static IUnityContainer container = UnityConfig.GetConfiguredContainer();
        static IProjectLogic logic = container.Resolve<IProjectLogic>();

        // GET: Project
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {

                    var model = logic.GetAllProjectsForCurrentUser(User.Identity.Name);
                    var viewModel = new ProjectsViewModel(model.ToList());

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

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewModel = new ProjectViewModel(logic.HandleProjectGet(id));
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

        // GET: Project/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            Response.StatusCode = (int)HttpCodeEnum.Forbidden;
            return View("Error");
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleProjectAdd(collection, User.Identity.Name);

                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Created, "Project successfully created!"), JsonRequestBehavior.AllowGet);
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

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewmodel = new ProjectViewModel(logic.HandleProjectGet(id));
                    return PartialView(viewmodel);
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

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleProjectEdit(collection, id);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.Accepted, "Project successfully updated!"), JsonRequestBehavior.AllowGet);
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

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var viewmodel = logic.HandleProjectGet(id);
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

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    logic.HandleProjectDelete(id, User.Identity.Name);
                    return Json(JsonHelpers.HttpMessage(HttpCodeEnum.OK, "Project successfully removed!"), JsonRequestBehavior.AllowGet);
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
