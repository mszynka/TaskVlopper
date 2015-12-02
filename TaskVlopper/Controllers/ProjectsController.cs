using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskVlopper.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ActionResult Index()
        {
            return Json(HttpNotFound());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            return Json(HttpNotFound());
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return Json(HttpNotFound());
        }

        // POST: Projects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return Json(HttpNotFound());
            }
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {
            return Json(HttpNotFound());
        }

        // POST: Projects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return Json(HttpNotFound());
            }
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
            return Json(HttpNotFound());
        }

        // POST: Projects/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return Json(HttpNotFound());
            }
        }
    }
}
