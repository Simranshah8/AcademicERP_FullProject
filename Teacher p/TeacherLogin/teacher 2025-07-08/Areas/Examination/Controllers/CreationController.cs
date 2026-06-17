using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.Examination.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {

        public ActionResult ClassTeacherRemarks()
        {
            return View();
        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentForExamComment(TeacherLogin.Models.Teacher.MarkEntryVal W)
        {

            TeacherLogin.Models.Teacher.ExamWiseCommentCollections dataColl = new Models.Teacher.ExamWiseCommentCollections();
            try
            { 
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetStudentForExamComment", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.DynamicObject<TeacherLogin.Models.Teacher.ExamWiseCommentCollections>>(W, keyValues);

                if (responseData != null)
                {
                    dataColl =  ((TeacherLogin.Models.Teacher.DynamicObject<TeacherLogin.Models.Teacher.ExamWiseCommentCollections>)responseData).DataColl;
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveExamComment(TeacherLogin.Models.Teacher.ExamWiseCommentCollections dataColl)
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/UpdateExamComment", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(dataColl, keyValues);


                if (responseData != null)
                {
                    try
                    {
                        resVal = ((TeacherLogin.Models.Responce)responseData);
                    }
                    catch 
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = responseData.ToString();
                    }
                    
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }

        // GET: Examination/Creation
        public ActionResult ExamAttendance()
        {
            return View();
        }

      


        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentForExamBA(TeacherLogin.Models.Teacher.MarkEntryVal W)
        {

            List<TeacherLogin.Models.Teacher.StudentForExamAttendance> dataColl = new List<Models.Teacher.StudentForExamAttendance>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetStudentForExamAttendance", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.StudentForExamAttendance>>(W, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.StudentForExamAttendance>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveExamBA(TeacherLogin.Models.Teacher.StudentForExamAttendanceCollections dataColl)
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/AddExamWiseAttendance", "POST");
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
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }


        public ActionResult MarksEntry()
        {
            return View();
        }

        #region"GetSubjectList"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentForSubjectME(TeacherLogin.Models.Teacher.MarkEntryVal W)
        {

            List<TeacherLogin.Models.Teacher.StudentForMarkEntry> dataColl = new List<Models.Teacher.StudentForMarkEntry>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetStudentForSubjectME", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.StudentForMarkEntry>>(W, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.StudentForMarkEntry>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveSubjectME(TeacherLogin.Models.Teacher.MarkEntryCollections dataColl)
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/AddMarkEntry", "POST");
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
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }


        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetSubjectColl()
        {
            TeacherLogin.Models.Subject data = new TeacherLogin.Models.Subject();
            List<TeacherLogin.Models.Subject> dataColl = new List<TeacherLogin.Models.Subject>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetSubjectList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Subject>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Subject>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion



        public ActionResult ClassTest()
        {
            return View();
        }
        public ActionResult CASMarkEntry()
        {
            return View();
        }
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetCASTypeList()
        {
            TeacherLogin.Models.ExamType data = new TeacherLogin.Models.ExamType();
            List<TeacherLogin.Models.Teacher.CASType> dataColl = new List<TeacherLogin.Models.Teacher.CASType>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetCASTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.CASType>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.CASType>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentForClassWiseCASExamME(int ClassId, int? SectionId, int SubjectId, int ExamTypeId, bool FilterSection)
        {

            AcademicLib.RE.Exam.StudentForCASExamTypeMarkEntryCollections dataColl = new AcademicLib.RE.Exam.StudentForCASExamTypeMarkEntryCollections();
            try
            {

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetStudentForCASME", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                { 
                    classId = ClassId,
                    sectionId = (SectionId.HasValue ? SectionId.Value : 0),
                    subjectId = SubjectId, 
                    examTypeId=ExamTypeId,
                    filterSection=FilterSection
                };
                var responseData = request.Execute<AcademicLib.RE.Exam.StudentForCASExamTypeMarkEntryCollections>(para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((AcademicLib.RE.Exam.StudentForCASExamTypeMarkEntryCollections)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentForCASE(int ClassId, int? SectionId, int SubjectId, int CASTypeId, bool FilterSection, DateTime ExamDate)
        {

            AcademicLib.RE.Exam.StudentForCASMarkEntryCollections dataColl = new AcademicLib.RE.Exam.StudentForCASMarkEntryCollections();
            try
            {

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetStudentForCASME", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    casTypeId = CASTypeId,
                    classId = ClassId,
                    sectionId = (SectionId.HasValue ? SectionId.Value : 0),
                    subjectId=SubjectId,
                    examDate=ExamDate
                };
                var responseData = request.Execute<AcademicLib.RE.Exam.StudentForCASMarkEntryCollections> (para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((AcademicLib.RE.Exam.StudentForCASMarkEntryCollections)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetCASMarkEntrySubjectSummary(int ClassId, int? SectionId, bool FilterSection, DateTime DateFrom, DateTime DateTo)
        {
            AcademicLib.RE.Exam.CASMarkEntrySubjectCollections dataColl = new AcademicLib.RE.Exam.CASMarkEntrySubjectCollections();
            try
            {

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetCASMarkEntrySubjectSummary", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {                    
                    classId = ClassId,
                    sectionId = (SectionId.HasValue ? SectionId.Value : 0),
                    filterSection=FilterSection,
                    dateFrom = DateFrom,
                    dateTo=DateTo
                };
                var responseData = request.Execute<AcademicLib.RE.Exam.CASMarkEntrySubjectCollections>(para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((AcademicLib.RE.Exam.CASMarkEntrySubjectCollections)responseData);

                }
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetCASMarkEntrySummary(int ClassId, int? SectionId, bool FilterSection, int SubjectId, DateTime DateFrom, DateTime DateTo)
        {
            AcademicLib.RE.Exam.CASMarkEntryCollections dataColl = new AcademicLib.RE.Exam.CASMarkEntryCollections();
            try
            {

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetCASMarkEntrySummary", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    classId = ClassId,
                    sectionId = (SectionId.HasValue ? SectionId.Value : 0),
                    filterSection = FilterSection,
                    dateFrom = DateFrom,
                    dateTo = DateTo,
                    subjectId=SubjectId
                };
                var responseData = request.Execute<AcademicLib.RE.Exam.CASMarkEntryCollections>(para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((AcademicLib.RE.Exam.CASMarkEntryCollections)responseData);

                }
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }
        public ActionResult ExamTypeMarkEntry()
        {
            return View();
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentForCASME(TeacherLogin.Models.Teacher.MarkEntryVal W)
        {

            List<TeacherLogin.Models.Teacher.StudentForCASExamTypeMarkEntry> dataColl = new List<Models.Teacher.StudentForCASExamTypeMarkEntry>();
            try
            {

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetStudentForCASME", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.StudentForCASExamTypeMarkEntry>>(W, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.StudentForCASExamTypeMarkEntry>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveCASExamWiseME()
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                var dataColl = DeserializeObject<AcademicLib.RE.Exam.StudentForCASMarkEntryCollections>(Request["jsonData"]);
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddCASMarkEntry", "POST");
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
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }

        #region"Student Achievement"
        public ActionResult StudentAchievement()
        {
            return View();
        }

        [HttpPost]

        public TeacherLogin.Controllers.JsonNetResult SaveStudentAchievement()
        {
            TeacherLogin.Models.Responce dataColl = new TeacherLogin.Models.Responce();
            try
            {
                 
                var beData = DeserializeObject<List<TeacherLogin.Models.Teacher.StudentAchievement>>(Request["jsonData"]);
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddAchievmentToStudent", "POST");
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
                TeacherLogin.Models.Teacher.StudentAchievement err = new TeacherLogin.Models.Teacher.StudentAchievement();
            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }


        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetPrevAchievement(int studentId, int examTypeId)
        {
            List<TeacherLogin.Models.Teacher.PrevAchievement> dataColl = new List<TeacherLogin.Models.Teacher.PrevAchievement>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetStudentAchievment", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    studentId = studentId,
                    examTypeId = examTypeId
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.PrevAchievement>>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.PrevAchievement>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.PrevAchievement err = new TeacherLogin.Models.Teacher.PrevAchievement();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion

        #region "Heigh & Weight"

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

    }
}