using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
namespace PivotalERP.Areas.DashBoard.Controllers
{
    public class AccountController : PivotalERP.Controllers.BaseController
    {
        // GET: DashBoard/Account
        public ActionResult Index()
        {
            return View();
        }
    }
}