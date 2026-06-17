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

namespace PivotalERP.Areas.Attendance.Controllers
{
    public class ReportController : PivotalERP.Controllers.BaseController
    {
        // GET: Attendance/Report

        [PermissionsAttribute(Actions.View, (int)ENTITIES.EmployeeAdvance, true)]
        public ActionResult EmployeeAdvanceSummary()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ExpensesReport, true)]
        public ActionResult ExpenseReport()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PaySlip, true)]
        public ActionResult PaySlip()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalarySheet, false)]
        public ActionResult SalarySheet()
        {
            return View();
        }
        public ActionResult RdlSalarySheet()
        {
            return View();
        }

        #region Period Salary Sheet
        public ActionResult PeriodSalarySheet()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult GetAllPeriodSalarySheet(int FromYearId, int FromMonthId, int ToYearId, int ToMonthId, int? BranchId = null, int? DepartmentId = null, int? CategoryId = null)
        {
            AcademicLib.BE.Payroll.PeriodSalarySheetCollections dataColl = new AcademicLib.BE.Payroll.PeriodSalarySheetCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.PeriodSalarySheet(User.UserId, User.HostName, User.DBName).GetAllPeriodSalarySheet(FromYearId, FromMonthId, ToYearId, ToMonthId, BranchId, DepartmentId, CategoryId);
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

        public ActionResult AttendanceDashboard()
        {
            return View();
        }

        #region "AttendanceDashboard"
        [HttpPost]
        public JsonNetResult GetAllAttendance()
        {
            AcademicLib.RE.Attendance.Reporting.AttendanceDashboard dataColl = new AcademicLib.RE.Attendance.Reporting.AttendanceDashboard();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.Reporting.AttendanceDashboard(User.UserId, User.HostName, User.DBName).getallAttendance(1);
                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        #region "AttendanceAnalysis"
        public ActionResult AttendanceAnalysis()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetAllAnalysis()
        {
            AcademicLib.RE.Attendance.Reporting.AttendanceAnalysis dataColl = new AcademicLib.RE.Attendance.Reporting.AttendanceAnalysis();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.Reporting.AttendanceAnalysis(User.UserId, User.HostName, User.DBName).getallAttendanceAnalysis();
                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };         
        }
        #endregion

        public ActionResult LeaveBalanceSummary()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetLeaveBalanaceSummary(string BranchIdColl = "", string DepartmentIdColl = "", string CategoryIdColl = "", int? ForUserId = null, int? PeriodId = null, int? LeaveTypeId = null)
        {
            AcademicLib.BE.Attendance.LeaveBalanceSummaryCollections dataColl = new AcademicLib.BE.Attendance.LeaveBalanceSummaryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.LeaveBalanceSummary(User.UserId, User.HostName, User.DBName).GetLeaveBalanaceSummary(BranchIdColl, DepartmentIdColl, CategoryIdColl, ForUserId, PeriodId, LeaveTypeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
    }
}