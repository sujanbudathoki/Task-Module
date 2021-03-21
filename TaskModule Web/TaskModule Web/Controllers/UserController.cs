using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskModule_Web.Models;
using TaskModule_Web.Repositories;

namespace TaskModule_Web.Controllers
{
    public class UserController : Controller
    {
        IRepository<UserModel> _users;
     
        public UserController(IRepository<UserModel> usercontext)
        {
            this._users = usercontext;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            //Checking for validation if details given by user is valid or not
            UserModel userlogindata = _users.Collection().Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            //Checking If userLoginData is empty or not
            if (userlogindata != null)
            {
                FormsAuthentication.SetAuthCookie(userlogindata.Email, false);
                return RedirectToAction("Index","Task");
               
            }
            else
            {
                //If email exists , then password must be invalid
                if (_users.Collection().Any(x => x.Email == user.Email))
                {
                    ModelState.AddModelError("", "Invalid Password");
                    return View();
                }
                //if it doesn't , then email must be invalid.
                else
                {
                    ModelState.AddModelError("", "The given E-mail doesn't belong to any matching account. Please try again and re-enter your email");
                    return View();
                }
            }
            
        }
        //Signup
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid)
            {
                //Avoiding e-mail duplication
                if (_users.Collection().Any(x => x.Email == model.Email))
                {
                    ModelState.AddModelError("", "E-mail has been already taken");
                    return View();
                }
                else
                {
                    _users.Insert(model);
                    _users.Commit();
                }
            }
            else
            { return View(); }
            return RedirectToAction("Login");
        }       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}