using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Attendance.Reporting
{
    public class AttendanceAnalysis : ResponeValues
    {
        public AttendanceAnalysis()
        {
            Attendancecoll = new Attendancecoll();
            MonthlyAttendancecoll = new MonthlyAttendanceOverviewcoll();
            ClasswiseLeavecoll = new ClassWiseStudentLeavecoll();
            EmployeeLeaveColl = new DepartmentWiseEmployeeleavecoll();
            DepartmentWiseAbsenteeismcoll = new DepartmentWiseAbsenteeismcoll();
            ClassAbsenteeismcoll = new ClasstWiseAbsenteeismcoll();
            TopAbsentEmpcoll = new TopAbsentEmployeesmcoll();
            TopAbsentScoll = new TopAbsentStudentscoll();
            ELCAcoll = new EmployeesLongestConsecutiveAbsencecoll();
            EMAcoll = new EmployeesMimimumAttendancecoll();
            EHAcoll = new EmployeesHigherAbsenteeismcoll();
            SLCAcoll = new StudentLongestConsecutiveAbsencecoll();
            SMAcoll = new StudentMinimumAttendancecoll();
            SHAcoll = new StudentHigherAbsenteeismcoll();
            ClassWisecoll = new ClassWiselateAttendancecoll();
            EmployeeLateAttdcoll = new DepartmentWiselateAttendancecoll();
            StudentFinecoll = new AbsenceFineByClasscoll();
            MinimumAttdcoll = new MeetingMinimumAttendancecoll();
            EmployeeFinecoll = new AbsenceFineByDepartmentcoll();
            Comparisoncoll = new AbsenteeismCompariosncoll();
            Studentcomparisoncoll = new StudentCvsPCompariosncoll();
            Employeecomparisoncoll = new EmployeeCvsPCompariosncoll();
            Highabsenteeismcoll = new HigherAbsenteeismcoll();
            Excessiveabsentcoll = new Excessiveabsencescoll();
        }
        public Attendancecoll Attendancecoll { get; set; }
        public MonthlyAttendanceOverviewcoll MonthlyAttendancecoll { get; set; }
        public ClassWiseStudentLeavecoll ClasswiseLeavecoll { get; set; }
        public DepartmentWiseEmployeeleavecoll EmployeeLeaveColl { get; set; }
        public DepartmentWiseAbsenteeismcoll DepartmentWiseAbsenteeismcoll { get; set; }
        public ClasstWiseAbsenteeismcoll ClassAbsenteeismcoll { get; set; }
        public TopAbsentEmployeesmcoll TopAbsentEmpcoll { get; set; }
        public TopAbsentStudentscoll TopAbsentScoll { get; set; }
        public EmployeesLongestConsecutiveAbsencecoll ELCAcoll { get; set; }
        public EmployeesMimimumAttendancecoll EMAcoll { get; set; }
        public EmployeesHigherAbsenteeismcoll EHAcoll { get; set; }
        public StudentLongestConsecutiveAbsencecoll SLCAcoll { get; set; }
        public StudentMinimumAttendancecoll SMAcoll { get; set; }
        public StudentHigherAbsenteeismcoll SHAcoll { get; set; }
        public ClassWiselateAttendancecoll ClassWisecoll { get; set; }
        public DepartmentWiselateAttendancecoll EmployeeLateAttdcoll { get; set; }
        public AbsenceFineByClasscoll StudentFinecoll { get; set; }
        public MeetingMinimumAttendancecoll MinimumAttdcoll { get; set; }
        public AbsenceFineByDepartmentcoll EmployeeFinecoll { get; set; }
        public AbsenteeismCompariosncoll Comparisoncoll { get; set; }
        public StudentCvsPCompariosncoll Studentcomparisoncoll { get; set; }
        public EmployeeCvsPCompariosncoll Employeecomparisoncoll { get; set; }
        public HigherAbsenteeismcoll Highabsenteeismcoll { get; set; }
        public Excessiveabsencescoll Excessiveabsentcoll { get; set; }
    }
    public class Attendance : ResponeValues
    {
        //weekly attendance overview
        public string Week { get; set; } = " ";
        public int? presentSTD { get; set; }
        public int? presentEMP { get; set; }
    }
    public class Attendancecoll : List<Attendance>
    {
        public Attendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class AttendanceAnalysiscoll : List<AttendanceAnalysis>
    {
        public AttendanceAnalysiscoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class MonthlyAttendanceOverview : ResponeValues
    {
        public string Month { get; set; } = " ";
        public int? MonthlyPStudent { get; set; }
        public int? MonthlyPEmployee { get; set; }
    }
    public class MonthlyAttendanceOverviewcoll : List<MonthlyAttendanceOverview>
    {
        public MonthlyAttendanceOverviewcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClassWiseStudentLeave : ResponeValues
    {
        public string Class { get; set; } = " ";
        public int? SickLeave { get; set; }
        public int? PersonalLeave { get; set; }
        public int? Others { get; set; }
    }
    public class ClassWiseStudentLeavecoll : List<ClassWiseStudentLeave>
    {
        public ClassWiseStudentLeavecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class DepartmentWiseEmployeeleave : ResponeValues
    {
        public string DepartmentE { get; set; } = " ";
        public int? SickLeaveE { get; set; }
        public int? PersonalLeaveE { get; set; }
        public int? OthersE { get; set; }
    }
    public class DepartmentWiseEmployeeleavecoll : List<DepartmentWiseEmployeeleave>
    {
        public DepartmentWiseEmployeeleavecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class DepartmentWiseAbsenteeism : ResponeValues
    {
        public string DepartmentA { get; set; } = " ";
        public int? BoysE { get; set; }
        public int? GirlsE { get; set; }

    }
    public class DepartmentWiseAbsenteeismcoll : List<DepartmentWiseAbsenteeism>
    {
        public DepartmentWiseAbsenteeismcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClasstWiseAbsenteeism : ResponeValues
    {
        public string ClassA { get; set; } = " ";
        public int? BoysC { get; set; }
        public int? GirlsC { get; set; }

    }
    public class ClasstWiseAbsenteeismcoll : List<ClasstWiseAbsenteeism>
    {
        public ClasstWiseAbsenteeismcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class TopAbsentEmployees : ResponeValues
    {
        public string NameTAE { get; set; } = " ";
        public string GenderTAE { get; set; } = " ";
        public string DepartmentTAE { get; set; } = " ";
        public int? AbsentDayTAE { get; set; }
        public string PhotoTAE { get; set; } = " ";
    }
    public class TopAbsentEmployeesmcoll : List<TopAbsentEmployees>
    {
        public TopAbsentEmployeesmcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class TopAbsentStudents : ResponeValues
    {
        public string NameTAS { get; set; } = " ";
        public string GenderTAS { get; set; } = " ";
        public string ClassTAS { get; set; } = " ";
        public string SectionTAS { get; set; } = " ";
        public int? AbsentDayTAS { get; set; }
        public string PhotoTAS { get; set; } = " ";
    }
    public class TopAbsentStudentscoll : List<TopAbsentStudents>
    {
        public TopAbsentStudentscoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeesLongestConsecutiveAbsence : ResponeValues
    {
        public string NameCD { get; set; } = " ";
        public string PhotopathCD { get; set; } = " ";
        public string DepartmentCD { get; set; } = " ";
        public int? LongestConsecutiveDays { get; set; }

    }
    public class EmployeesLongestConsecutiveAbsencecoll : List<EmployeesLongestConsecutiveAbsence>
    {
        public EmployeesLongestConsecutiveAbsencecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeesMimimumAttendance : ResponeValues
    {
        public string NameMA { get; set; } = " ";
        public string PhotopathMA { get; set; } = " ";
        public string DepartmentMA { get; set; } = " ";
        public int? MinimumAbsenteeism { get; set; }
    } 
    public class EmployeesMimimumAttendancecoll : List<EmployeesMimimumAttendance>
    {
        public EmployeesMimimumAttendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeesHigherAbsenteeism : ResponeValues
    {
        public string NameHA { get; set; } = " ";
        public string PhotopathHA { get; set; } = " ";
        public string DepartmentHA { get; set; } = " ";
        public int? HighestAbsenteeism { get; set; }
    }
    public class EmployeesHigherAbsenteeismcoll : List<EmployeesHigherAbsenteeism>
    {
        public EmployeesHigherAbsenteeismcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentLongestConsecutiveAbsence : ResponeValues
    {
        public string NameSCD { get; set; } = " ";
        public string PhotopathSCD { get; set; } = " ";
        public string ClassSCD { get; set; } = " ";
        public int? LongestConsecutiveDaysS { get; set; }
    }
    public class StudentLongestConsecutiveAbsencecoll : List<StudentLongestConsecutiveAbsence>
    {
        public StudentLongestConsecutiveAbsencecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentMinimumAttendance : ResponeValues
    {
        public string NameSMA { get; set; } = " ";
        public string PhotopathSMA { get; set; } = " ";
        public string ClassSMA { get; set; } = " ";
        public int? MinimumAttendanceS { get; set; }
    }
    public class StudentMinimumAttendancecoll : List<StudentMinimumAttendance>
    {
        public StudentMinimumAttendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentHigherAbsenteeism : ResponeValues
    {
        public string NameSHA { get; set; } = " ";
        public string PhotopathSHA { get; set; } = " ";
        public string DepartmentSHA { get; set; } = " ";
        public int? HighestAbsenteeismS { get; set; }
    }
    public class StudentHigherAbsenteeismcoll : List<StudentHigherAbsenteeism>
    {
        public StudentHigherAbsenteeismcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ClassWiselateAttendance : ResponeValues
    {
        public string ClassCLA { get; set; } = " ";
        public int? LateBoysS { get; set; }
        public int? LateGirlsS { get; set; }
    }
    public class ClassWiselateAttendancecoll : List<ClassWiselateAttendance>
    {
        public ClassWiselateAttendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class DepartmentWiselateAttendance : ResponeValues
    {
        public string Department { get; set; } = " ";
        public int? LateBoysE { get; set; }
        public int? LateGirlsE { get; set; }
    }
    public class DepartmentWiselateAttendancecoll : List<DepartmentWiselateAttendance>
    {
        public DepartmentWiselateAttendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class AbsenceFineByClass : ResponeValues
    {
        public string ClassAFC { get; set; } = " ";
        public int? AbsentAFC { get; set; }
        public double? FineAFC { get; set; }
    }
    public class AbsenceFineByClasscoll : List<AbsenceFineByClass>
    {
        public AbsenceFineByClasscoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class MeetingMinimumAttendance : ResponeValues
    {
        public string MonthNameMMA { get; set; } = " ";
        public int? StudentMMA { get; set; }
        public int? EmployeeMMA { get; set; }
    }
    public class MeetingMinimumAttendancecoll : List<MeetingMinimumAttendance>
    {
        public MeetingMinimumAttendancecoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class AbsenceFineByDepartment : ResponeValues
    {
        public string DepartmentAFD { get; set; } = " ";
        public int? AbsentAFD { get; set; }
        public double? FineAFD { get; set; }
    }
    public class AbsenceFineByDepartmentcoll : List<AbsenceFineByDepartment>
    {
        public AbsenceFineByDepartmentcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class AbsenteeismCompariosn : ResponeValues
    {
        public int? AbsentData2078P { get; set; }
        public int? AbsentData2079P { get; set; }
        public int? AbsentData2080P { get; set; }
        public int? AbsentData2081P { get; set; }

        public int? AbsentData2078C { get; set; }
        public int? AbsentData2079C { get; set; }
        public int? AbsentData2080C { get; set; }
        public int? AbsentData2081C { get; set; }
    }
    public class AbsenteeismCompariosncoll : List<AbsenteeismCompariosn>
    {
        public AbsenteeismCompariosncoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentCvsPCompariosn : ResponeValues
    {
        public string ClassE { get; set; } = " ";
        public int? AbsentData2080SCvsP { get; set; }
        public int? AbsentData2081SCvsP { get; set; }
    }
    public class StudentCvsPCompariosncoll : List<StudentCvsPCompariosn>
    {
        public StudentCvsPCompariosncoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeeCvsPCompariosn : ResponeValues
    {
        public string DepartmentE { get; set; } = " ";
        public int? AbsentData2080ECvsP { get; set; }
        public int? AbsentData2081ECvsP { get; set; }
    }
    public class EmployeeCvsPCompariosncoll : List<EmployeeCvsPCompariosn>
    {
        public EmployeeCvsPCompariosncoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class HigherAbsenteeism : ResponeValues
    {
        public string MonthHA { get; set; } = " ";
        public int? AbsentSTD { get; set; }
        public int? AbsentEMP { get; set; }
        public int? MonthlyAbsentHA { get; set; }
    }
    public class HigherAbsenteeismcoll : List<HigherAbsenteeism>
    {
        public HigherAbsenteeismcoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class Excessiveabsences : ResponeValues
    {
        public string MonthEA { get; set; } = " ";
        public int? AbsentSTDEA { get; set; }
        public int? AbsentEMPEA { get; set; }
    }
    public class Excessiveabsencescoll : List<Excessiveabsences>
    {
        public Excessiveabsencescoll()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}