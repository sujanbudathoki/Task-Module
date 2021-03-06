using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskModule_Web.Models;

namespace TaskModule_Web.Data_Access
{
    public class DataContext : DbContext
    {
        //For code first approach
        public DataContext() : base("newtest")
        { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
    }
}