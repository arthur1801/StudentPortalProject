using Project.Dal;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AdminMain()
        {
            string id = (string)Session["id"];
            PDAL DBConnection = new PDAL();
            List<Course> List_Courses =
                (from x in DBConnection.Courses select x).ToList<Course>();

            ViewModel vm = new ViewModel();
            vm.ListCource = List_Courses;
            return View("AdminMain", vm);
        }


        public ActionResult GetStudentListByJSON()
        {
            int id = Convert.ToInt32(Session["Course_id"]);
            PDAL DBConnection = new PDAL();
            List<UsersCourses> Students =
                (from x in DBConnection.UsersCoursess where x.course == id select x).ToList<UsersCourses>();
            return Json(Students, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeStudentGrade()
        {


            if ((Request.Form["gridRadios1"] != null) && (Request.Form["Student_id"] != null))
            {
                string student_id = Request.Form["Student_id"];
                int course_id = Convert.ToInt32(Request.Form["Course_id"]);
                int grade = Convert.ToInt32(Request.Form["Grade"]);
                PDAL DBconnection = new PDAL();
                UsersCourses Student = DBconnection.UsersCoursess.Find(student_id, course_id);
                string option = Request.Form["gridRadios1"].ToString();
                if (Student != null)
                {
                    if (option.Equals("option1"))
                    {
                        Student.moed1 = grade;
                        Student.final = grade;
                    }
                    else if (option.Equals("option2"))
                    {
                        Student.moed2 = grade;
                        Student.final = grade;
                    }
                    else
                    {
                        Student.final = grade;
                    }
                }


                DBconnection.SaveChanges();
            }

            return Redirect("~/Administration/AdminMain");
        }
        public ActionResult ShowStudentList()
        {
            Session["Course_id"] = Request.Form["Course_id"];
            return AdminMain();
        }
        public ActionResult logout()
        {
            Session["name"] = "";
            Session["ID"] = "";
            Session["Course_id"] = "0";
            Session["role"] = "";
            return Redirect("~");
        }
        public bool CheckIfTimeOverlaps()
        {



            return true;
        }


        public ActionResult AddStudentToCource()
        {
            
            


            return Redirect("~/Administrator/AdminMain");
        }
    }
}