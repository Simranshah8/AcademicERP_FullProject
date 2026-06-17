using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using Newtonsoft.Json;
using PivotalERP.Models;
using AcademicLib.BE.Global;
namespace PivotalERP.Areas.Exam.Controllers
{
    public class ReportController : PivotalERP.Controllers.BaseController
    {

        public ActionResult RdlcMarkSheet()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Tabulation, false)]
        public ActionResult CASTabulation()
        {
            return View();
        }
        public ActionResult RdlCASTabulation()
        {
            return View();
        }

        // GET: Exam/Report
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Tabulation, false)]
        public ActionResult Tabulation()
        {            
            return View();
        }
        public ActionResult RldTabulation()
        {
            return View();
        }
        public ActionResult RdlReTabulation()
        {
            return View();
        }
        public ActionResult RdlGroupTabulation()
        {
            return View();
        }
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Marksheet, false)]
        public ActionResult MarkSheet()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ExamAnalysis, false)]
        public ActionResult Analysis()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult PublishExamResult(int ExamTypeId,string ClassIdColl, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).PublishedExamResult(this.AcademicYearId, ExamTypeId,ClassIdColl, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PublishGroupExamResult(int ExamTypeGroupId,int? CurExamTypeId, string ClassIdColl)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).PublishedGroupExamResult(this.AcademicYearId, ExamTypeGroupId,CurExamTypeId, ClassIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetExamResultSummary(int ExamTypeId,int? BranchId=null)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getExamResultSummary(this.AcademicYearId, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetTeaherWiseSubjectAnalysis(int? ExamTypeId,int? ExamTypeGroupId,int? BranchId=null)
        {

            if (!ExamTypeId.HasValue)
                ExamTypeId = 0;

            if (!ExamTypeGroupId.HasValue)
                ExamTypeGroupId = 0;

            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getTeacherWiseSubjectAnalysis(this.AcademicYearId, ExamTypeId.Value,ExamTypeGroupId.Value,BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamGroupResultSummary(int ExamTypeGroupId,int? BranchId=null)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getExamGroupResultSummary(this.AcademicYearId, ExamTypeGroupId,BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentListForExam(int ClassId, int? SectionId, int ExamTypeId,int? SubjectId,bool FilterSection)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getStudentListForExam(this.AcademicYearId, ClassId, SectionId, ExamTypeId,SubjectId,FilterSection);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentListForReExam(int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId, int? SubjectId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getStudentListForReExam(this.AcademicYearId, ClassId, SectionId, ExamTypeId,ReExamTypeId, SubjectId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonNetResult PrintExamType()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Exam.MarkSheet> paraData = DeserializeObject<List<AcademicLib.RE.Exam.MarkSheet>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonNetResult PrintExamTypeGroup()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Exam.MarkSheet> paraData = DeserializeObject<List<AcademicLib.RE.Exam.MarkSheet>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PrintStudentListForExam()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Exam.StudentForExam> paraData = DeserializeObject<List<AcademicLib.RE.Exam.StudentForExam>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentEvaluation(int ClassId, int SectionId, int ExamTypeId, string ExamTypeIdColl = null, string StudentIdColl = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            AcademicLib.BE.Exam.Transaction.StudentEvalCollections dataColl = new AcademicLib.BE.Exam.Transaction.StudentEvalCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.StudentEval(User.UserId, User.HostName, User.DBName).GetStudentEvaluation(0, this.AcademicYearId, ClassId, SectionId, ExamTypeId, ExamTypeIdColl, StudentIdColl, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "Exam Dashboard"
        public ActionResult ExamDashboard()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetDashboardData()
        {
            var dataColl = new AcademicLib.BL.Exam.Reporting.ExamDashboard(User.UserId, User.HostName, User.DBName).GetExamDashboardData(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion
        public ActionResult MasterReport()
        {
            return View();
        }
        public ActionResult ViewLedger()
        {
            return View();
        }
        public ActionResult ClassWiseAnalysis()
        {
            return View();
        }
        public ActionResult IDCardTemplate()
        {
            return View();
        }

        public ActionResult ExamMarkLedgerReport()
        {
            return View();
        }
        public ActionResult ICExamMarkLedgerReport()
        {
            return View();
        }
        public ActionResult ICGradeSheet()
        {
            return View();
        }
        public ActionResult ThemewiseGradeSheet()
        {
            return View();
        }

        // GET: Exam/Reporting
        #region "Exam Analysis"
        public ActionResult ExamAnalysis()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetExamAnalysisData()
        {
            var dataColl = new AcademicLib.BL.Exam.Reporting.ExamAnalysis(User.UserId, User.HostName, User.DBName).GetExamAnalysisData(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion
    }
}