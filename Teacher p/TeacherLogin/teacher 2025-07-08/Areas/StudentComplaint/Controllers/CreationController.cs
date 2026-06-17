using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.StudentComplaint.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: StudentComplaint/Creation
        public ActionResult StudentComplaint()
        {
            return View();
        }
    }
}