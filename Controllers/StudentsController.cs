using Project.Dal;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult StudentMain()
        {
            string id = (string)Session["id"];
            PDAL DBConnection = new PDAL();
            List<UsersCourses> List_usersCourses = 
                (from x in DBConnection.UsersCoursess where id == x.id select x).ToList<UsersCourses>();

            List<int> UsersCourseString =
                (from x in DBConnection.UsersCoursess where id == x.id select x.course).ToList<int>();
            List<Course> ALL_courses = (from x in DBConnection.Courses select x).ToList<Course>();
            List<Course> Courses_user_in = new List<Course>();
            foreach (Course c in ALL_courses)
            {
                if (UsersCourseString.Contains(c.course_id))
                {
                    Courses_user_in.Add(c);
                }
            }
                /*
                            List<Course> List_courses=
                              (from x in DBConnection.Courses 
                               where x.course_id == (
                                    from x in DBConnection.UsersCoursess where id == x.id select x.course
                               ) 
                               select x).ToList<Course>();*/




                ViewModel vm = new ViewModel();
            vm.users_courses = new UsersCourses();
            vm.ListCource = Courses_user_in;
            vm.List_users_courses = List_usersCourses;
            return View("StudentMain", vm);
        }
    }
}