using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using AcademicLib.BE.Global;
namespace PivotalERP.Areas.Fee.Controllers
{
    public class ReportController : PivotalERP.Controllers.BaseController
    {
        // GET: Fee/Report
        [PermissionsAttribute(Actions.View, (int)ENTITIES.StudentStatement, false)]
        public ActionResult StudentStatement()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetStudentVoucher(int StudentId,int? SemesterId,int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getStudentVoucher(this.AcademicYearId, StudentId,SemesterId,ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentLedger(int StudentId,int? SemesterId,int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getStudentLedger(this.AcademicYearId, StudentId,SemesterId,ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        
        public ActionResult RptStudentLedger()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.FeeSummary, false)]
        public ActionResult FeeSummary()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetFeeSummary(int fromMonthId,int toMonthId,int forStudent,string feeItemIdColl,int? classId,int? sectionId, int? batchId, int? semesterId, int? classYearId, bool ForPaymentFollowup, int FollowupType,DateTime? dateFrom,DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getFeeSummary(this.AcademicYearId, fromMonthId, toMonthId, forStudent,feeItemIdColl,classId,sectionId,batchId,semesterId,classYearId,ForPaymentFollowup,FollowupType,dateFrom,dateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetFeeDateWise(string feeItemIdColl, DateTime? dateFrom, DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getFeeSummaryDateWise(this.AcademicYearId,   feeItemIdColl,    dateFrom, dateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult PrintFeeSummary()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Fee.FeeSummary> paraData = DeserializeObject<List<AcademicLib.RE.Fee.FeeSummary>>(jsonData);
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
        public JsonNetResult GetFeeMappingStudent(string ClassIdColl,string FeeItemIdColl,int For)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeMappingClassWise(User.UserId, User.HostName, User.DBName).getFeeMappingStudentList(this.AcademicYearId, ClassIdColl, FeeItemIdColl,For);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PrintFeeMappingStudent()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Fee.FeeMappingStudent> paraData = DeserializeObject<List<AcademicLib.RE.Fee.FeeMappingStudent>>(jsonData);
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

        public ActionResult RdlFeeDayBook()
        {
            return View();
        }
        public ActionResult RptFeeSummaryClassWise()
        {
            return View();
        }
        public ActionResult RptFeeSummaryStudentWise()
        {
            return View();
        }
        [PermissionsAttribute(Actions.View, (int)ENTITIES.IncomeRegister, false)]
        public ActionResult IncomeRegister()
        {
            return View();
        }

        public ActionResult RdlDateWiseIncomeRegister()
        {
            return View();
        }
        public ActionResult RptFeeIncomeClassWise()
        {
            return View();
        }
        public ActionResult RptFeeIncomeStudentWise()
        {
            return View();
        }
        public ActionResult RdlClassAndFeeWiseSummary()
        {
            return View();
        }

        public ActionResult RdlFeeSummaryPC()
        {
            return View();
        }
        public ActionResult Additional()
        {
            return View();
        }
        #region "Billing Summary"
        public ActionResult BillingSummary()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetBillingSummaryList(DateTime? FromDate, DateTime? ToDate, string BillingType, int ReportType, bool? IsCancel)
        {
            var dataColl = new AcademicLib.BL.Fee.Report.BillingSummary(User.UserId, User.HostName, User.DBName).GetBillingSummaryList(FromDate, ToDate, BillingType, ReportType, IsCancel);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

    }
}