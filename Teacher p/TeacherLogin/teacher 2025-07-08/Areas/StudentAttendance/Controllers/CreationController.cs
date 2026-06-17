using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.StudentAttendance.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: StudentAttendance/Creation/AttendanceClasswise
        public ActionResult AttendanceClasswise()
        {
            return View();
        }
        #region"ClassWiseAttendance Crude"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult AddClassWiseAttendance()
        {
            TeacherLogin.Models.Responce dataColl = new TeacherLogin.Models.Responce();
            // List<TeacherLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = JsonConvert.DeserializeObject<List<TeacherLogin.Models.Teacher.ClassWiseAttendance>>(Request["jsonData"]);
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/SaveClassWiseAttendance", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ClassWiseAttendance err = new TeacherLogin.Models.Teacher.ClassWiseAttendance();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }


        #endregion
        #region"ClassWiseAttendance"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAllClassWiseAttendance(TeacherLogin.Models.Teacher.ClassWiseAttendance Data)
        {

            List<TeacherLogin.Models.Teacher.ClassWiseAttendance> dataColl = new List<TeacherLogin.Models.Teacher.ClassWiseAttendance>();
            try
            {

                if (Data.sectionId == null)
                {
                    Data.sectionId = 0;  // Default value for null sectionId, assuming it's an integer
                }
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetClassWiseAttendance", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);

                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ClassWiseAttendance>>(Data, keyValues);


                //var responseData = request.Execute<TeacherLogin.Models.Teachers.ClassWiseAttendance>();
                if (responseData != null)
                {

                    dataColl = ((List<TeacherLogin.Models.Teacher.ClassWiseAttendance>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ClassWiseAttendance err = new TeacherLogin.Models.Teacher.ClassWiseAttendance();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"Section ANd Class Coll"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetClassSection()
        {

            TeacherLogin.Models.Responsemsg dataColl = new TeacherLogin.Models.Responsemsg();

            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetClassSectionList", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responsemsg>(dataColl, keyValues);
               
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Responsemsg)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ClassWiseAttendance err = new TeacherLogin.Models.Teacher.ClassWiseAttendance();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };


        }

        #endregion

        #region"Class Group"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetClassGroupList()
        {

            List<TeacherLogin.Models.Teacher.ClassGroup> dataColl = new List<Models.Teacher.ClassGroup>();

            try
            {

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetClassGroupList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ClassGroup>>(dataColl, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.ClassGroup>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ClassWiseAttendance err = new TeacherLogin.Models.Teacher.ClassWiseAttendance();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };


        }

        #endregion

        public ActionResult AttendanceSubjectwise()
        {
            return View();
        }

        #region"AttendanceSubjectwise save"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveSubjectWiseAttendance()
        {
            TeacherLogin.Models.Responce dataColl = new TeacherLogin.Models.Responce();
            // List<TeacherLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = DeserializeObject<List<TeacherLogin.Models.Teacher.SubjectWiseAttendance> >(Request["jsonData"]);
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/SaveSubjectWiseAttendance", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ExamSetup err = new TeacherLogin.Models.Teacher.ExamSetup();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }


        #endregion
        #region"SubjectWiseAttendance"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAllSubjectWiseAttendance(TeacherLogin.Models.Teacher.SubjectWiseAttendance Data)
        {

            List<TeacherLogin.Models.Teacher.SubjectWiseAttendance> dataColl = new List<TeacherLogin.Models.Teacher.SubjectWiseAttendance>();
            try
            {

                if (Data.sectionId == null)
                {
                    Data.sectionId = 0;  // Default value for null sectionId, assuming it's an integer
                }
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetSubjectWiseAttendance", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);

                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.SubjectWiseAttendance>>(Data, keyValues);


                //var responseData = request.Execute<TeacherLogin.Models.Teachers.SubjectWiseAttendance>();
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.SubjectWiseAttendance>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.SubjectWiseAttendance err = new TeacherLogin.Models.Teacher.SubjectWiseAttendance();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion


    }
}