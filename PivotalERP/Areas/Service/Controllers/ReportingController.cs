using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
namespace PivotalERP.Areas.Service.Controllers
{
    public class ReportingController : PivotalERP.Controllers.BaseController
    {
        
        public ActionResult JobCardList()
        {
            return View();
        }

      
    }
}