using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.QuickAccess.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: QuickAccess/Creation
        public ActionResult QuickAccess()
        {
            return View();
        }
    }
}