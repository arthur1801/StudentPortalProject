using Project.Dal;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class LecturersController : Controller
    {
        // GET: Lectures
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LecturerMain()
        {
            string id = (string)Session["id"];
            PDAL DBConnection = new PDAL();
            List<Course> List_Courses =
                (from x in DBConnection.Courses where id == x.lecturer_id select x).ToList<Course>();
            
            ViewModel vm = new ViewModel();
            vm.ListCource = List_Courses;
            return View("LecturerMain", vm);
        }

        public ActionResult ShowStudentList()
        {
            Session["Course_id"] = Request.Form["Course_id"];
            return LecturerMain();
        }

        public ActionResult GetStudentListByJSON()
        {
            
            int id = Convert.ToInt32(Session["Course_id"]);
            PDAL DBConnection = new PDAL();
            List<UsersCourses> Students =
                (from x in DBConnection.UsersCoursess where x.course==id select x).ToList<UsersCourses>();
            return Json(Students,JsonRequestBehavior.AllowGet);
        }


    }
}