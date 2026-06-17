using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.Examination.Controllers
{
    public class ReportingController : TeacherLogin.Controllers.BaseController
    {
        // GET: Examination/Reporting
        public ActionResult Tabulation()
        {
            return View();
        }

        public ActionResult RdlTabulation()
        {
            return View();
        }
        public ActionResult Marksheet()
        {
            return View();
        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetMarkSheet(int ExamTypeId,int ClassId,int? SectionId)
        {

            Models.Responce responce = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/PrintMarkSheet", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    classId=ClassId,
                    sectionId=SectionId,
                    examTypeId=ExamTypeId
                };
                var responseData = request.Execute<Models.Responce>(para, keyValues);


                if (responseData != null)
                {
                    responce = ((Models.Responce)responseData);

                    if (responce.IsSuccess)
                        responce.ResponseMSG = BaseUrl.Replace("v1/","") + responce.ResponseMSG;
                }
            }
            catch (Exception ee)
            {
                responce.IsSuccess = false;
                responce.ResponseMSG = ee.Message;
            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = responce };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetGroupMarkSheet(int ExamTypeGroupId, int ClassId, int? SectionId)
        {

            Models.Responce responce = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/PrintGroupMarkSheet", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    classId = ClassId,
                    sectionId = SectionId,
                    examTypeGroupId = ExamTypeGroupId
                };
                var responseData = request.Execute<Models.Responce>(para, keyValues);

                if(responseData is string)
                {
                    responce.IsSuccess = false;
                    responce.ResponseMSG = responseData.ToString();
                }
                else
                {
                    if (responseData != null)
                    {
                        responce = ((Models.Responce)responseData);

                        if (responce.IsSuccess)
                            responce.ResponseMSG = BaseUrl.Replace("v1/", "") + responce.ResponseMSG;
                    }
                }
                
            }
            catch (Exception ee)
            {
                responce.IsSuccess = false;
                responce.ResponseMSG = ee.Message;
            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = responce };
        }

        public ActionResult Analysis()
        {
            return View();
        }

        public ActionResult ExamEvaluation()
        {
            return View();
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentEvaluation(int ClassId, int SectionId, int ExamTypeId, string ExamTypeIdColl = "", string StudentIdColl = "", int? BatchId = 0, int? SemesterId = 0, int? ClassYearId = 0)
        {
            TeacherLogin.Models.Teacher.StudentEvalCollections dataColl = new TeacherLogin.Models.Teacher.StudentEvalCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetStudentEvaluation", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    ClassId = ClassId,
                    SectionId = SectionId,
                    ExamTypeId = ExamTypeId,
                    ExamTypeIdColl = ExamTypeIdColl,
                    StudentIdColl = StudentIdColl,
                    BatchId = BatchId,
                    SemesterId = SemesterId,
                    ClassYearId = ClassYearId
                };
                var responseData = request.Execute<TeacherLogin.Models.Teacher.StudentEvalCollections>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.StudentEvalCollections)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.StudentEval err = new TeacherLogin.Models.Teacher.StudentEval();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
    }
}