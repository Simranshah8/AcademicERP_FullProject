using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Controllers
{
    public class ReportEngineController : TeacherLogin.Controllers.BaseController
    {
        // GET: ReportEngine
        [HttpGet]
        public JsonNetResult GetReportTemplates(int entityId, int voucherId, bool isTran)
        {
            TeacherLogin.Models.ReportTempletesAttachmentsCollections dataColl = new TeacherLogin.Models.ReportTempletesAttachmentsCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetRptTemplates", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    entityId = entityId,
                    voucherId = voucherId,
                    isTran = isTran
                };
                var responseData = request.Execute<TeacherLogin.Models.ReportTempletesAttachmentsCollections>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.ReportTempletesAttachmentsCollections)responseData);
                }
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
           
        }

    }
}