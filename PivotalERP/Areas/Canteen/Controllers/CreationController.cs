using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using Newtonsoft.Json;
using PivotalERP.Models;
using AcademicLib.BE.Global;
namespace PivotalERP.Areas.Canteen.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: Canteen/Creation
        
        public ActionResult CanteenMaster()
        {
            return View();
        }
    }
}