using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Course
    {

        [Key]
        public int course_id { get; set; }

        public string lecturer_id { get; set; }
        public string day { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        public DateTime moed1 { get; set; }
        public DateTime moed2 { get; set; }

        public string name { get; set; }

    }   
}