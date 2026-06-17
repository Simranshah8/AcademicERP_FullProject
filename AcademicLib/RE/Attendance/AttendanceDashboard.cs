using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Attendance.Reporting
{
    public class AttendanceDashboard : ResponeValues
    {
        public string Quotes { get; set; } = " ";
        public string QuotesPhotoPath { get; set; } = " ";
        public AttendanceDashboard()
        {
            StudentAttendancecoll = new TodayStudentAttendancecoll();
            Employeeattendancecoll = new TodayEmployeeAttendancecoll();
            WeeklyStudentcoll = new WeeklyStudentAttendancecoll();
            MonthlyStudentcoll = new Monthlystudentattendancecoll();
            weeklyEmployeecoll = new WeeklyEmployeeattendancecoll();
            monthlyEmployeecoll = new MonthlyEmployeeattendancecoll();
            Leaverequestcoll = new LeaveRequestsOverviewcoll();
            Maxabsencecoll = new MaxConsecutiveAbsencecoll();
            Absenttrendcoll = new AbsenteeismTrendscoll();
            lateattendancecoll = new LateAttendanceTrackercoll();
            compilancecoll = new MinimumAttendanceCompliancecoll();
            Finecoll = new AbsenceFineOverviewcoll();
            StudentAlertcoll = new StudentAttendanceAlertscoll();
            EmployeeAlertscoll = new EmployeeAttendanceAlertscoll();
        }
        public TodayStudentAttendancecoll StudentAttendancecoll { get; set; }
        public TodayEmployeeAttendancecoll Employeeattendancecoll { get; set; }
        public WeeklyStudentAttendancecoll WeeklyStudentcoll { get; set; }
        public Monthlystudentattendancecoll MonthlyStudentcoll { get; set; }
        public WeeklyEmployeeattendancecoll weeklyEmployeecoll { get; set; }
        public MonthlyEmployeeattendancecoll monthlyEmployeecoll { get; set; }
        public LeaveRequestsOverviewcoll Leaverequestcoll { get; set; }
        public MaxConsecutiveAbsencecoll Maxabsencecoll { get; set; }
        public AbsenteeismTrendscoll Absenttrendcoll { get; set; }
        public LateAttendanceTrackercoll lateattendancecoll { get; set; }
        public MinimumAttendanceCompliancecoll compilancecoll { get; set; }
        public AbsenceFineOverviewcoll Finecoll { get; set; }
        public StudentAttendanceAlertscoll StudentAlertcoll { get; set; }
        public EmployeeAttendanceAlertscoll EmployeeAlertscoll { get; set; }

    }
    public class AttendanceDashboardcoll : List<AttendanceDashboard>
    {
        public AttendanceDashboardcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class TodayStudentAttendance
    {
        public string ClassName { get; set; } = " ";
        public DateTime? AttendanceDateS { get; set; }
        public int? TotalSTDPresent { get; set; }
        public int? TotalSTDAbsent { get; set; }
    }
    public class TodayStudentAttendancecoll : List<TodayStudentAttendance>
    {
        public TodayStudentAttendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class TodayEmployeeAttendance
    {
        public string DepartmentName { get; set; } = " ";
        public DateTime? AttendanceDateE { get; set; }
        public int? TotalEMPPresent { get; set; }
        public int? TotalEMPAbsent { get; set; }
    }
    public class TodayEmployeeAttendancecoll : List<TodayEmployeeAttendance>
    {
        public TodayEmployeeAttendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //attendance overview

    public class WeeklyStudentAttendance
    {
        public DateTime? ForDateW { get; set; }
        public int? WeeklyTotalPStudents { get; set; }
        public int? WeeklyPresentSTDBoys { get; set; }
        public int? WeeklyPresentSTDGirls { get; set; }
        public int? WeeklyTotalAStudents { get; set; }
        public int? WeeklyAbsentSTDBoys { get; set; }
        public int? WeeklyAbsentSTDGirls { get; set; }

    }
    public class WeeklyStudentAttendancecoll : List<WeeklyStudentAttendance>
    {
        public WeeklyStudentAttendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Monthly Student Attendance Overview
    public class Monthlystudentattendance
    {

        public DateTime? ForDateM { get; set; }
        public int? MonthlyTotalStudents { get; set; }
        public int? MonthlyPresentSTDBoys { get; set; }
        public int? MonthlyPresentSTDGirls { get; set; }
        public int? MonthlyAbsentSTDBoys { get; set; }
        public int? MonthlyAbsentSTDGirls { get; set; }
    }
    public class Monthlystudentattendancecoll : List<Monthlystudentattendance>
    {
        public Monthlystudentattendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Weekly Employee Attendance Overview
    public class WeeklyEmployeeattendance
    {
        public DateTime? ForDateWE { get; set; }
        public int? WeeklyTotalEmployees { get; set; }
        public int? WeeklyPresentEmpBoys { get; set; }
        public int? WeeklyPresentEmpGirls { get; set; }
        public int? WeeklyAbsentEmpBoys { get; set; }
        public int? WeeklyAbsentEmpGirls { get; set; }
    }

    public class WeeklyEmployeeattendancecoll : List<WeeklyEmployeeattendance>
    {
        public WeeklyEmployeeattendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Monthly Employee Attendance Overview
    public class MonthlyEmployeeattendance
    {
        public DateTime? ForDateME { get; set; }
        public int? MonthlyTotalEmployee { get; set; }
        public int? MonthlyPresentEMPBoys { get; set; }
        public int? MonthlyPresentEMPGirls { get; set; }
        public int? MonthlyAbsentEMPBoys { get; set; }
        public int? MonthlyAbsentEMPGirls { get; set; }
    }

    public class MonthlyEmployeeattendancecoll : List<MonthlyEmployeeattendance>
    {
        public MonthlyEmployeeattendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Leave Requests Overview

    public class LeaveRequestsOverview
    {

        public int? totalstudents { get; set; }
        public int? totalSTDleave { get; set; }
        public int? StudentLeaveBoys { get; set; }
        public int? StudentLeaveGirls { get; set; }
        public int? totalEmployees { get; set; }
        public int? totalEMPleave { get; set; }
        public int? EmployeeLeaveBoys { get; set; }
        public int? EmployeeLeaveGirls { get; set; }
        public int? PendingLeave { get; set; }
        public int? Today { get; set; }
        public int? Weekly { get; set; }
        public int? monthly { get; set; }
    }

    public class LeaveRequestsOverviewcoll : List<LeaveRequestsOverview>
    {
        public LeaveRequestsOverviewcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Max Consecutive Absence
    public class MaxConsecutiveAbsence
    {
        public string StudentNameMCA { get; set; } = " ";
        public string PhotoPathMCA { get; set; } = " ";
        public string ClassMCA { get; set; } = " ";
        public int? AbsentDays { get; set; }
    }

    public class MaxConsecutiveAbsencecoll : List<MaxConsecutiveAbsence>
    {
        public MaxConsecutiveAbsencecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Absenteeism Trends
    public class AbsenteeismTrends
    {
        public int? DataIndex { get; set; }
        public int? absentdata { get; set; }
    }

    public class AbsenteeismTrendscoll : List<AbsenteeismTrends>
    {
        public AbsenteeismTrendscoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Late Attendance Tracker
    public class LateAttendanceTracker
    {
        public int? totalleaveSTD { get; set; }
        public int? totalleaveEMP { get; set; }
        public int? Totalleave { get; set; }
    }

    public class LateAttendanceTrackercoll : List<LateAttendanceTracker>
    {

        public LateAttendanceTrackercoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Minimum Attendance Compliance
    public class MinimumAttendanceCompliance
    {
        public int? TotalComplliance { get; set; }
        public int? Students { get; set; }
        public int? Employee { get; set; }
    }

    public class MinimumAttendanceCompliancecoll : List<MinimumAttendanceCompliance>
    {

        public MinimumAttendanceCompliancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    //Absence Fine Overview
    public class AbsenceFineOverview
    {
        public int? TotalFine { get; set; }
        public int? StudentAbsence { get; set; }
        public int? EmployeeAbsence { get; set; }
    }

    public class AbsenceFineOverviewcoll : List<AbsenceFineOverview>
    {

        public AbsenceFineOverviewcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


    //Student Attendance Alerts
    public class StudentAttendanceAlerts
    {
        public string StudentNameAlerts { get; set; } = " ";
        public string photopathAlerts { get; set; } = " ";
        public string ClassAlerts { get; set; } = " ";
        public int? AbsentdaysAlerts { get; set; }
    }

    public class StudentAttendanceAlertscoll : List<StudentAttendanceAlerts>
    {

        public StudentAttendanceAlertscoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    //Employee Attendance Alerts

    public class EmployeeAttendanceAlerts
    {
        public string EmployeeName { get; set; } = " ";
        public string PhotopathEAA { get; set; } = " ";
        public string Department { get; set; } = " ";
        public int? AbsentDays { get; set; }
    }

    public class EmployeeAttendanceAlertscoll : List<EmployeeAttendanceAlerts>
    {

        public EmployeeAttendanceAlertscoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}