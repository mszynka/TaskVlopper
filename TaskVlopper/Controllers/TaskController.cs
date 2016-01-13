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
using TaskVlopper.Identity;

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

                    var viewModel = new TasksViewModel(model.ToList(), logic.GetTaskStatuses());

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Task/Details/5
        [HttpGet]
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
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Task/Create
        [HttpGet]
        public ActionResult Create(int projectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "User authenticated!").getInfo(), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
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

                    return Json(new JsonDataHandler(
                        httpCode: HttpCodeEnum.Created, 
                        message: "Task successfully created!",
                        id: task.ID.ToString()).getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Task/Edit/5
        [HttpGet]
        public ActionResult Edit(int projectId, int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    var viewmodel = new TaskViewModel(logic.HandleTaskGet(projectId, id));

                    return Json(viewmodel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
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

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Accepted, message: "Task successfully updated!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Task/Delete/5
        [HttpGet]
        public ActionResult Delete(int projectId, int id)
        {
            try {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    var viewmodel = new TaskViewModel(logic.HandleTaskGet(projectId, id));

                    return Json(viewmodel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getError(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
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

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "Task successfully removed!"), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getError(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Task/Users/5
        [HttpGet]
        public ActionResult Users(int projectId, int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    var queryTaskUsers = logic.GetAllUsersForGivenTask(projectId, id);

                    var viewModel = new UsersViewModel();
                    viewModel.Users.AddRange(queryTaskUsers.Select(x => new UserViewModel(x)));
                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Task/Users/5
        [HttpPost]
        public ActionResult Users(int id, int projectId, string userId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var users = container.Resolve<ApplicationUserManager>();
                    users.Users.First(x => x.Email == userId);

                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    logic.AssignUserToProjectTask(projectId, id, userId);

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "User successfully assigned!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Task/UnbindUser/5
        [HttpPost]
        public ActionResult UnbindUser(int id, string userId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var users = container.Resolve<ApplicationUserManager>();
                    users.Users.First(x => x.Email == userId);

                    ITaskLogic logic = container.Resolve<ITaskLogic>();
                    logic.UnassignUserFromTask(id, userId);

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "User successfully assigned!").getInfo(), JsonRequestBehavior.AllowGet);
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