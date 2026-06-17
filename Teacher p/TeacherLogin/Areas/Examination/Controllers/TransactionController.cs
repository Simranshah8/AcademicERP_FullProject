using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.Examination.Controllers
{
    public class TransactionController : TeacherLogin.Controllers.BaseController
    {
        // GET: Examination/Transaction
        #region"Height & Weight"
        public ActionResult HeightWeight()
        {
            return View();
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetClassForClassTeacher()
        {
            List<TeacherLogin.Models.Teacher.ClassTeacher> dataColl = new List<TeacherLogin.Models.Teacher.ClassTeacher>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetClassForClassTeacher", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                dataColl = (List<Models.Teacher.ClassTeacher>)request.Execute<List<TeacherLogin.Models.Teacher.ClassTeacher>>(dataColl, keyValues);
            }
            catch (Exception ee)
            {
                List<TeacherLogin.Models.Teacher.ClassTeacher> err = new List<TeacherLogin.Models.Teacher.ClassTeacher>();
            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetHeightWeightById(TeacherLogin.Models.Teacher.HeightAndWeight Data)
        {
            List<TeacherLogin.Models.Teacher.HeightAndWeight> dataColl = new List<TeacherLogin.Models.Teacher.HeightAndWeight>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetExamWiseHeightWeight", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    classId = Data.classId,
                    sectionId = Data.sectionId,
                    examTypeId = Data.examTypeId
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.HeightAndWeight>>(para, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.HeightAndWeight>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.HeightAndWeight err = new TeacherLogin.Models.Teacher.HeightAndWeight();

            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveHeightAndWeight()
        {
            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                var dataColl = DeserializeObject<List<TeacherLogin.Models.Teacher.HeightAndWeight>>(Request["jsonData"]);
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddExamWiseHeightWeight", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(dataColl, keyValues);
                if (responseData != null)
                {
                    resVal = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.HeightAndWeight err = new TeacherLogin.Models.Teacher.HeightAndWeight();
            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };
        }
        #endregion

        #region "Indicator"
        public ActionResult Indicator()
        {
            return View();
        }

        #endregion

        #region "ICMarkEntry"
        public ActionResult ICMarkEntry()
        {
            return View();
        }

        #endregion

        #region "ICTabulation"
        public ActionResult ICTabulation()
        {
            return View();
        }

        #endregion


        #region "ICMarksheet"
        public ActionResult ICMarksheet()
        {
            return View();
        }

        #endregion
        
        #region "Class Test"
        public ActionResult ClassTest()
        {
            return View();
        }

        #endregion






        public ActionResult MarkSubmitStatus()
        {
            return View();
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetMarkSubmit(int ExamTypeId)
        {
            TeacherLogin.Models.Teacher.MarkSubmitCollections dataColl = new TeacherLogin.Models.Teacher.MarkSubmitCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetMarkSubmit", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    ExamTypeId = ExamTypeId,
                    
                };
                var responseData = request.Execute<TeacherLogin.Models.Teacher.MarkSubmitCollections>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.MarkSubmitCollections)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.MarkSubmit err = new TeacherLogin.Models.Teacher.MarkSubmit();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

    }
}