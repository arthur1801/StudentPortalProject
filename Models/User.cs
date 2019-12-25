using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class User
    {
        public string name { get; set; }
        [Key]
        public string id { get; set; }
        public string role { get; set; }
        public string password { get; set; }
        
    }
}