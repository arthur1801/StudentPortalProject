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
        public Boolean CheckIfTimeOverlaps(Course c,TimeSpan start,TimeSpan end)
        {
            Boolean r1 = c.start_time > start && c.start_time < end;
            Boolean r2 = c.end_time > start && c.end_time < end;
            Boolean r3 = c.start_time < start && c.end_time > end;
            Boolean r4 = c.start_time == start || c.end_time == end;
            if (r1 || r2 || r3 || r4)
            {
                return true;
            }
            return false;
        }
        public Boolean CheckIfCourseOverlaps(string Student_id, int course_id)
        {
            PDAL DBConnection = new PDAL();
            List<int> StudentsCourses_id = (
                from x in DBConnection.UsersCoursess where x.id==Student_id select x.course).ToList<int>();
            
            Course toCheck = DBConnection.Courses.Find(course_id);
            foreach (int n in StudentsCourses_id)
            {
                Course a = DBConnection.Courses.Find(n);
                if (a.day == toCheck.day)
                {
                    if (CheckIfTimeOverlaps(a, toCheck.start_time, toCheck.end_time))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public ActionResult AddStudentToCource()
        {
            int course_id = Convert.ToInt32(Request.Form["Course_id"]);
            string Student_id = (string)Request.Form["Student_id"];
            PDAL DBconnection = new PDAL();
            UsersCourses Student = DBconnection.UsersCoursess.Find(Student_id, course_id);
            if (Student == null)
            {
                Session["Alert"] = "Student is alredy tacking the course or no such student.";
                return Redirect("~/Administration/AdminMain");
            }
            string role = (DBconnection.Users.Find(Student_id)).role.ToString();
            if (Regex.Replace(role, " ", "") != "1")
            {
                Session["Alert"] = "The user is not student.";
                return Redirect("~/Administration/AdminMain");
            }
            if(CheckIfCourseOverlaps(Student_id, course_id))
            {
                Session["Alert"] = "Course overlap with another course.";
                return Redirect("~/Administration/AdminMain");
            }
            Student = new UsersCourses();
            Student.id = Student_id;Student.course = course_id;
            Student.moed1 = Student.moed2 = Student.final = 0;
            DBconnection.UsersCoursess.Add(Student);
            DBconnection.SaveChanges();

            return Redirect("~/Administration/AdminMain");
        }


        public ActionResult ChangeCourseTime()
        {
            string day = Request.Form["day"];
            TimeSpan start, end;
            try
            {
                start = TimeSpan.Parse(Request.Form["start"]);
                end = TimeSpan.Parse(Request.Form["end"]);
                if (start >= end) { Session["Alert"] = "Bad time format."; ; return Redirect("~/Administration/AdminMain"); }
            }
            catch (Exception a)
            {
                a = null;
                Session["Alert"] = "Bad time format."; ; return Redirect("~/Administration/AdminMain");
            }
            int Course_id = Convert.ToInt32(Request.Form["Course_id"]);
            PDAL DBconnection = new PDAL();
            Course c = DBconnection.Courses.Find(Course_id);
            if (c == null) { Session["Alert"] = "No such course."; ; return Redirect("~/Administration/AdminMain"); }
            foreach(Course a in DBconnection.Courses)
            {
                if (a.day == c.day)
                {
                    if (a != c)
                    {
                        if (CheckIfTimeOverlaps(a, start, end))
                        {
                            Session["Alert"] = "Time overlaps other course.";
                            return Redirect("~/Administration/AdminMain");
                        }
                    }
                }
            }
            c.end_time = end;
            c.start_time = start;
            DBconnection.SaveChanges();
            return Redirect("~/Administration/AdminMain");
        }
    }
}