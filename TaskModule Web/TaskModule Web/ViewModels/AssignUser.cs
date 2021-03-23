using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskModule_Web.Models;

namespace TaskModule_Web.ViewModels
{
    public class AssignUser
    {
         public int Id { get; set; }
         public UserTask userTask { get; set; }
         public DateTime TimeLeft { get; set; }
         public bool deadlineMissed { get; set; }

    }
}