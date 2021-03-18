using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskModule_Web.Models
{
    public class UserTask
    {
        //UserTaskModel
        public int Id { get; set; }
        public int Name { get; set; }
        //At First , Task is in incomplete state
        public bool IsComplete { get; set; } = false;
        //Foreign Key of UserID
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
    }
}