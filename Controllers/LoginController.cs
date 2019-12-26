using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.Dal;
using System.Text.RegularExpressions;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {

            return View("Login");
        }
        public ActionResult connect()
        {
            string pass = Request.Form["Password"];
            string ID = Request.Form["user_id"];
            PDAL DBConnection = new PDAL();
            List<User> UserList =
                (from x in DBConnection.Users where x.id == ID && x.password == pass select x).ToList<User>();


            try
            {
                Session["id"] = Regex.Replace(UserList[0].id, " ", ""); 
                Session["role"] = Regex.Replace(UserList[0].role, " ", "");
                Session["name"] = UserList[0].name;
            }
            catch (Exception) { return Redirect("~"); }

            if (Session["role"].ToString()== "1")
                return RedirectToAction("StudentMain","Students");
            else if (Session["role"].ToString() == "2")
                return RedirectToAction("LecturerMain", "Lecturers");
            else if (Session["role"].ToString() == "3")
                return RedirectToAction("AdminMain", "Administration");
            else
                return Redirect("~");
        }

    }
}