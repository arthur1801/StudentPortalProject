using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.Dal;

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
            string ID = Request.Form["ID"];
            
            User logedin = new User
            {
                Name = "Arthur",
                ID = 320582232,
                Role = "Student",
                pass = "123456"

            };
            return View("Login");
        }

    }
}