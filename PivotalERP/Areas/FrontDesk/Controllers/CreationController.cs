using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.FrontDesk.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: FrontDesk/Creation
        public ActionResult Index()
        {
            return View();
        }
    }
}