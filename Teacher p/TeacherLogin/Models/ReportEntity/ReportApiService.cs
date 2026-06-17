using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TeacherLogin.Models
{
    public class ReportApiService
    {
        protected string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["baseURL"].ToString() + "/v1/";

        public ReportApiService(TeacherLogin.Models.Teacher.TeacherLogin user)
        {
            this.user = user;
        }
        TeacherLogin.Models.Teacher.TeacherLogin user = null;
        public ReportTempletes GetReportTemplate(int entityId, int voucherId, bool isTran, int rptTranId)
        {
            var template = new ReportTempletes();
            try
            {
                var request = new APIRequest(BaseUrl, "General/GetRptTemplates", "POST");
                var keyValues = new Dictionary<string, string> { { "Bearer", user.access_token } };

                var para = new
                {
                    entityId,
                    voucherId,
                    isTran,
                    rptTranId
                };

                var response = request.Execute<ReportTempletes>(para, keyValues);
                if (response != null)
                    template = (ReportTempletes)response;
            }
            catch { }
            return template;
        }

        public AboutCompany GetCompanyDetails()
        {
            var comDet = new AboutCompany();
            try
            {
                var request = new APIRequest(BaseUrl, "General/GetAboutCompany", "POST");
                var keyValues = new Dictionary<string, string> { { "Bearer", user.access_token } };

                var response = request.Execute<AboutCompany>(comDet, keyValues);
                if (response != null)
                    comDet = (AboutCompany)response;
            }
            catch (Exception ee)
            {
                comDet.IsSuccess = false;
                comDet.ResponseMSG = ee.Message;
            }
            return comDet;
        }

        public List<Models.Teacher.AcademicYear> GetAcademicYearList()
        {
            var dataColl = new List<Models.Teacher.AcademicYear>();
            try
            {
                var request = new APIRequest(BaseUrl, "General/GetAcademicYearList", "POST");
                var keyValues = new Dictionary<string, string> { { "Bearer", user.access_token } };

                var response = request.Execute<List<Models.Teacher.AcademicYear>>(dataColl, keyValues);
                if (response != null)
                    dataColl = (List<Models.Teacher.AcademicYear>)response;
            }
            catch { }
            return dataColl;
        }

    }
}