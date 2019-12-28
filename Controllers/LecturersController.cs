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
            string id = (string)Session["id"];
            string Rq_form = (string)Request.Form["Course_id"];
            int req_course_id = -1;
            if (Rq_form != "") { req_course_id = Convert.ToInt32(Request.Form["Course_id"]); }
            PDAL DBconnaction = new PDAL();
            Course CourseIns = DBconnaction.Courses.Find(req_course_id);
            if (CourseIns != null)
            {
                if (Regex.Replace(CourseIns.lecturer_id, " ", "") == id)
                {
                    Session["Course_id"] = req_course_id.ToString();
                    return LecturerMain();
                }
            }
            Session["Course_id"] = "Denied";
            return LecturerMain();
        }
            

        public ActionResult GetStudentListByJSON()
        {
            try{
                int id = Convert.ToInt32(Session["Course_id"]);
                PDAL DBConnection = new PDAL();
                List<UsersCourses> Students =
                    (from x in DBConnection.UsersCoursess where x.course == id select x).ToList<UsersCourses>();
                return Json(Students, JsonRequestBehavior.AllowGet);
            }
            catch(Exception a)
            {
                var v = a;
                v = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult logout()
        {
            Session["name"] = "";
            Session["ID"] = "";
            Session["Course_id"] = "0";
            Session["role"] = "";
            return Redirect("~");
        }
        public ActionResult ChangeStudentGrade()
        {
            
            if ((Request.Form["gridRadios1"] != null)&&(Request.Form["Student_id"]!=null)){
                string student_id = Request.Form["Student_id"];
                int course_id = Convert.ToInt32(Request.Form["Course_id"]);
                int grade = Convert.ToInt32(Request.Form["Grade"]);
                string Lecturer = (string)Session["id"];
                PDAL DBconnection = new PDAL();

                UsersCourses Student = DBconnection.UsersCoursess.Find(student_id, course_id);
                string option = Request.Form["gridRadios1"].ToString();
                string Course_Lecturer = "-1";
                try
                { 
                    Course_Lecturer = (DBconnection.Courses.Find(course_id).lecturer_id).ToString();
                }
                catch (Exception a)
                {
                    if (a != null)
                    {
                        Course_Lecturer = "-1";
                    }
                }
                if (Regex.Replace(Course_Lecturer, " ", "") != Lecturer)
                {
                    Session["Alert"] = "You dont have permission to change that course.";
                    return Redirect("~/Lecturers/LecturerMain");
                }
                if (Student == null) {
                    Session["Alert"] = "No such student.";
                    return Redirect("~/Lecturers/LecturerMain"); 
                }

                if (option.Equals("option1"))
                {
                    Student.moed1 = grade;
                    Student.final = grade;
                }
                else if(option.Equals("option2"))
                {
                    Student.moed2 = grade;
                    Student.final = grade;
                }
                else
                {
                    Student.final = grade;
                }


                DBconnection.SaveChanges();
            }
            
            return Redirect("~/Lecturers/LecturerMain");
        }

    }
}