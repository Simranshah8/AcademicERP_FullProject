using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace TeacherLogin.Controllers
{
    public class HomeController : TeacherLogin.Controllers.BaseController
    {
        public ActionResult Index()
        {
            ViewBag.EmpName = User.name;
            ViewBag.EmpPhotoPath = User.photoPath;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {            
            return RedirectToAction("Login", "Accounts");
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Accounts");
        }
    }
}