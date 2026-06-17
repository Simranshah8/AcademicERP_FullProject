using Dynamic.DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.DA.Attendance.Reporting
{
    internal class AttendanceAnalysisDB
    {
        DataAccessLayer1 dal = null;
        public AttendanceAnalysisDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public AcademicLib.RE.Attendance.Reporting.AttendanceAnalysis GetAcademicAnalysis(int UserId)
        {
            AcademicLib.RE.Attendance.Reporting.AttendanceAnalysis beData = new AcademicLib.RE.Attendance.Reporting.AttendanceAnalysis();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_AttendanceAnalysis";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                beData.Attendancecoll = new RE.Attendance.Reporting.Attendancecoll();
                while (reader.Read())
                {
                    AcademicLib.RE.Attendance.Reporting.Attendance dataColl = new AcademicLib.RE.Attendance.Reporting.Attendance();
                    if (!(reader[0] is DBNull)) dataColl.Week = reader.GetString(0);
                    if (!(reader[1] is DBNull)) dataColl.presentSTD = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) dataColl.presentEMP = reader.GetInt32(2);
                    beData.Attendancecoll.Add(dataColl);
                }
                if (reader.NextResult())
                {
                    beData.MonthlyAttendancecoll = new RE.Attendance.Reporting.MonthlyAttendanceOverviewcoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.MonthlyAttendanceOverview dataColl = new AcademicLib.RE.Attendance.Reporting.MonthlyAttendanceOverview();
                        if (!(reader[0] is DBNull)) dataColl.Month = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.MonthlyPStudent = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) dataColl.MonthlyPEmployee = reader.GetInt32(2);
                        beData.MonthlyAttendancecoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.ClasswiseLeavecoll = new RE.Attendance.Reporting.ClassWiseStudentLeavecoll();
                    while (reader.Read())
                    {
                        RE.Attendance.Reporting.ClassWiseStudentLeave dataColl = new RE.Attendance.Reporting.ClassWiseStudentLeave();
                        if (!(reader[0] is DBNull)) dataColl.Class = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.SickLeave = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) dataColl.PersonalLeave = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.Others = reader.GetInt32(3);
                        beData.ClasswiseLeavecoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.EmployeeLeaveColl = new RE.Attendance.Reporting.DepartmentWiseEmployeeleavecoll();
                    while (reader.Read())
                    {
                        RE.Attendance.Reporting.DepartmentWiseEmployeeleave dataColl = new RE.Attendance.Reporting.DepartmentWiseEmployeeleave();
                        if (!(reader[0] is DBNull)) dataColl.DepartmentE = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.SickLeaveE = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) dataColl.PersonalLeaveE = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) dataColl.OthersE = reader.GetInt32(3);
                        beData.EmployeeLeaveColl.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.DepartmentWiseAbsenteeismcoll = new RE.Attendance.Reporting.DepartmentWiseAbsenteeismcoll();
                    while (reader.Read())
                    {
                        RE.Attendance.Reporting.DepartmentWiseAbsenteeism dataColl = new RE.Attendance.Reporting.DepartmentWiseAbsenteeism();
                        if (!(reader[0] is DBNull)) dataColl.DepartmentA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.BoysE = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) dataColl.GirlsE = reader.GetInt32(2);
                        beData.DepartmentWiseAbsenteeismcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.ClassAbsenteeismcoll = new RE.Attendance.Reporting.ClasstWiseAbsenteeismcoll();
                    while (reader.Read())
                    {
                        RE.Attendance.Reporting.ClasstWiseAbsenteeism dataColl = new RE.Attendance.Reporting.ClasstWiseAbsenteeism();
                        if (!(reader[0] is DBNull)) dataColl.ClassA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.BoysC = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) dataColl.GirlsC = reader.GetInt32(2);
                        beData.ClassAbsenteeismcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.TopAbsentEmpcoll = new RE.Attendance.Reporting.TopAbsentEmployeesmcoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.TopAbsentEmployees dataColl = new AcademicLib.RE.Attendance.Reporting.TopAbsentEmployees();
                        if (!(reader[0] is DBNull)) dataColl.NameTAE = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.GenderTAE = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.DepartmentTAE = reader.GetString(2);
                        if (!(reader[3] is DBNull)) dataColl.AbsentDayTAE = reader.GetInt32(3);
                        if (!(reader[4] is DBNull)) dataColl.PhotoTAE = reader.GetString(4);
                        beData.TopAbsentEmpcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.TopAbsentScoll = new RE.Attendance.Reporting.TopAbsentStudentscoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.TopAbsentStudents dataColl = new AcademicLib.RE.Attendance.Reporting.TopAbsentStudents();
                        if (!(reader[0] is DBNull)) dataColl.NameTAS = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.GenderTAS = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.ClassTAS = reader.GetString(2);
                        if (!(reader[3] is DBNull)) dataColl.SectionTAS = reader.GetString(3);
                        if (!(reader[4] is DBNull)) dataColl.AbsentDayTAS = reader.GetInt32(4);
                        if (!(reader[5] is DBNull)) dataColl.PhotoTAS = reader.GetString(5);
                        beData.TopAbsentScoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.ELCAcoll = new RE.Attendance.Reporting.EmployeesLongestConsecutiveAbsencecoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.EmployeesLongestConsecutiveAbsence dataColl = new AcademicLib.RE.Attendance.Reporting.EmployeesLongestConsecutiveAbsence();
                        if (!(reader[0] is DBNull)) dataColl.NameCD = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.PhotopathCD = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.DepartmentCD = reader.GetString(2);
                        if (!(reader[3] is DBNull)) dataColl.LongestConsecutiveDays = reader.GetInt32(3);
                        beData.ELCAcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.EMAcoll = new RE.Attendance.Reporting.EmployeesMimimumAttendancecoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.EmployeesMimimumAttendance dataColl = new AcademicLib.RE.Attendance.Reporting.EmployeesMimimumAttendance();
                        if (!(reader[0] is DBNull)) dataColl.NameMA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.PhotopathMA = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.DepartmentMA = reader.GetString(2);
                        if (!(reader[3] is DBNull)) dataColl.MinimumAbsenteeism = reader.GetInt32(3);
                        beData.EMAcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.EHAcoll = new RE.Attendance.Reporting.EmployeesHigherAbsenteeismcoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.EmployeesHigherAbsenteeism dataColl = new AcademicLib.RE.Attendance.Reporting.EmployeesHigherAbsenteeism();
                        if (!(reader[0] is DBNull)) dataColl.NameHA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.PhotopathHA = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.DepartmentHA = reader.GetString(2);
                        if (!(reader[3] is DBNull)) dataColl.HighestAbsenteeism = reader.GetInt32(3);
                        beData.EHAcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.SLCAcoll = new RE.Attendance.Reporting.StudentLongestConsecutiveAbsencecoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.StudentLongestConsecutiveAbsence dataColl = new AcademicLib.RE.Attendance.Reporting.StudentLongestConsecutiveAbsence();
                        if (!(reader[0] is DBNull)) dataColl.NameSCD = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.PhotopathSCD = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.ClassSCD = reader.GetString(2);
                        if (!(reader[3] is DBNull)) dataColl.LongestConsecutiveDaysS = reader.GetInt32(3);
                        beData.SLCAcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.SMAcoll = new RE.Attendance.Reporting.StudentMinimumAttendancecoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.StudentMinimumAttendance dataColl = new AcademicLib.RE.Attendance.Reporting.StudentMinimumAttendance();
                        if (!(reader[0] is DBNull)) dataColl.NameSMA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.PhotopathSMA = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.ClassSMA = reader.GetString(2);
                        if (!(reader[3] is DBNull)) dataColl.MinimumAttendanceS = reader.GetInt32(3);
                        beData.SMAcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.SHAcoll = new RE.Attendance.Reporting.StudentHigherAbsenteeismcoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.StudentHigherAbsenteeism dataColl = new AcademicLib.RE.Attendance.Reporting.StudentHigherAbsenteeism();
                        if (!(reader[0] is DBNull)) dataColl.NameSHA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) dataColl.PhotopathSHA = reader.GetString(1);
                        if (!(reader[2] is DBNull)) dataColl.DepartmentSHA = reader.GetString(2);
                        if (!(reader[3] is DBNull)) dataColl.HighestAbsenteeismS = reader.GetInt32(3);
                        beData.SHAcoll.Add(dataColl);
                    }
                }
                if (reader.NextResult())
                {
                    beData.ClassWisecoll = new RE.Attendance.Reporting.ClassWiselateAttendancecoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.ClassWiselateAttendance absentSLC = new AcademicLib.RE.Attendance.Reporting.ClassWiselateAttendance();
                        if (!(reader[0] is DBNull)) absentSLC.ClassCLA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) absentSLC.LateBoysS = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) absentSLC.LateGirlsS = reader.GetInt32(2);
                        beData.ClassWisecoll.Add(absentSLC);
                    }
                }
                if (reader.NextResult())
                {
                    beData.EmployeeLateAttdcoll = new RE.Attendance.Reporting.DepartmentWiselateAttendancecoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.DepartmentWiselateAttendance absentDWL = new AcademicLib.RE.Attendance.Reporting.DepartmentWiselateAttendance();
                        if (!(reader[0] is DBNull)) absentDWL.Department = reader.GetString(0);
                        if (!(reader[1] is DBNull)) absentDWL.LateBoysE = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) absentDWL.LateGirlsE = reader.GetInt32(2);
                        beData.EmployeeLateAttdcoll.Add(absentDWL);
                    }
                }
                if (reader.NextResult())
                {
                    beData.StudentFinecoll = new RE.Attendance.Reporting.AbsenceFineByClasscoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.AbsenceFineByClass absentAC = new AcademicLib.RE.Attendance.Reporting.AbsenceFineByClass();
                        if (!(reader[0] is DBNull)) absentAC.ClassAFC = reader.GetString(0);
                        if (!(reader[1] is DBNull)) absentAC.AbsentAFC = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) absentAC.FineAFC = Convert.ToDouble(reader[2]);
                        beData.StudentFinecoll.Add(absentAC);
                    }
                }
                if (reader.NextResult())
                {
                    beData.MinimumAttdcoll = new RE.Attendance.Reporting.MeetingMinimumAttendancecoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.MeetingMinimumAttendance absentMMA = new AcademicLib.RE.Attendance.Reporting.MeetingMinimumAttendance();
                        if (!(reader[0] is DBNull)) absentMMA.MonthNameMMA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) absentMMA.StudentMMA = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) absentMMA.EmployeeMMA = reader.GetInt32(2);
                        beData.MinimumAttdcoll.Add(absentMMA);
                    }
                }
                if (reader.NextResult())
                {
                    beData.EmployeeFinecoll = new RE.Attendance.Reporting.AbsenceFineByDepartmentcoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.AbsenceFineByDepartment absentMMA = new AcademicLib.RE.Attendance.Reporting.AbsenceFineByDepartment();
                        if (!(reader[0] is DBNull)) absentMMA.DepartmentAFD = reader.GetString(0);
                        if (!(reader[1] is DBNull)) absentMMA.AbsentAFD = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) absentMMA.FineAFD = Convert.ToDouble(reader[2]);
                        beData.EmployeeFinecoll.Add(absentMMA);
                    }
                }
                if (reader.NextResult())
                {
                    beData.Comparisoncoll = new RE.Attendance.Reporting.AbsenteeismCompariosncoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.AbsenteeismCompariosn Acomparison = new AcademicLib.RE.Attendance.Reporting.AbsenteeismCompariosn();
                        if (!(reader[0] is DBNull)) Acomparison.AbsentData2078P = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) Acomparison.AbsentData2079P = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) Acomparison.AbsentData2080P = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) Acomparison.AbsentData2081P = reader.GetInt32(3);
                        if (!(reader[4] is DBNull)) Acomparison.AbsentData2078C = reader.GetInt32(4);
                        if (!(reader[5] is DBNull)) Acomparison.AbsentData2079C = reader.GetInt32(5);
                        if (!(reader[6] is DBNull)) Acomparison.AbsentData2080C = reader.GetInt32(6);
                        if (!(reader[7] is DBNull)) Acomparison.AbsentData2081C = reader.GetInt32(7);
                        beData.Comparisoncoll.Add(Acomparison);
                    }
                }
                if (reader.NextResult())
                {
                    beData.Studentcomparisoncoll = new RE.Attendance.Reporting.StudentCvsPCompariosncoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.StudentCvsPCompariosn comparison = new AcademicLib.RE.Attendance.Reporting.StudentCvsPCompariosn();
                        if (!(reader[0] is DBNull)) comparison.ClassE = reader.GetString(0);
                        if (!(reader[1] is DBNull)) comparison.AbsentData2080SCvsP = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) comparison.AbsentData2081SCvsP = reader.GetInt32(2);
                        beData.Studentcomparisoncoll.Add(comparison);
                    }
                }
                if (reader.NextResult())
                {
                    beData.Employeecomparisoncoll = new RE.Attendance.Reporting.EmployeeCvsPCompariosncoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.EmployeeCvsPCompariosn comparisonE = new AcademicLib.RE.Attendance.Reporting.EmployeeCvsPCompariosn();
                        if (!(reader[0] is DBNull)) comparisonE.DepartmentE = reader.GetString(0);
                        if (!(reader[1] is DBNull)) comparisonE.AbsentData2080ECvsP = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) comparisonE.AbsentData2081ECvsP = reader.GetInt32(2);
                        beData.Employeecomparisoncoll.Add(comparisonE);
                    }
                }
                if (reader.NextResult())
                {

                    beData.Highabsenteeismcoll = new RE.Attendance.Reporting.HigherAbsenteeismcoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.HigherAbsenteeism HAbsence = new AcademicLib.RE.Attendance.Reporting.HigherAbsenteeism();
                        if (!(reader[0] is DBNull)) HAbsence.MonthHA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) HAbsence.AbsentSTD = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) HAbsence.AbsentEMP = reader.GetInt32(2);
                        if (!(reader[3] is DBNull)) HAbsence.MonthlyAbsentHA = reader.GetInt32(3);
                        beData.Highabsenteeismcoll.Add(HAbsence);
                    }
                }
                if (reader.NextResult())
                {
                    beData.Excessiveabsentcoll = new RE.Attendance.Reporting.Excessiveabsencescoll();
                    while (reader.Read())
                    {
                        AcademicLib.RE.Attendance.Reporting.Excessiveabsences absenceE = new AcademicLib.RE.Attendance.Reporting.Excessiveabsences();
                        if (!(reader[0] is DBNull)) absenceE.MonthEA = reader.GetString(0);
                        if (!(reader[1] is DBNull)) absenceE.AbsentSTDEA = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) absenceE.AbsentEMPEA = reader.GetInt32(2);
                        beData.Excessiveabsentcoll.Add(absenceE);
                    }
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