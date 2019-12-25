using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class ViewModel
    {
        public ViewModel()
        {
            this.users_courses = new UsersCourses();
            List_users_courses = null;
            ListCource = null;
            SingleCourse = new Course();
        }

        public UsersCourses users_courses { get; set; }
        public List<UsersCourses> List_users_courses { get; set; }

        public List<Course> ListCource { get; set; }

        public Course SingleCourse { get; set; }




    }
}