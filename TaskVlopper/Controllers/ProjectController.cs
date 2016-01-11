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
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/GetAllWithStats
        [HttpGet]
        public ActionResult GetAllWithStats()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic projectLogic = container.Resolve<IProjectLogic>();
                    ITaskLogic taskLogic = container.Resolve<ITaskLogic>();
                    IMeetingLogic meetingLogic = container.Resolve<IMeetingLogic>();

                    var projectLists = projectLogic.GetAllProjectsForCurrentUser(User.Identity.Name)
                        .Select(x => new ProjectViewModel(
                            x,
                            new ProjectStatisticsViewModel(
                                taskLogic.CountAllTasksForGivenProjectAndCurrentUser(x.ID, User.Identity.Name),
                                meetingLogic.CountAllFutureMeetingsForCurrentUserAndProject(User.Identity.Name, x.ID),
                                projectLogic.CountAllUsersForProject(x.ID)
                            ))
                        )
                        .ToList();
                    var viewModel = new ProjectsViewModel(projectLists);

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic projectLogic = container.Resolve<IProjectLogic>();
                    ITaskLogic taskLogic = container.Resolve<ITaskLogic>();
                    IMeetingLogic meetingLogic = container.Resolve<IMeetingLogic>();

                    var viewModel = new ProjectViewModel(projectLogic.HandleProjectGet(id), 
                        new ProjectStatisticsViewModel(
                                taskLogic.CountAllTasksForGivenProjectAndCurrentUser(id, User.Identity.Name),
                                meetingLogic.CountAllFutureMeetingsForCurrentUserAndProject(User.Identity.Name, id),
                                projectLogic.CountAllUsersForProject(id)
                            ));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/DetailsWithStats/5
        [HttpGet]
        public ActionResult DetailsWithStats(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    var viewModel = new ProjectViewModel(logic.HandleProjectGet(id));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "User authenticated!").getInfo(), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    logic.HandleProjectAdd(project, User.Identity.Name);

                    return Json(new JsonDataHandler(
                        httpCode: HttpCodeEnum.Created, 
                        message: "Project successfully created!",
                        id: project.ID.ToString()).getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    var viewmodel = new ProjectViewModel(logic.HandleProjectGet(id));

                    return Json(viewmodel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
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

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Accepted, message: "Project successfully updated!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    var viewmodel = new ProjectViewModel(logic.HandleProjectGet(id));

                    return Json(viewmodel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
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

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "Project successfully removed!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Users/5
        [HttpGet]
        public ActionResult Users(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    var queryProjectUsers = logic.GetAllUsersForGivenProject(id);

                    var viewModel = new UsersViewModel();
                    viewModel.Users.AddRange(queryProjectUsers.Select(x => new UserViewModel(x)));

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Project/Users/5
        [HttpPost]
        public ActionResult Users(int id, string userId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userManager = container.Resolve<ApplicationUserManager>();
                    userManager.Users.First(x => x.Email == userId);

                    IProjectLogic logic = container.Resolve<IProjectLogic>();
                    logic.AssignUserToProject(id, userId);

                    return Json(new JsonDataHandler(httpCode: HttpCodeEnum.OK, message: "User successfully assigned!").getInfo(), JsonRequestBehavior.AllowGet);
                }
                return Json(new JsonDataHandler(httpCode: HttpCodeEnum.Forbidden).getWarning(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonDataHandler(ex).getError(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UnbindUser(int id , string userId)
        {
            throw new NotImplementedException();
        }
    }
}