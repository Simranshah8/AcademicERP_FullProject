using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Dashboard
{

    public class AcademicDashBoard : ResponeValues
    {
        public string Quotes { get; set; } = "";
        public string QuotesPhotoPath { get; set; } = "";

        //For Student Count
        public int? TotalStudent { get; set; }
        public int? TotalFemaleStudent { get; set; }
        public int? TotalMaleStudent { get; set; }
        public int? TotalMonthlyAdmissions { get; set; }
        public int? MonthlyMaleAdmissions { get; set; }
        public int? MonthlyFemaleAdmissions { get; set; }
        public int? TotalYearlyAdmissions { get; set; }
        public int? YearlyMaleAdmissions { get; set; }
        public int? YearlyFemaleAdmissions { get; set; }
        public int? TotalLeftStudent { get; set; }
        public int? LeftMaleStudent { get; set; }
        public int? LeftFemaleStudent { get; set; }
        public int? TotalPassoutStudent { get; set; }
        public int? PassoutMaleStudent { get; set; }
        public int? PassoutFemaleStudent { get; set; }

        //for Employee Count

        public int? TotalEmployee { get; set; }
        public int? TotalFemale { get; set; }
        public int? TotalMale { get; set; }
        public int? TotalNewJoining { get; set; }
        public int? MaleJoining { get; set; }
        public int? FemaleJoining { get; set; }
        public int? TotalLeftEmployee { get; set; }
        public int? LeftMaleEmp { get; set; }
        public int? LeftFemaleEmp { get; set; }
        public int? Teaching { get; set; }
        public int? MaleTeaching { get; set; }
        public int? FemaleTeaching { get; set; }
        public int? NotTeaching { get; set; }
        public int? MaleNotTeaching { get; set; }
        public int? FemaleNotTeaching { get; set; }

        //For Subject Oveview
        public int? TotalSubject { get; set; }
        public int? TotalECA { get; set; }
        public int? TotalNonECA { get; set; }

        //For Student Account
        public int? TotalStudentAcc { get; set; }
        public int? Created { get; set; }
        public int? Remaining { get; set; }
        public int? TotalEmpAccount { get; set; }
        public int? CreatedEmp { get; set; }
        public int? RemainingEmp { get; set; }

        //For Certificates Summary
        public int? TotalCertificate { get; set; }
        public int? TransferCertificate { get; set; }
        public int? CharacterCertificate { get; set; }
        public int? ExtraCertificate { get; set; }
        public int? PhotoUpdated { get; set; }
        public int? PhotoUpdatedEmp { get; set; }
        public int? EmpRemaining { get; set; }
        
        //For PTM
        public int? TotalYearlyPTM { get; set; }
        public int? TotalMonthlyPTM { get; set; }
        public int? MonthlyCreatedPTM { get; set; }
        public int? MonthlyRemainingPTM { get; set; }
        public int? YearlyCreatedPTM { get; set; }
        public int? YearlyRemaining { get; set; }


        public AcademicDashBoard()
        {
            ClassScheduleColl = new ClassScheduleCollections();
            HouseColl = new HouseCollections();
            ClassTeacherColl = new ClassTeacherCollections();
            StudentBirthdaysColl = new StudentBirthdaysCollections();
            EmployeeBirthdaysColl = new EmployeeBirthdaysCollections();
            HODColl = new HODCollections();
            StudentRemarksColl = new StudentRemarksCollections();
            EmployeeRemarksColl = new EmployeeRemarksCollections();
        }
        public ClassScheduleCollections ClassScheduleColl { get; set; }
        public HouseCollections HouseColl { get; set; }
        public ClassTeacherCollections ClassTeacherColl { get; set; }
        public StudentBirthdaysCollections StudentBirthdaysColl { get; set; }
        public EmployeeBirthdaysCollections EmployeeBirthdaysColl { get; set; }
        public HODCollections HODColl { get; set; }
        public StudentRemarksCollections StudentRemarksColl { get; set; }
        public EmployeeRemarksCollections EmployeeRemarksColl { get; set; }

    }
    public class AcademicDashBoardCollections : System.Collections.Generic.List<AcademicDashBoard>
    {
        public AcademicDashBoardCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClassSchedule: ResponeValues
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public TimeSpan? ShiftStartTime { get; set; } 
        public string ShiftStartTimeStr { get; set; } = "";
        public TimeSpan? ShiftEndTime { get; set; } 
        public string ShiftEndTimeStr { get; set; } = "";
        public TimeSpan? StartTime { get; set; } 
        public string StartTimeStr { get; set; } = "";
        public TimeSpan? EndTime { get; set; } 
        public string EndTimeStr { get; set; } = "";
        public string SubjectName { get; set; } = "";
        public string TeacherName { get; set; } = "";

    }

    public class ClassScheduleCollections : System.Collections.Generic.List<ClassSchedule>
    {
        public ClassScheduleCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class House : ResponeValues
    {
        public int? HouseNameId { get; set; }
        public int? TotalStudents { get; set; }
        public int? TotalBoys { get; set; }
        public int? TotalGirls { get; set; }
        public string HouseName { get; set; } = "";
        public string InchargeName { get; set; } = "";
        public string PhoneNo { get; set; } = "";
        public string ColorCode { get; set; } = "";
    }

    public class HouseCollections : System.Collections.Generic.List<House>
    {
        public HouseCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClassTeacher : ResponeValues
    {
        public int? ClassTeacherId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = "";
        public string TeacherName { get; set; } = "";
        public string Designation { get; set; } = "";
        public string PhotoPath { get; set; } = "";
        public string Gender { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public string ContactNo { get; set; } = "";
        public string Address { get; set; } = "";
    }

    public class ClassTeacherCollections : System.Collections.Generic.List<ClassTeacher>
    {
        public ClassTeacherCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentBirthdays : ResponeValues
    {
        public int? StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public string StudentBdayPhoto { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public DateTime DOB_AD { get; set; }
    }
    public class StudentBirthdaysCollections : System.Collections.Generic.List<StudentBirthdays>
    {
        public StudentBirthdaysCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeeBirthdays : ResponeValues
    {
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; } = "";
        public string EmpBdayPhoto { get; set; } = "";
        public string Department { get; set; } = "";
        public DateTime DOB_AD { get; set; }
    }
    public class HOD : ResponeValues
    {
        public int? HODId { get; set; }
        public int? DepartmentId { get; set; }
        public int? HODEmployeeId { get; set; }
        public string HODCode { get; set; } = "";
        public string HODName { get; set; } = "";
        public string Designation_HOD { get; set; } = "";
        public string PhotoPath_HOD { get; set; } = "";
        public string Gender_HOD { get; set; } = "";
        public string ClassName_HOD { get; set; } = "";
        public string SectionName_HOD { get; set; } = "";
        public string ContactNo_HOD { get; set; } = "";
        public string Address_HOD { get; set; } = "";
        public string DepartmentHOD { get; set; } = "";
        public string ClassShift { get; set; } = "";
    }

    public class HODCollections : System.Collections.Generic.List<HOD>
    {
        public HODCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeeBirthdaysCollections : System.Collections.Generic.List<EmployeeBirthdays>
    {
        public EmployeeBirthdaysCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentRemarks : ResponeValues
    {
        public int? TranId { get; set; }
        public int? StudentId { get; set; }
        public double? Point { get; set; }
        public string Remarks { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public string RegNo { get; set; } = "";
        public int? RollNo { get; set; }
        public string RemarksBy { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string PhotoPath { get; set; } = "";
    }
    public class StudentRemarksCollections : System.Collections.Generic.List<StudentRemarks>
    {
        public StudentRemarksCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeeRemarks : ResponeValues
    {
        public int? TranId { get; set; }
        public int? EmployeeId { get; set; }
        public double? PointEmp { get; set; }
        public string Remarks { get; set; } = "";
        public string FilePathEmp { get; set; } = "";
        public string Designation { get; set; } = "";
        public string RemarksBy { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public string PhotoPathEmp { get; set; } = "";
    }
    public class EmployeeRemarksCollections : System.Collections.Generic.List<EmployeeRemarks>
    {
        public EmployeeRemarksCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}

