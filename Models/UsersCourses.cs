using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class UsersCourses
    {
        [Key]
        [Column(Order = 0)]
        public string id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int course { get; set; }
        public int moed1 { get; set; }
        public int moed2 { get; set; }


    }
}