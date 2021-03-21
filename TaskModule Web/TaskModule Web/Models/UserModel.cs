using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskModule_Web.Models
{
    public class UserModel
    {


        //Customer Model
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
      
        public string City { get; set; }
       
      
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
       
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
      
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }
       

      

    
    }
}