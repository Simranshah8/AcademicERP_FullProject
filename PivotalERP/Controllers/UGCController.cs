using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PivotalERP.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AcademicERP.Controllers
{
    public class UGCController : APIBaseController
    {
        // Post api/UpdateMaster
        /// <summary>
        ///  Update All Master
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateMaster()
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                
                resVal =await new UGC.BL.GlobalFN(UserId, hostName, dbName).UpdateMaster(AcademicYearId);                  
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return Json(resVal, new JsonSerializerSettings
            {
            });

        }

        // Post api/UpdateStudent
        /// <summary>
        ///  Update All Student
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateStudent()
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                resVal = await new UGC.BL.GlobalFN(UserId, hostName, dbName).UpdateStudent(AcademicYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return Json(resVal, new JsonSerializerSettings
            {
            });

        }

        // Post api/UpdateEmployee
        /// <summary>
        ///  Update All Employee
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public async Task<IHttpActionResult> UpdateEmployee()
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                resVal = await new UGC.BL.GlobalFN(UserId, hostName, dbName).UpdateEmployee(AcademicYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return Json(resVal, new JsonSerializerSettings
            {
            });

        }
    }
}
