using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Dal
{
    public class PDAL : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("tblUser");
            modelBuilder.Entity<Course>().ToTable("tblCourses");
            modelBuilder.Entity<UsersCourses>().ToTable("tblUsersCourses");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UsersCourses> UsersCoursess { get; set; }

    }
}