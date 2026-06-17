using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Controllers
{
    public class GlobalController : TeacherLogin.Controllers.BaseController
    {
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetAllExamTypeList()
        {
            AcademicLib.BE.Exam.Creation.ExamTypeCollections dataColl = new AcademicLib.BE.Exam.Creation.ExamTypeCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetExamTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {

                };
                var responseData = request.Execute<AcademicLib.BE.Exam.Creation.ExamTypeCollections>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((AcademicLib.BE.Exam.Creation.ExamTypeCollections)responseData);
                }
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetAllClassList()
        {
            AcademicLib.BE.Academic.Creation.ClassSectionList dataColl = new AcademicLib.BE.Academic.Creation.ClassSectionList();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetClassSectionList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {

                };
                var responseData = request.Execute<AcademicLib.BE.Academic.Creation.ClassSectionList>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((AcademicLib.BE.Academic.Creation.ClassSectionList)responseData);
                }
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetAllCASTypeList()
        {
            AcademicLib.BE.Exam.Creation.CASTypeCollections dataColl = new AcademicLib.BE.Exam.Creation.CASTypeCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetCASTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {

                };
                var responseData = request.Execute<AcademicLib.BE.Exam.Creation.CASTypeCollections>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((AcademicLib.BE.Exam.Creation.CASTypeCollections)responseData);
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
        public TeacherLogin.Controllers.JsonNetResult GetAllSubjectList(int? ClassId,int? SectionId)
        {
            AcademicLib.BE.Academic.Creation.SubjectCollections dataColl = new AcademicLib.BE.Academic.Creation.SubjectCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetSubjectList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                bool forAllSubject = false;

                if ((!ClassId.HasValue || ClassId == 0) && (!SectionId.HasValue || SectionId == 0))
                    forAllSubject = true;

                var para = new
                {
                    classId=(ClassId.HasValue ? ClassId : 0),
                    sectionId= (SectionId.HasValue ? SectionId: 0),
                    forAllSubject= forAllSubject
                };
                var responseData = request.Execute<AcademicLib.BE.Academic.Creation.SubjectCollections>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((AcademicLib.BE.Academic.Creation.SubjectCollections)responseData);
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
        public TeacherLogin.Controllers.JsonNetResult GetSubjectMappingClassWise(int? ClassId, string SectionIdColl)
        {
            AcademicLib.BE.Academic.Creation.SubjectCollections dataColl = new AcademicLib.BE.Academic.Creation.SubjectCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetSubjectMapping", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
             
                var para = new
                {
                    classId = (ClassId.HasValue ? ClassId : 0),
                    SectionIdColl = SectionIdColl 
                };
                var responseData = request.Execute<AcademicLib.BE.Academic.Creation.SubjectCollections>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((AcademicLib.BE.Academic.Creation.SubjectCollections)responseData);
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
        public TeacherLogin.Controllers.JsonNetResult GetSubjectTeacher(int ClassId,int? SectionId,int? SubjectId)
        {
            AcademicLib.BE.Academic.Transaction.EmployeeUserCollections dataColl = new AcademicLib.BE.Academic.Transaction.EmployeeUserCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetEmpListForClassTeacher", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);

                var para = new
                {
                    classId = ClassId,
                    sectionId= SectionId,
                    subjectId = SubjectId
                };
                var responseData = request.Execute<AcademicLib.BE.Academic.Transaction.EmployeeUserCollections>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((AcademicLib.BE.Academic.Transaction.EmployeeUserCollections)responseData);
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