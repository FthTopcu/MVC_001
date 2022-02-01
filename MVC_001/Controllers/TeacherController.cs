using MVC_001.DataAccess;
using MVC_001.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_001.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            List<Teacher> teachers = TeacherDAL.Methods.List();
            return View(teachers);
        }
        public ActionResult Add()
        {
            Teacher teacher = new Teacher();
            return View(teacher);
        }
        [HttpPost]
        public ActionResult Add(Teacher teacher)
        {
            TempData["insertedID"] = TeacherDAL.Methods.Add(teacher);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {

            return View(TeacherDAL.Methods.GetById(id));
        }
        [HttpPost]
        public ActionResult Delete(Teacher teacher)
        {
            int affectedRows = TeacherDAL.Methods.Delete(teacher.ID);
            if (affectedRows > 0)
                TempData["message"] = "suksesfuluy";
            else
                TempData["message"] = "unsuksesfuluy";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(TeacherDAL.Methods.GetById(id));
        }
        public ActionResult Edit(int id)
        {
            return View(TeacherDAL.Methods.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            int affectedRows = TeacherDAL.Methods.Update(teacher);
            if (affectedRows > 0)
                TempData["message"] = "suksesfuluy";
            else
                TempData["message"] = "unsuksesfuluy";
            return RedirectToAction("Index");
        }
        
    }
}