using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskModule_Web.Models;
using TaskModule_Web.Repositories;

namespace TaskModule_Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        IRepository<UserModel> _customers;
        IRepository<UserTask> _tasks;
        public TaskController(IRepository<UserModel> custcontext,IRepository<UserTask> usercontext)
        {
            _customers = custcontext;
            _tasks = usercontext;
        }

        
        // GET: Task
        public ActionResult Index()
        {
            //Data of an logged-in user
            UserModel Lin_User = _customers.Collection().Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            //Name contains e-mail . since , we have passed  e-mail in user controller as name at settingcookie.

            //List of Tasks
            IEnumerable<UserTask> tasks = _tasks.Collection().Where(x => x.UserId == Lin_User.Id).ToList();
            return View(tasks);


        }
        public ActionResult CreateNewTask()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewTask(UserTask tasks)
        {
            if (ModelState.IsValid)
            {
                UserModel Lin_User = _customers.Collection().Where(x => x.Email == User.Identity.Name).FirstOrDefault();
                UserTask task_data = new UserTask()
                {
                    Name = tasks.Name,
                    Description = tasks.Description,
                    UserId = Lin_User.Id,
                    IsComplete = false,
                    CreatedOn = DateTime.Now.Date
                   
                };
                _tasks.Insert(task_data);
                _tasks.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "There must be some invalid data");

                return View();
            }


        }
       
    }
}