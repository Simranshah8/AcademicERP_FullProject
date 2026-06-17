using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.StudentLeave.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: StudentLeave/Creation
        public ActionResult StudentLeave()
        {
            return View();
        }
    }
}