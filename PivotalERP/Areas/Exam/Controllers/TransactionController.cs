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

namespace PivotalERP.Areas.Exam.Controllers
{
    public class TransactionController : PivotalERP.Controllers.BaseController
    {
        // GET: Exam/Transaction

        #region "ExamType"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.ExamType, false)]
        public ActionResult ExamType()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveExamType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.ExamType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ExamTypeId.HasValue)
                        beData.ExamTypeId = 0;

                    resVal = new AcademicLib.BL.Exam.Creation.ExamType(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId,beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllExamTypeList()
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ExamType(User.UserId, User.HostName, User.DBName).GetAllExamType(this.AcademicYearId, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamTypeById(int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ExamType(User.UserId, User.HostName, User.DBName).GetExamTypeById(0, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelExamType(int ExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.ExamType(User.UserId, User.HostName, User.DBName).DeleteById(0, ExamTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "ReExamType"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ExamType, false)]
        public ActionResult ReExamType()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveReExamType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.ReExamType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ReExamTypeId.HasValue)
                        beData.ReExamTypeId = 0;

                    resVal = new AcademicLib.BL.Exam.Creation.ReExamType(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllReExamTypeList()
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ReExamType(User.UserId, User.HostName, User.DBName).GetAllReExamType(this.AcademicYearId, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetReExamTypeById(int ReExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ReExamType(User.UserId, User.HostName, User.DBName).GetReExamTypeById(0, ReExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelReExamType(int ReExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.ReExamType(User.UserId, User.HostName, User.DBName).DeleteById(0, ReExamTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "ExamTypeGroup"

        [HttpPost]
        public JsonNetResult SaveExamTypeGroup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.ExamTypeGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ExamTypeGroupId.HasValue)
                        beData.ExamTypeGroupId = 0;

                    resVal = new AcademicLib.BL.Exam.Creation.ExamTypeGroup(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "ExamType Group";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllExamTypeGroupList()
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ExamTypeGroup(User.UserId, User.HostName, User.DBName).GetAllExamTypeGroup(this.AcademicYearId,0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamTypeGroupById(int ExamTypeGroupId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ExamTypeGroup(User.UserId, User.HostName, User.DBName).GetExamTypeGroupById(0, ExamTypeGroupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelExamTypeGroup(int ExamTypeGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.ExamTypeGroup(User.UserId, User.HostName, User.DBName).DeleteById(0, ExamTypeGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "ParentExamTypeGroup"

        [HttpPost]
        public JsonNetResult SaveParentExamTypeGroup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.ParentExamTypeGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Exam.Creation.ParentExamTypeGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllParentExamTypeGroupList()
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ParentExamTypeGroup(User.UserId, User.HostName, User.DBName).GetAllParentExamTypeGroup(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetParentExamTypeGroupById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ParentExamTypeGroup(User.UserId, User.HostName, User.DBName).GetParentExamTypeGroupById(0, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelParentExamTypeGroup(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.ParentExamTypeGroup(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.DivisionGrade, false)]
        public ActionResult GradeDivision()
        {
            return View();
        }
        #region "Division"

        [HttpPost]
        public JsonNetResult SaveDivision()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.Division>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.DivisionId.HasValue)
                        beData.DivisionId = 0;

                    resVal = new AcademicLib.BL.Exam.Creation.Division(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllDivisionList()
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.Division(User.UserId, User.HostName, User.DBName).GetAllDivision(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDivisionById(int DivisionId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.Division(User.UserId, User.HostName, User.DBName).GetDivisionById(0, DivisionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDivision(int DivisionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.Division(User.UserId, User.HostName, User.DBName).DeleteById(0, DivisionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Grade"

        [HttpPost]
        public JsonNetResult SaveGrade()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.Grade>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.GradeId.HasValue)
                        beData.GradeId = 0;

                    resVal = new AcademicLib.BL.Exam.Creation.Grade(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllGradeList()
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.Grade(User.UserId, User.HostName, User.DBName).GetAllGrade(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetGradeById(int GradeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.Grade(User.UserId, User.HostName, User.DBName).GetGradeById(0, GradeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelGrade(int GradeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.Grade(User.UserId, User.HostName, User.DBName).DeleteById(0, GradeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Comment Setup"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.CommentSetup, false)]
        public ActionResult CommentSetup()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonNetResult SaveCommentSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.CommentSetup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Exam.Transaction.CommentSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllCommentSetupList()
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CommentSetup(User.UserId, User.HostName, User.DBName).GetAllCommentSetup(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCommentSetupById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CommentSetup(User.UserId, User.HostName, User.DBName).GetCommentSetupById(0, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelCommentSetup(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.CommentSetup(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ResultSetup, false)]
        public ActionResult ResultSetup()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveExamTypeWiseResultSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamTypeWiseResultSetup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    beData.TranId = 0;
                    resVal = new AcademicLib.BL.Exam.Transaction.ExamTypeWiseResultSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }



            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetExamTypeWiseResultSetupById(int ClassId, string SectionIdColl, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamTypeWiseResultSetup(User.UserId, User.HostName, User.DBName).GetExamTypeWiseResultSetupByClassId(ClassId, SectionIdColl, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveExamGroupWiseResultSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamGroupWiseResultSetup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    beData.TranId = 0;
                    resVal = new AcademicLib.BL.Exam.Transaction.ExamGroupWiseResultSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }



            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetExamGroupWiseResultSetupById(int ClassId, string SectionIdColl, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamGroupWiseResultSetup(User.UserId, User.HostName, User.DBName).GetExamGroupWiseResultSetupByClassId(ClassId, SectionIdColl, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelExamTypeWiseResultSetup(int ClassId, string SectionIdColl, int ExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamTypeWiseResultSetup(User.UserId, User.HostName, User.DBName).DeleteById(0, 0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult DelExamTypeGroupWiseResultSetup(int ClassId, int ExamTypeId, string SectionIdColl = "")
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamGroupWiseResultSetup(User.UserId, User.HostName, User.DBName).DeleteExamTypeGroupWiseById(0, ClassId,ExamTypeId, SectionIdColl);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #region "Exam Schedule"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.ExamSchedule, false)]
        public ActionResult ExamSchedule()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveExamSchedule()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamSchedule>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ExamScheduleId.HasValue)
                        beData.ExamScheduleId = 0;

                    resVal = new AcademicLib.BL.Exam.Transaction.ExamSchedule(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllExamSchedule()
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSchedule(User.UserId, User.HostName, User.DBName).GetAllExamSchedule(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamScheduleStatus(int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSchedule(User.UserId, User.HostName, User.DBName).GetExamScheduleStatus(ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamScheduleById(int ClassId,string SectionIdColl,int ExamTypeId,int? SemesterId=null,int? ClassYearId=null,int? BatchId=null)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSchedule(User.UserId, User.HostName, User.DBName).getExamScheduleById(0, ClassId, SectionIdColl, ExamTypeId,SemesterId,ClassYearId,BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelExamSchedule(int ClassId, string SectionIdColl, int ExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamSchedule(User.UserId, User.HostName, User.DBName).DeleteById(0,ClassId,SectionIdColl,ExamTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        public ActionResult RdlAllClassExamSchedule()
        {
            return View();
        }



        #endregion


        #region "ExamWise SymbolNo"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SymbolNumber, false)]
        public ActionResult SymbolNumber()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveSymbolNumber()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections>(Request["jsonData"]);
                if (beData != null)
                {                    
                    resVal = new AcademicLib.BL.Exam.Transaction.ExamWiseSymbolNo(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSymbolNumberById(int ClassId, int? SectionId, int ExamTypeId,bool FilterSection=true)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamWiseSymbolNo(User.UserId, User.HostName, User.DBName).GetAllExamWiseSymbolNo(ClassId, SectionId, ExamTypeId, FilterSection);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult TransforSymbolNumber(int FromExamTypeId,int ToExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamWiseSymbolNo(User.UserId, User.HostName, User.DBName).Transfor(FromExamTypeId, ToExamTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult TransforMarkSetup(int FromExamTypeId, int ToExamTypeId, int? BranchId = null)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.MarksSetup(User.UserId, User.HostName, User.DBName).Transfor(FromExamTypeId, ToExamTypeId,BranchId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelSymbolNumber(int ClassId, int? SectionId, int ExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                //resVal = new AcademicLib.BL.Exam.Transaction.ExamWiseSymbolNo(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveHeightAndWeight()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.HeightAndWeightCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.HeightAndWeight(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }



        [HttpPost]
        public JsonNetResult GetHeightWeightById(int ClassId, int? SectionId, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.HeightAndWeight(User.UserId, User.HostName, User.DBName).getHeightWeightClassWise(this.AcademicYearId, ClassId, SectionId, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelHeightWeight(int ClassId, int? SectionId, int ExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                //resVal = new AcademicLib.BL.Exam.Transaction.ExamWiseSymbolNo(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveResultDispatch()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ResultDispatchCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.ResultDispatch(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }



        [HttpPost]
        public JsonNetResult GetResultDispatch(int ClassId, int? SectionId, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ResultDispatch(User.UserId, User.HostName, User.DBName).getResultDispatch(this.AcademicYearId, ClassId, SectionId, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        #endregion

        #region "ReExamWise SymbolNo"


        [HttpPost]
        public JsonNetResult SaveReSymbolNumber()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamWiseSymbolNoCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.ReExamWiseSymbolNo(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetReSymbolNumber(int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId,bool ForClassWise)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ReExamWiseSymbolNo(User.UserId, User.HostName, User.DBName).getStudentList(ForClassWise, ClassId, SectionId, ExamTypeId, ReExamTypeId,this.AcademicYearId);

            var retVal = new
            {
                StudentList = dataColl,
                SubjectList = dataColl.SubjectList,
                IsSuccess = dataColl.IsSuccess,
                ResponseMSG = dataColl.ResponseMSG
            };
            return new JsonNetResult() { Data = retVal, TotalCount = retVal.IsSuccess ? 1 : 0, IsSuccess = retVal.IsSuccess, ResponseMSG = retVal.ResponseMSG };
        }

      
        [HttpPost]
        public JsonNetResult DelReSymbolNumber(int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                //resVal = new AcademicLib.BL.Exam.Transaction.ExamWiseSymbolNo(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
         
        

        #endregion


        #region "MarkSetup"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Marksetp, false)]
        public ActionResult MarkSetup()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveMarkSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.MarksSetup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    beData.TranId = 0;
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }



            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
         

        [HttpPost]
        public JsonNetResult GetMarkSetupById(int ClassId, string SectionIdColl, int ExamTypeId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksSetup(User.UserId, User.HostName, User.DBName).GetMarksSetupByClassId( ClassId, SectionIdColl, ExamTypeId,SemesterId,ClassYearId,BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetMarkSetupStatus(int ExamTypeId,int? BranchId=null)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksSetup(User.UserId, User.HostName, User.DBName).GetMarkSetupStatus(ExamTypeId,BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelMarkSetup(int ClassId, string SectionIdColl, int ExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamSchedule(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassId, SectionIdColl, ExamTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

      
        #region "ExamWise Attendance"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.ExamAttendance, false)]
        public ActionResult Attendance()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveBulkAttendance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamwiseAttendenceCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.ExamAttendance(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Bulk Attendance";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBulkAttendanceById(int ClassId, int? SectionId, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamAttendance(User.UserId, User.HostName, User.DBName).getExamWiseAttendanceClassWise(this.AcademicYearId, ClassId, SectionId, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAttendanceForExam(int ClassId, int? SectionId, DateTime DateFrom,DateTime DateTo)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamAttendance(User.UserId, User.HostName, User.DBName).getAttendanceForExam( ClassId, SectionId, DateFrom,DateTo,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveExamWiseBlockedMarkSheet()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
            
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamWiseBlockMarkSheetCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    var first = beData[0];
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).SaveExamWiseBlockMarkSheet(this.AcademicYearId, first.ClassId,first.SectionId,first.ExamTypeId,beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "ExamWise Block MarkSheet";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamWiseBlockedMarkSheetById(int ClassId, int? SectionId, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getExamWiseBlockedMarksheet(this.AcademicYearId, ClassId, SectionId, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveExamGroupWiseBlockedMarkSheet()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamWiseBlockMarkSheetCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    var first = beData[0];
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).SaveExamGroupWiseBlockMarkSheet(this.AcademicYearId, first.ClassId, first.SectionId, first.ExamTypeId, beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamGroupWiseBlockedMarkSheetById(int ClassId, int? SectionId, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getExamGroupWiseBlockedMarksheet(this.AcademicYearId, ClassId, SectionId, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.MarkEntry, false)]
        public ActionResult MarkEntry()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveClassWiseMarkEntry()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.API.Teacher.MarkEntryCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).SaveMarkEntry(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "ClassWise MarkEntry";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentForClassWiseME(int ClassId, int? SectionId, int ExamTypeId,bool FilterSection,int? SemesterId=null,int? ClassYearId=null,int? BatchId=null,int? BranchId=null)
        {
            var markBL = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName);
            var dataColl = markBL.getStudentForMarkEntry(this.AcademicYearId, ClassId, SectionId, ExamTypeId,FilterSection,BranchId);

            if (dataColl.Count == 0)
            {
                markBL.resetSubjectMapping(this.AcademicYearId);
                dataColl = markBL.getStudentForMarkEntry(this.AcademicYearId, ClassId, SectionId, ExamTypeId, FilterSection,SemesterId,ClassYearId,BatchId,BranchId);
            }

            var query = from dc in dataColl
                        group dc by dc.StudentId  into g
                        select new
                        {
                            StudentId=g.Key,
                            AutoNumber=g.First().AutoNumber,
                            RollNo=g.First().RollNo,
                            Name=g.First().Name,
                            RegdNo=g.First().RegdNo,
                            BoardRegdNo=g.First().BoardRegdNo,
                            SymbolNo=g.First().SymbolNo,
                            PhotoPath=g.First().PhotoPath,
                            Remarks=g.First().Remarks,
                            ClassName=g.First().ClassName,
                            SectionName=g.First().SectionName,
                            MarkColl=g
                        };


            return new JsonNetResult() { Data = query, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentForStudentWiseME(int StudentId,int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getStudentForStudentWiseMarkEntry(this.AcademicYearId, StudentId, ExamTypeId);

            var query = from dc in dataColl
                        group dc by dc.StudentId into g
                        select new
                        {
                            StudentId = g.Key,
                            AutoNumber = g.First().AutoNumber,
                            RollNo = g.First().RollNo,
                            Name = g.First().Name,
                            RegdNo = g.First().RegdNo,
                            BoardRegdNo = g.First().BoardRegdNo,
                            SymbolNo = g.First().SymbolNo,
                            PhotoPath = g.First().PhotoPath,
                            FMTH=g.Sum(p1=>p1.FMTH),
                            FMPR = g.Sum(p1 => p1.FMPR),
                            PMTH = g.Sum(p1 => p1.PMTH),
                            PMPR = g.Sum(p1 => p1.PMPR),                            
                            MarkColl = g
                        };
            return new JsonNetResult() { Data = query, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetStudentForSubjectME(int ClassId, int? SectionId, int ExamTypeId, int SubjectId, bool FilterSection, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null, int? BranchId = null)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getStudentForMarkEntrySubWise(this.AcademicYearId, ClassId, SectionId, ExamTypeId, SubjectId, FilterSection, BatchId, SemesterId, ClassYearId, BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetMarkSubmit( int ExamTypeId,int? BranchId=null)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getMarkSubmit(this.AcademicYearId, ExamTypeId,BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "Re-Exam MarkEntry"


        [PermissionsAttribute(Actions.View, (int)ENTITIES.MarkEntry, false)]
        public ActionResult ReMarkEntry()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetStudentForClassWiseReME(int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getStudentForReMarkEntry(ClassId, SectionId, ExamTypeId,ReExamTypeId);

            var query = from dc in dataColl
                        group dc by dc.StudentId into g
                        select new
                        {
                            StudentId = g.Key,
                            AutoNumber = g.First().AutoNumber,
                            RollNo = g.First().RollNo,
                            Name = g.First().Name,
                            RegdNo = g.First().RegdNo,
                            BoardRegdNo = g.First().BoardRegdNo,
                            SymbolNo = g.First().SymbolNo,
                            PhotoPath = g.First().PhotoPath,
                            Remarks = g.First().Remarks,
                            MarkColl = g
                        };
            return new JsonNetResult() { Data = query, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveClassWiseReMarkEntry()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.API.Teacher.MarkEntryCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).SaveReMarkEntry(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentForSubjectReME(int ClassId, int? SectionId, int ExamTypeId,int ReExamTypeId, int SubjectId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getStudentForReMarkEntry(ClassId, SectionId, ExamTypeId,ReExamTypeId, SubjectId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentForStudentWiseReME(int StudentId, int ExamTypeId,int ReExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(User.UserId, User.HostName, User.DBName).getStudentForStudentWiseReMarkEntry(StudentId, ExamTypeId,ReExamTypeId,this.AcademicYearId);

            var query = from dc in dataColl
                        group dc by dc.StudentId into g
                        select new
                        {
                            StudentId = g.Key,
                            AutoNumber = g.First().AutoNumber,
                            RollNo = g.First().RollNo,
                            Name = g.First().Name,
                            RegdNo = g.First().RegdNo,
                            BoardRegdNo = g.First().BoardRegdNo,
                            SymbolNo = g.First().SymbolNo,
                            PhotoPath = g.First().PhotoPath,
                            FMTH = g.Sum(p1 => p1.FMTH),
                            FMPR = g.Sum(p1 => p1.FMPR),
                            PMTH = g.Sum(p1 => p1.PMTH),
                            PMPR = g.Sum(p1 => p1.PMPR),
                            MarkColl = g
                        };
            return new JsonNetResult() { Data = query, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "Exam Configuration"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ExamSetup, false)]
        public ActionResult ExamSetup()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveExamConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Setup.Configuration>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Exam.Setup.Configuration(User.UserId, User.HostName, User.DBName).SaveFormData(beData,User.BranchId);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamConfiguration()
        {
            var dataColl = new AcademicLib.BL.Exam.Setup.Configuration(User.UserId, User.HostName, User.DBName).GetConfiguration(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveSeatPlanConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<List<AcademicLib.BE.Exam.Setup.SeatPlanConfiguraion>>(Request["jsonData"]);
                if (beData != null)
                { 
                    resVal = new AcademicLib.BL.Exam.Setup.Configuration(User.UserId, User.HostName, User.DBName).SaveSeatPlanConfiguration(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSeatPlanConfiguration()
        {
            var dataColl = new AcademicLib.BL.Exam.Setup.Configuration(User.UserId, User.HostName, User.DBName).getSeatPlanConfiguration();

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        [HttpPost]
        public JsonNetResult SaveExamWiseTemplate()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.ExamTypeWiseTemplateCollections>(Request["jsonData"]);
                if (beData != null)
                {                    
                    resVal = new AcademicLib.BL.Exam.Creation.ExamTypeWiseTemplate(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamTypeWiseTemplate(int? ExamTypeId,int? ExamTypeGroupId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.ExamTypeWiseTemplate(User.UserId, User.HostName, User.DBName).getExamTypeWiseTemplate(ExamTypeId, ExamTypeGroupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion



        [PermissionsAttribute(Actions.View, (int)ENTITIES.ClassTest, false)]
        public ActionResult ClassTest()
        {
            return View();
        }

        #region "ClassTestMarkEntry"
        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.WebFeaturesSetup)]
        public JsonNetResult SaveUpdateClassTestMarkEntry()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ClassTestMarkEntryCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Exam.Transaction.ClassTestMarkEntry(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCSubjectWiseLesson(int? SubjectId)
        {
            AcademicLib.BE.Exam.Transaction.CSubjectWiseLessonCollections dataColl = new AcademicLib.BE.Exam.Transaction.CSubjectWiseLessonCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ClassTestMarkEntry(User.UserId, User.HostName, User.DBName).GetCSubjectWiseLesson(0, SubjectId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentsDetailsClassWise(int ClassId, int? SectionId, int SubjectId, bool FilterSection, int? LessonSNo, DateTime TestDate, int EmployeeId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.StudentsDetailsClassWiseCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ClassTestMarkEntry(User.UserId, User.HostName, User.DBName).GetStudentsDetailsClassWise(0, this.AcademicYearId, ClassId, SectionId, SubjectId, FilterSection, LessonSNo, TestDate, EmployeeId, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        [PermissionsAttribute(Actions.View, (int)ENTITIES.CAS, false)]
        public ActionResult CAS()
        {
            return View();
        }


        #region "Exam Seat Plan"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SeatPlan, false)]
        public ActionResult ExamSeatPlan()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SeatPlan, false)]
        public JsonNetResult SaveExamRoom()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamRoom>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.RoomId.HasValue)
                        beData.RoomId = 0;

                    resVal = new AcademicLib.BL.Exam.Transaction.ExamRoom(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllExamSeatPlan()
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSeatPlan(User.UserId, User.HostName, User.DBName).GetAllExamSeatPlan(this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentExamSeatPlanForSMS(int ExamShiftId, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSeatPlan(User.UserId, User.HostName, User.DBName).GetStudentListForSMS(this.AcademicYearId, ExamTypeId, ExamShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamRoomList(int ExamShiftId,int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSeatPlan(User.UserId, User.HostName, User.DBName).GetRoomList(this.AcademicYearId, ExamShiftId,ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GeRoomSeatDetails(int ExamShiftId, int ExamTypeId,int RoomId,int SeatPlanAs)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSeatPlan(User.UserId, User.HostName, User.DBName).GetRoomSeatDetails(this.AcademicYearId, ExamShiftId, ExamTypeId,RoomId,SeatPlanAs);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllExamRoomList()
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamRoom(User.UserId, User.HostName, User.DBName).GetAllExamRoom(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassSectionForSeatPlan(int ExamShiftId,int ExamTypeId,int RoomId,int SeatPlanAs)
        {
           AcademicLib.BE.Exam.Transaction.StudentForSeatPlanCollections studentColl = new AcademicLib.BE.Exam.Transaction.StudentForSeatPlanCollections();
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSeatPlan(User.UserId, User.HostName, User.DBName).GetClassSectionList(this.AcademicYearId, ExamShiftId,ExamTypeId,RoomId,SeatPlanAs, ref studentColl);

            var retVal = new
            {
                ClassList=dataColl,
                StudentList=studentColl
            };

            return new JsonNetResult() { Data = retVal, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SaveSeatPlan(AcademicLib.BE.Exam.Transaction.SeatDetailsCollections dataColl)
        {
            var resVal = new AcademicLib.BL.Exam.Transaction.ExamSeatPlan(User.UserId, User.HostName, User.DBName).SaveSeatDetails(dataColl);

            return new JsonNetResult() { Data = resVal, TotalCount = resVal.IsSuccess ? 1 : 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetExamRoomById(int RoomId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamRoom(User.UserId, User.HostName, User.DBName).GetExamRoomById(0, RoomId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelExamRoom(int RoomId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamRoom(User.UserId, User.HostName, User.DBName).DeleteById(0, RoomId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveExamShift()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamShift>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ExamShiftId.HasValue)
                        beData.ExamShiftId = 0;

                    resVal = new AcademicLib.BL.Exam.Transaction.ExamShift(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllExamShiftList()
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamShift(User.UserId, User.HostName, User.DBName).GetAllExamShift(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetExamShiftById(int ExamShiftId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamShift(User.UserId, User.HostName, User.DBName).GetExamShiftById(0, ExamShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelExamShift(int ExamShiftId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamShift(User.UserId, User.HostName, User.DBName).DeleteById(0, ExamShiftId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GenerateSeatPlan(AcademicLib.BE.Exam.Transaction.GenerateSeatPlan plan)
        {
            plan.CUserId = User.UserId;
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSeatPlan(User.UserId, User.HostName, User.DBName).GenerateSeatPlan(this.AcademicYearId, plan);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelSeatPlan(int ExamShiftId,int ExamTypeId,int RoomId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamSeatPlan(User.UserId, User.HostName, User.DBName).DeleteSeatPlan(ExamShiftId,ExamTypeId,RoomId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult RdlExamSeatPlan()
        {
            return View();
        }

        #endregion

        #region "CAS System"

        public ActionResult CASType()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveCASType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.CASType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.CASTypeId.HasValue)
                        beData.CASTypeId = 0;

                    resVal = new AcademicLib.BL.Exam.Transaction.CASType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllCASTypeList()
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASType(User.UserId, User.HostName, User.DBName).GetAllCASType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllCASTypeForEdit()
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASType(User.UserId, User.HostName, User.DBName).GetAllCASType(0,null,null,null,null,true);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCASTypeById(int CASTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASType(User.UserId, User.HostName, User.DBName).GetCASTypeById(0, CASTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelCASType(int CASTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.CASType(User.UserId, User.HostName, User.DBName).DeleteById(0, CASTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveCASMarkSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.CASMarksSetup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    beData.TranId = 0;
                    resVal = new AcademicLib.BL.Exam.Transaction.CASMarkSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }



            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetExamClassSubject(int? ClassId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkSetup(User.UserId, User.HostName, User.DBName).getExamClassSubjectList(ClassId,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? dataColl.Count : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCASMarkSetupById(int ClassId,int? SectionId,int SubjectId, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkSetup(User.UserId, User.HostName, User.DBName).GetMarksSetupByClassId(ClassId, SectionId,SubjectId, ExamTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetCASMarkSetupStatus(int ExamTypeId,int ClassId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkSetup(User.UserId, User.HostName, User.DBName).GetMarkSetupStatus(ExamTypeId,ClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult CASMarkEntry()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetStudentForClassWiseCASME(int ClassId, int? SectionId,int SubjectId, int CASTypeId, bool FilterSection,DateTime ExamDate)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(User.UserId, User.HostName, User.DBName).getStudentForMarkEntry(ClassId, SectionId, FilterSection, SubjectId, CASTypeId, ExamDate,this.AcademicYearId);
                        
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SaveCASMarkEntry()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.RE.Exam.StudentForCASMarkEntryCollections>(Request["jsonData"]);
                if (beData != null)
                { 
                    resVal = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCASMarkEntrySummary(int ClassId, int? SectionId, bool FilterSection,int SubjectId, DateTime DateFrom,DateTime DateTo)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(User.UserId, User.HostName, User.DBName).getMarkEntrySummary(ClassId, SectionId, FilterSection,SubjectId,DateFrom,DateTo,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetCASMarkEntrySubjectSummary(int? ClassId, int? SectionId, bool FilterSection, DateTime DateFrom, DateTime DateTo,int? CASTypeId=null,int? SubjectId=null)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(User.UserId, User.HostName, User.DBName).getMarkEntrySubjectSummary(ClassId, SectionId, FilterSection, DateFrom, DateTo,this.AcademicYearId,CASTypeId,SubjectId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult ExamTypeMarkEntry()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetStudentForClassWiseCASExamME(int ClassId, int? SectionId, int SubjectId, int ExamTypeId, bool FilterSection )
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(User.UserId, User.HostName, User.DBName).getStudentForExamTypeMarkEntry(ClassId, SectionId, FilterSection, SubjectId, ExamTypeId,null,AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        public ActionResult ICMarkEntry()
        {
            return View();
        }
        public ActionResult ICTabulation()
        {
            return View();
        }
        public ActionResult ICMarksheet()
        {
            return View();
        }
         

        public ActionResult Indicator()
        {
            return View();
        }
        public ActionResult ICMarksEntry()
        {
            return View();
        }

        #region "Indicator"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.Indicator)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Indicator)]
        public JsonNetResult SaveUpdateIndicator()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.Indicator>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Exam.Transaction.Indicator(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllIndicator(int ClassId, int SubjectId, int? LessonId, string TopicName, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Exam.Transaction.IndicatorCollections dataColl = new AcademicLib.BE.Exam.Transaction.IndicatorCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.Indicator(User.UserId, User.HostName, User.DBName).GetAllIndicator(0, ClassId, SubjectId, LessonId, TopicName, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "ICMarkEntry"
        [HttpPost]
        public JsonNetResult GetSubjectLessonWise(int ClassId, int SubjectId)
        {
            AcademicLib.BE.Exam.Transaction.SubjectLessonWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.SubjectLessonWiseCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.Indicator(User.UserId, User.HostName, User.DBName).GetSubjectLessonWise(0, ClassId, SubjectId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetLessonTopicDetailsWise(int? LessonId)
        {
            AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.LessonTopicDetailsWiseCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.Indicator(User.UserId, User.HostName, User.DBName).GetLessonTopicDetailsWise(0, LessonId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTopicWiseIndicator(int? LessonId, string TopicName)
        {
            AcademicLib.BE.Exam.Transaction.TopicWiseIndicatorCollections dataColl = new AcademicLib.BE.Exam.Transaction.TopicWiseIndicatorCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.Indicator(User.UserId, User.HostName, User.DBName).GetTopicWiseIndicator(LessonId, TopicName);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        //Code for ICMarkEntry here start

        [HttpPost]
        //[PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)] 
        public JsonNetResult SaveUpdateICMarkSubject()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ICMarkEntryCollections>(Request["dataToSave"]);
                if (beData != null)
                {
                    //beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Exam.Transaction.ICMarkEntry(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetIStudentsDetailsSubjectsWise(int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, string TopicName, int? AssessmentTypeId, int? CFAssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Exam.Transaction.ICStudentsDetailCollections dataColl = new AcademicLib.BE.Exam.Transaction.ICStudentsDetailCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ICMarkEntry(User.UserId, User.HostName, User.DBName).GetIStudentsDetailsSubjectsWise(0, this.AcademicYearId, ClassId, SectionId, FilterSection, SubjectId, LessonId, TopicName, AssessmentTypeId, CFAssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTopicForStudentWiseIC(int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, int StudentId, int? AssessmentTypeId, int? CFAssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Exam.Transaction.TopicForStudentWiseICCollections dataColl = new AcademicLib.BE.Exam.Transaction.TopicForStudentWiseICCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ICMarkEntry(User.UserId, User.HostName, User.DBName).GetTopicForStudentWiseIC(0, this.AcademicYearId, ClassId, SectionId, FilterSection, SubjectId, LessonId, StudentId, AssessmentTypeId, CFAssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetICMArkEntryStatus(int ClassId, int? SectionId, int SubjectId, int LessonId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Exam.Transaction.ICMArkEntryStatusCCollections dataColl = new AcademicLib.BE.Exam.Transaction.ICMArkEntryStatusCCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ICMarkEntry(User.UserId, User.HostName, User.DBName).GetICMArkSubmitStatus(ClassId, SectionId, SubjectId, LessonId, this.AcademicYearId, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        //Code for ICMarkEntry here end

        //NEw code added by bibek for IndicatorSummary
        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.Vaccine)]
        public JsonNetResult GetIndicatorSummary(int? ClassId, int? SubjectId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.Indicator(User.UserId, User.HostName, User.DBName).GetIndicatorSummary(0, ClassId, SubjectId, BatchId, SemesterId, ClassYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "StudentAchievement"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.StudentAchievement, false)]
        public ActionResult StudentAchievement()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetStudentForAchievement(int ClassId, int? SectionId)
        {
            AcademicLib.BE.Exam.Transaction.StudentListForAchievementCollections dataColl = new AcademicLib.BE.Exam.Transaction.StudentListForAchievementCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.StudentAchievement(User.UserId, User.HostName, User.DBName).GetStudentForAchievement(ClassId, SectionId, this.AcademicYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.StudentAchievement, false)]
        public JsonNetResult SaveStudentAchievement()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.StudentAchievementCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Exam.Transaction.StudentAchievement(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

 

        [HttpPost]
        public JsonNetResult GetPrevAchievement(int StudentId, int ExamTypeId/*, int RemarksTypeId*/)
        {
            AcademicLib.BE.Exam.Transaction.PrevAchievementCollections dataColl = new AcademicLib.BE.Exam.Transaction.PrevAchievementCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.StudentAchievement(User.UserId, User.HostName, User.DBName).GetPreviousAchievement(0, StudentId, ExamTypeId/*, RemarksTypeId*/);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        #region "ClassTestMarkEntry Summary"
        [HttpPost]
        public JsonNetResult GetClassSummarySubjectClassWise(int? ClassId)
        {
            AcademicLib.BE.Exam.Transaction.ClassSummarySubjectClassWiseCollections dataColl = new AcademicLib.BE.Exam.Transaction.ClassSummarySubjectClassWiseCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ClassSummary(User.UserId, User.HostName, User.DBName).GetClassSummarySubjectClassWise(0, ClassId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetViewClassSummaryDetails(int? ClassId, int? SectionId, int? SubjectId, DateTime DateFrom, DateTime DateTo, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            AcademicLib.BE.Exam.Transaction.ClassSummaryCollections dataColl = new AcademicLib.BE.Exam.Transaction.ClassSummaryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ClassSummary(User.UserId, User.HostName, User.DBName).GetViewClassSummaryDetails(0, ClassId, SectionId, SubjectId, DateFrom, DateTo, BatchId, ClassYearId, SemesterId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

         
        [HttpPost]
        public JsonNetResult getClassSummaryDetailsById(int ClassId, int EmployeeId, int SubjectId, int? LessonSno, DateTime TestDate, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsByIdCollections dataColl = new AcademicLib.BE.Exam.Transaction.ClassSummaryDetailsByIdCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ClassSummary(User.UserId, User.HostName, User.DBName).GetClassSummaryDetailsById(0, this.AcademicYearId, ClassId, EmployeeId, SubjectId, LessonSno, TestDate, BatchId, ClassYearId, SemesterId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Achievement)]
        public JsonNetResult DeleteAchievement(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Achievement";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Exam.Transaction.StudentAchievement(User.UserId, User.HostName, User.DBName).DeleteAchievementById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult TransforHeightWeight(int FromExamTypeId, int ToExamTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamWiseHeightWeight(User.UserId, User.HostName, User.DBName).TransforHeightWeight(FromExamTypeId, ToExamTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetTranforHeightWeight()
        {
            AcademicLib.BE.Exam.Transaction.ExamWiseHeightWeightCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamWiseHeightWeightCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ExamWiseHeightWeight(User.UserId, User.HostName, User.DBName).GetExamWiseHeightWeights();
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.TestName)]
        public JsonNetResult DeleteTransforHeigthWeigthById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Past History";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Exam.Transaction.ExamWiseHeightWeight(User.UserId, User.HostName, User.DBName).DeleteById(TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #region "BenchColumn"
        public ActionResult BenchColumn()
        {
            return View();
        }

        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.BenchColumn)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.BenchColumn)]
        public JsonNetResult SaveUpdateBenchColumn()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.BenchColumn>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.BenchColumnId.HasValue)
                        beData.BenchColumnId = 0;

                    resVal = new AcademicLib.BL.Exam.Transaction.BenchColumn(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.BenchColumn)]
        public JsonNetResult getBenchColumnById(int BenchColumnId)
        {
            AcademicLib.BE.Exam.Transaction.BenchColumn resVal = new AcademicLib.BE.Exam.Transaction.BenchColumn();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.BenchColumn(User.UserId, User.HostName, User.DBName).GetBenchColumnById(0, BenchColumnId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.BenchColumn)]
        public JsonNetResult DeleteBenchColumn(int BenchColumnId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (BenchColumnId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Exam.Transaction.BenchColumn(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, BenchColumnId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllBenchColumn()
        {
            AcademicLib.BE.Exam.Transaction.BenchColumnCollections dataColl = new AcademicLib.BE.Exam.Transaction.BenchColumnCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.BenchColumn(User.UserId, User.HostName, User.DBName).GetAllBenchColumn(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetColumnNameById(int NoOfColumn)
        {
            AcademicLib.BE.Exam.Transaction.BenchColumnDetailCollections dataColl = new AcademicLib.BE.Exam.Transaction.BenchColumnDetailCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.BenchColumn(User.UserId, User.HostName, User.DBName).GetColumnNameById(0, NoOfColumn);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        [HttpPost]
        public JsonNetResult SaveExamBulkRoom()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamBulkRoomCollections>(Request["jsonData"]);
                if (beData != null)
                {


                    //if (!beData.ExamRoomId.HasValue)
                    //    beData.ExamRoomId = 0;
                    resVal = new AcademicLib.BL.Exam.Transaction.ExamBulkRoom(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllExamBulkRoom()
        {
            AcademicLib.BE.Exam.Transaction.ExamBulkRoomCollections dataColl = new AcademicLib.BE.Exam.Transaction.ExamBulkRoomCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ExamBulkRoom(User.UserId, User.HostName, User.DBName).GetAllExamBulkRoom(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult HeightAndWeight()
        {
            return View();
        }

        public ActionResult StudentListForExam()
        {
            return View();
        }

        public ActionResult EvaluationArea()
        {
            return View();
        }


        #region EvaluationArea
        [HttpPost]
        public JsonNetResult SaveEvaluationArea()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.EvaluationArea>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.EvaluationId.HasValue)
                        beData.EvaluationId = 0;

                    resVal = new AcademicLib.BL.Exam.Creation.EvaluationArea(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllEvaluationArea()
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.EvaluationArea(User.UserId, User.HostName, User.DBName).GetAllEvaluationArea(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetEvaluationAreaById(int EvaluationId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.EvaluationArea(User.UserId, User.HostName, User.DBName).GetEvaluationAreaById(0, EvaluationId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelEvaluationArea(int EvaluationId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.EvaluationArea(User.UserId, User.HostName, User.DBName).DeleteById(0, EvaluationId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion EvaluationArea


        public ActionResult AssessmentType()
        {
            return View();
        }

        #region "AssessmentType"
        [HttpPost]
        public JsonNetResult SaveAssessmentType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Creation.AssessmentType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.AssessmentTypeId.HasValue)
                        beData.AssessmentTypeId = 0;

                    resVal = new AcademicLib.BL.Exam.Creation.AssessmentType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllAssessmentType()
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.AssessmentType(User.UserId, User.HostName, User.DBName).GetAllAssessmentType(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]

        public JsonNetResult GetAssessmentTypeById(int AssessmentTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Creation.AssessmentType(User.UserId, User.HostName, User.DBName).GetAssessmentById(0, AssessmentTypeId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]

        public JsonNetResult DelAssessmentType(int AssessmentTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.AssessmentType(User.UserId, User.HostName, User.DBName).DeleteById(0, AssessmentTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        //Delete the ICMarkEntry starts
        [HttpPost]
        public JsonNetResult DeleteICMarkEntry(int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, string TopicName, int AssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ICMarkEntry(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassId, SectionId, FilterSection, SubjectId, LessonId, TopicName, AssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteICMarkEntryStudentWise(int ClassId, int? SectionId, bool FilterSection, int SubjectId, int LessonId, int StudentId, int AssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ICMarkEntry(User.UserId, User.HostName, User.DBName).DeleteByIdStudentWise(0, ClassId, SectionId, FilterSection, SubjectId, LessonId, StudentId, AssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        //
        [HttpPost]
        public JsonNetResult DelExamTypeWiseTemplate(int? ExamTypeId, int? ExamTypeGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Creation.ExamTypeWiseTemplate(User.UserId, User.HostName, User.DBName).DelExamTypeWiseTemplate(ExamTypeId, ExamTypeGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        public ActionResult LOCWiseMarkEntry()
        {
            return View();
        }
        //TODO: GradeSheet
        #region "GradeSheet"
        public ActionResult GradeSheet()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetGradeSheet(TableFilter filter, double? GPA, string Alphabet = "", string SEESymbolNo = "")
        {
            AcademicLib.BE.Scholarship.GradeSheetCollections dataColl = new AcademicLib.BE.Scholarship.GradeSheetCollections();
            try
            {
                filter.UserId = User.UserId;
                dataColl = new AcademicLib.BL.Scholarship.GradeSheet(User.UserId, User.HostName, User.DBName).getGradeSheet(filter, GPA, Alphabet, SEESymbolNo);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.TotalRows, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DeleteGradeSheet(DateTime? DateForm, DateTime? DateTo, double? GPA, string Alphabet, string SEESymbolNo)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Scholarship.GradeSheet(User.UserId, User.HostName, User.DBName).DeleteGradeSheet(DateForm, DateTo, GPA, Alphabet, SEESymbolNo);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        [HttpPost]
        public JsonNetResult GetClassWiseMinDues(int ClassId)
        {
            var dataColl = new AcademicLib.BL.Exam.Setup.Configuration(User.UserId, User.HostName, User.DBName).GetMinDuesClasswise(ClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        //Added by simran Shah on 7th poush
        public ActionResult Objective()
        {
            return View();
        }

        #region "Objective"
        [HttpPost]
        public JsonNetResult SaveObjective()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.Objective>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.AcademicYearId = this.AcademicYearId;
                    if (!beData.ObjectiveId.HasValue)
                        beData.ObjectiveId = 0;

                    resVal = new AcademicLib.BL.Exam.Transaction.Objective(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetObjective(int? ClassId, int? ExamTypeId, int? SubjectId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.Objective(User.UserId, User.HostName, User.DBName).GetObjective(0, ClassId, ExamTypeId, SubjectId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllObjective()
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.Objective(User.UserId, User.HostName, User.DBName).GetAllObjective(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelObjective(int ObjectiveId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.Objective(User.UserId, User.HostName, User.DBName).DeleteById(0, ObjectiveId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult TransferObjective(int? FromExamTypeId, int? ToExamTypeId, int? FromClassId, int? ToClassId, int? FromSectionId, int? ToSectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.Objective(User.UserId, User.HostName, User.DBName).TransferObjective(FromExamTypeId, ToExamTypeId, FromClassId, ToClassId, FromSectionId, ToSectionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllObjectiveTansfer(int? FromExamTypeId, int? ToExamTypeId, int? FromClassId, int? ToClassId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.Objective(User.UserId, User.HostName, User.DBName).GetAllObjectiveTansfer(0, FromExamTypeId, ToExamTypeId, FromClassId, ToClassId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion
        //End

        #region "General Evaluation ExamType mark entry"
        public ActionResult ObjectiveMarkEntry()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult GetStudentList(int ClassId, int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ObjectiveMarkEntry(User.UserId, User.HostName, User.DBName).GetStudentList(0, ClassId, SectionId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetObjectiveList(int ClassId, int? SectionId, int SubjectId, int ExamTypeId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ObjectiveMarkEntry(User.UserId, User.HostName, User.DBName).GetObjectiveList(0, ClassId, SectionId, SubjectId, ExamTypeId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        //Added By Suresh on 24 Chaitra
        [HttpPost]
        public JsonNetResult GetExamShiftWiseRoom(int ExamTypeId, int ExamShiftId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamRoom(User.UserId, User.HostName, User.DBName).GetExamShiftWiseRoom(0, ExamTypeId, ExamShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllExamWiseAttendance(int ExamTypeId, int ExamShiftId, int RoomId, DateTime ExamDate)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.ExamRoomAttendance(User.UserId, User.HostName, User.DBName).GetExamWiseAttendance(this.AcademicYearId, 0, ExamTypeId, ExamShiftId, RoomId, ExamDate);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveUpdateExamRoomAttendance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Transaction.ExamRoomAttendanceCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Exam.Transaction.ExamRoomAttendance(User.UserId, User.HostName, User.DBName).SaveUpdateExamAttendance(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult DeleteExamRoomWiseAttendance(DateTime ExamDate, int ExamTypeId, int ExamShiftId, int RoomId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ExamRoomAttendance(User.UserId, User.HostName, User.DBName).DeleteExamRoomWiseAttendance(ExamDate, ExamTypeId, ExamShiftId, RoomId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #region "LOCMarkEntry"

        [HttpPost]
        public JsonNetResult GetStudentsForLOCMarkEntry(int ClassId, int? SectionId, int SubjectId, int LessonId, string TopicName, int? AssessmentTypeId, int? CFAssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Exam.Transaction.LOCWiseMarkEntryCollections dataColl = new AcademicLib.BE.Exam.Transaction.LOCWiseMarkEntryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ICMarkEntry(User.UserId, User.HostName, User.DBName).GetStudentsForLOCMarkEntry(0, this.AcademicYearId, ClassId, SectionId, SubjectId, LessonId, TopicName, AssessmentTypeId, CFAssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStdWiseLOCMarkEntry(int ClassId, int? SectionId, int SubjectId, int LessonId, int StudentId, int? AssessmentTypeId, int? CFAssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Exam.Transaction.StdWiseLOCMarkEntryCollections dataColl = new AcademicLib.BE.Exam.Transaction.StdWiseLOCMarkEntryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ICMarkEntry(User.UserId, User.HostName, User.DBName).GetStdWiseLOCMarkEntry(0, this.AcademicYearId, ClassId, SectionId, SubjectId, LessonId, StudentId, AssessmentTypeId, CFAssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "Theme Mark Entry"

        public ActionResult ThemeMarkEntry()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetThemeWiseMarkEntry(int ClassId, int? SectionId, int SubjectId, int LessonId, int AssessmentTypeId, int? CFAssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.BE.Exam.Transaction.ThemeWiseMarkEntryCollections dataColl = new AcademicLib.BE.Exam.Transaction.ThemeWiseMarkEntryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Exam.Transaction.ThemeMarkEntry(User.UserId, User.HostName, User.DBName).GetThemeWiseMarkEntry(0, this.AcademicYearId, ClassId, SectionId, SubjectId, LessonId, AssessmentTypeId, CFAssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveThemeMarkEntry()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<List<AcademicLib.BE.Exam.Transaction.ThemeMarkEntry>>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.ThemeMarkEntry(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelThemeMarkEntry(int ClassId, int? SectionId, int SubjectId, int LessonId, int AssessmentTypeId, DateTime? AssessmentDate, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Exam.Transaction.ThemeMarkEntry(User.UserId, User.HostName, User.DBName).DelThemeMarkEntry(0, ClassId, SectionId,  SubjectId, LessonId,  AssessmentTypeId, AssessmentDate, BatchId, SemesterId, ClassYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion
    }
}