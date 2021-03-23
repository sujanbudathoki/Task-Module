using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskModule_Web.Models;
using TaskModule_Web.Repositories;
using TaskModule_Web.ViewModels;

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
                    CreatedAt = DateTime.Now
                    
                    
                   
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
        public ActionResult Delete(int Id)
        {
            UserTask tasks = _tasks.Find(Id);
            if(tasks!=null)
            {
                return View(tasks);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ActionName("Delete")]//To avoid same parameter clash betn get and post we rename to confirmdelete but for routing "delete" used.
        public ActionResult ConfirmDelete(int Id)
        {
            UserTask user_task = _tasks.Find(Id);
            if(user_task!=null)
            {
                _tasks.Delete(Id);
                _tasks.Commit();
                return RedirectToAction("Index");

            }
            else
            {
                return HttpNotFound();
            }
            
        }
        public ActionResult Edit(int Id)
        {
            UserTask task = _tasks.Collection().Where(x => x.Id == Id).FirstOrDefault();
            return View(task);
        }
        [HttpPost]
        public ActionResult Edit(UserTask editedtask,int Id)
        {
            UserTask task = _tasks.Collection().Where(x => x.Id == Id).FirstOrDefault();
            if (task != null)
            {
                task.Name = editedtask.Name;
                task.Description = editedtask.Description;
                task.IsAssign = editedtask.IsAssign;
                task.IsComplete = editedtask.IsComplete;

                if (ModelState.IsValid)
                {
                    _tasks.Edit(task);
                    _tasks.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is some error in data.");
                    return View();
                }

            }
            return HttpNotFound();
           
        }

        public ActionResult Details(int Id)
        {
            UserTask task = _tasks.Collection().Where(x => x.Id == Id).FirstOrDefault();
            if (task != null)
            {
                return View(task);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult Assign(int Id)
        {
            UserTask task = _tasks.Collection().Where(x => x.Id == Id).FirstOrDefault();
            return View(task);
        }
        [HttpPost]
        public ActionResult Assign(UserTask editedtask,int Id)
        {
            if (ModelState.IsValid)
            {
                
                UserTask task = _tasks.Collection().Where(x => x.Id == Id).FirstOrDefault();
                double timespan = (task.CreatedAt - editedtask.Deadline).Value.Hours;
                 if(timespan>0)//ie Created at is greater which means deadline is somewhat before which is invalid.
                {
                    ModelState.AddModelError("", "Deadline invalid , seems past date");
                    return View();
                }
                task.IsAssign = true;
                task.Deadline = editedtask.Deadline;
                _tasks.Edit(task);
                _tasks.Commit();
            }
            return View("AssignedMessage");

        }
        public ActionResult AssignedMessage()
        {
            return View();
        }


        //return lists of asssigned tasks
        public ActionResult AssignedTaskList()
        {
            UserModel Lin_User = _customers.Collection().Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            
            IEnumerable < UserTask > tasks = _tasks.Collection().Where(x => x.UserId == Lin_User.Id && x.IsAssign == true).ToList();
            return View(tasks);

        }
        //return lists of Completed tasks
        public ActionResult CompletedTaskList()
        {
            UserModel usermodel = _customers.Collection().Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            IEnumerable<UserTask> tasks = _tasks.Collection().Where(x => x.UserId == usermodel.Id && x.IsComplete == true).ToList();
            return View(tasks);
        }

        public ActionResult CompleteTask(int id)
        {
            UserTask task = _tasks.Collection().Where(x => x.Id == id).FirstOrDefault();
            if(task!=null)
            {
                task.IsComplete = true;
                _tasks.Edit(task);
                _tasks.Commit();
                return RedirectToAction("CompletedTaskList");
            }
            else
            {
                return HttpNotFound();
            }

        }

       
    }
}