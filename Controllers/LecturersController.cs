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
    }
}