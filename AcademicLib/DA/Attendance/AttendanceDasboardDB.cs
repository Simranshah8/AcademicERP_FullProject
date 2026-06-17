using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Attendance.Reporting
{
    internal class AttendanceDashboardDB
    {
        DataAccessLayer1 dal = null;
        public AttendanceDashboardDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public AcademicLib.RE.Attendance.Reporting.AttendanceDashboard getDashboard(int UserId)
        {
            AcademicLib.RE.Attendance.Reporting.AttendanceDashboard beData = new AcademicLib.RE.Attendance.Reporting.AttendanceDashboard();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_AttendanceDashboard";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.Quotes = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.QuotesPhotoPath = reader.GetString(1);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.TodayStudentAttendance Astudent = new AcademicLib.RE.Attendance.Reporting.TodayStudentAttendance();
                    if (!(reader[0] is DBNull)) Astudent.ClassName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) Astudent.AttendanceDateS = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) Astudent.TotalSTDPresent = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) Astudent.TotalSTDAbsent = reader.GetInt32(3);
                    beData.StudentAttendancecoll.Add(Astudent);

                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.TodayEmployeeAttendance Employee = new AcademicLib.RE.Attendance.Reporting.TodayEmployeeAttendance();
                    if (!(reader[0] is DBNull)) Employee.DepartmentName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) Employee.AttendanceDateE = reader.GetDateTime(1);
                    if (!(reader[2] is DBNull)) Employee.TotalEMPPresent = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) Employee.TotalEMPAbsent = reader.GetInt32(3);
                    beData.Employeeattendancecoll.Add(Employee);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.WeeklyStudentAttendance StudentW = new AcademicLib.RE.Attendance.Reporting.WeeklyStudentAttendance();
                    if (!(reader[0] is DBNull)) StudentW.ForDateW = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) StudentW.WeeklyTotalPStudents = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) StudentW.WeeklyPresentSTDBoys = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) StudentW.WeeklyPresentSTDGirls = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) StudentW.WeeklyTotalAStudents = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) StudentW.WeeklyAbsentSTDBoys = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) StudentW.WeeklyAbsentSTDGirls = reader.GetInt32(6);

                    beData.WeeklyStudentcoll.Add(StudentW);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.Monthlystudentattendance StudentM = new AcademicLib.RE.Attendance.Reporting.Monthlystudentattendance();
                    if (!(reader[0] is DBNull)) StudentM.ForDateM = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) StudentM.MonthlyTotalStudents = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) StudentM.MonthlyPresentSTDBoys = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) StudentM.MonthlyPresentSTDGirls = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) StudentM.MonthlyAbsentSTDBoys = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) StudentM.MonthlyAbsentSTDGirls = reader.GetInt32(5);
                    beData.MonthlyStudentcoll.Add(StudentM);

                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.WeeklyEmployeeattendance EmployeeW = new AcademicLib.RE.Attendance.Reporting.WeeklyEmployeeattendance();
                    if (!(reader[0] is DBNull)) EmployeeW.ForDateWE = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) EmployeeW.WeeklyTotalEmployees = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) EmployeeW.WeeklyPresentEmpBoys = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) EmployeeW.WeeklyPresentEmpGirls = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) EmployeeW.WeeklyAbsentEmpBoys = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) EmployeeW.WeeklyAbsentEmpGirls = reader.GetInt32(5);
                    beData.weeklyEmployeecoll.Add(EmployeeW);

                }
                reader.NextResult();
                    while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.MonthlyEmployeeattendance EmployeeM = new AcademicLib.RE.Attendance.Reporting.MonthlyEmployeeattendance();
                    if (!(reader[0] is DBNull)) EmployeeM.ForDateME = reader.GetDateTime(0);
                    if (!(reader[1] is DBNull)) EmployeeM.MonthlyTotalEmployee = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) EmployeeM.MonthlyPresentEMPBoys = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) EmployeeM.MonthlyPresentEMPGirls = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) EmployeeM.MonthlyAbsentEMPBoys = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) EmployeeM.MonthlyAbsentEMPGirls = reader.GetInt32(5);
                    beData.monthlyEmployeecoll.Add(EmployeeM);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.LeaveRequestsOverview Student = new AcademicLib.RE.Attendance.Reporting.LeaveRequestsOverview();
                    if (!(reader[0] is DBNull)) Student.totalstudents = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) Student.totalSTDleave = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) Student.StudentLeaveBoys = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) Student.StudentLeaveGirls = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) Student.totalEmployees = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) Student.totalEMPleave = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) Student.EmployeeLeaveBoys = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) Student.EmployeeLeaveGirls = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) Student.PendingLeave = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) Student.Today = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) Student.Weekly = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) Student.monthly = reader.GetInt32(11);
                    beData.Leaverequestcoll.Add(Student);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.MaxConsecutiveAbsence Maxabsence = new AcademicLib.RE.Attendance.Reporting.MaxConsecutiveAbsence();
                    if (!(reader[0] is DBNull)) Maxabsence.StudentNameMCA = reader.GetString(0);
                    if (!(reader[1] is DBNull)) Maxabsence.PhotoPathMCA = reader.GetString(1);
                    if (!(reader[2] is DBNull)) Maxabsence.ClassMCA = reader.GetString(2);
                    if (!(reader[3] is DBNull)) Maxabsence.AbsentDays = reader.GetInt32(3);
                    beData.Maxabsencecoll.Add(Maxabsence);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.AbsenteeismTrends Trends = new AcademicLib.RE.Attendance.Reporting.AbsenteeismTrends();
                    if (!(reader[0] is DBNull)) Trends.DataIndex = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) Trends.absentdata = reader.GetInt32(1);
                    beData.Absenttrendcoll.Add(Trends);

                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.LateAttendanceTracker Tracker = new AcademicLib.RE.Attendance.Reporting.LateAttendanceTracker();
                    if (!(reader[0] is DBNull)) Tracker.totalleaveSTD = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) Tracker.totalleaveEMP = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) Tracker.Totalleave = reader.GetInt32(2);
                    beData.lateattendancecoll.Add(Tracker); 

                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.MinimumAttendanceCompliance Compilance = new AcademicLib.RE.Attendance.Reporting.MinimumAttendanceCompliance();
                    if (!(reader[0] is DBNull)) Compilance.TotalComplliance = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) Compilance.Students = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) Compilance.Employee = reader.GetInt32(2);
                    beData.compilancecoll.Add(Compilance);

                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.AbsenceFineOverview Fine = new AcademicLib.RE.Attendance.Reporting.AbsenceFineOverview();
                    if (!(reader[0] is DBNull)) Fine.TotalFine = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) Fine.StudentAbsence = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) Fine.EmployeeAbsence = reader.GetInt32(2);
                    beData.Finecoll.Add(Fine);


                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.StudentAttendanceAlerts Alerts = new AcademicLib.RE.Attendance.Reporting.StudentAttendanceAlerts();
                    if (!(reader[0] is DBNull)) Alerts.StudentNameAlerts = reader.GetString(0);
                    if (!(reader[1] is DBNull)) Alerts.photopathAlerts = reader.GetString(1);
                    if (!(reader[2] is DBNull)) Alerts.ClassAlerts = reader.GetString(2);
                    if (!(reader[3] is DBNull)) Alerts.AbsentdaysAlerts = reader.GetInt32(3);
                    beData.StudentAlertcoll.Add(Alerts);

                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.EmployeeAttendanceAlerts Alerts = new AcademicLib.RE.Attendance.Reporting.EmployeeAttendanceAlerts();
                    if (!(reader[0] is DBNull)) Alerts.EmployeeName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) Alerts.PhotopathEAA = reader.GetString(1);
                    if (!(reader[2] is DBNull)) Alerts.Department = reader.GetString(2);
                    if (!(reader[3] is DBNull)) Alerts.AbsentDays = reader.GetInt32(3);
                    beData.EmployeeAlertscoll.Add(Alerts);

                }

                reader.Close();
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)

            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;

        }

    }

}