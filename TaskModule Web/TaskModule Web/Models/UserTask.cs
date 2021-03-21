using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskModule_Web.Models
{
    public class UserTask
    {
        //UserTaskModel
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        //At First , Task is in incomplete state
        public bool IsComplete { get; set; } = false;
        public bool IsAssign { get; set; }
        //Foreign Key of CustomerID
        [ForeignKey(nameof(Usermodel))]
        public int UserId { get; set; }
        public UserModel Usermodel { get; set; }
        //To avoid datetime2 to datetime error
        
    }
}