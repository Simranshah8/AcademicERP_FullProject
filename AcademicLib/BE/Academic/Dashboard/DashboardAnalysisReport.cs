using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BE.Academic.Dashboard
{
    public class DashboardAnalysisReport : ResponeValues
    {
        //For Student Count
        public int? TotalEmployee { get; set; }
        public int? TotalNewJoining { get; set; }
        public int? TotalLeftEmployee { get; set; }
        public int? Teaching { get; set; }
        public int? NotTeaching { get; set; }

        public DashboardAnalysisReport()
        {
            StdRecordColl = new StudentRecordCollections();
            StdTpyDistColl = new StudentTypDistributionCollections();
            StdGenderDistColl = new StdGenderDistributionCollections();
            CasteStdColl = new CasteStdCollections();
            AgeWiseStdColl = new AgeWiseStdCollections();
            StdDisabiltyColl = new StdDisabiltyCollections();

            DepartmentWiseEmpColl = new DepartmentWiseEmpCollections();
            LevelColl = new LevelCollections();
            CasteEmpColl = new CasteEmpCollections();
            AgeWiseEmpColl = new AgeWiseEmpCollections();
            EmpDisabiltyColl = new EmpDisabiltyCollections();
            ClassWiseStdEmpColl = new ClassWiseStdEmpCollections();
            BirthdaySummaryColl = new BirthdaySummaryCollections();
            RemarkStdEmpColl = new RemarkStdEmpCollections();
            CretificateSummaryColl = new CretificateSummaryCollections();
            PTMDetailsColl = new PTMDetailsCollections();
        }
        public StudentRecordCollections StdRecordColl { get; set; }
        public StudentTypDistributionCollections StdTpyDistColl { get; set; }
        public StdGenderDistributionCollections StdGenderDistColl { get; set; }
        public CasteStdCollections CasteStdColl { get; set; }
        public AgeWiseStdCollections AgeWiseStdColl { get; set; }
        public StdDisabiltyCollections StdDisabiltyColl { get; set; }

        public DepartmentWiseEmpCollections DepartmentWiseEmpColl { get; set; }
        public LevelCollections LevelColl { get; set; }
        public CasteEmpCollections CasteEmpColl { get; set; }
        public AgeWiseEmpCollections AgeWiseEmpColl { get; set; }
        public EmpDisabiltyCollections EmpDisabiltyColl { get; set; }
        public ClassWiseStdEmpCollections ClassWiseStdEmpColl { get; set; }
        public BirthdaySummaryCollections BirthdaySummaryColl { get; set; }
        public RemarkStdEmpCollections RemarkStdEmpColl { get; set; }
        public CretificateSummaryCollections CretificateSummaryColl { get; set; }
        public PTMDetailsCollections PTMDetailsColl { get; set; }

    }
    public class DashboardAnalysisReportCollections : System.Collections.Generic.List<DashboardAnalysisReport>
    {
        public DashboardAnalysisReportCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentRecord : ResponeValues
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? TotalNewStudent { get; set; } 
        public int? TotalLeftStudent { get; set; } 
        public int? TotalPassoutStudent { get; set; } 
        public int? TotalOldStudent { get; set; } 
    }
    public class StudentRecordCollections : System.Collections.Generic.List<StudentRecord>
    {
        public StudentRecordCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentTypDistribution : ResponeValues
    {
        public int? StudentTypeId { get; set; }
        public string StudentTypeName { get; set; } = "";
        public int? TotalSTBoys { get; set; } 
        public int? TotalSTGirls { get; set; } 
    }
    public class StudentTypDistributionCollections : System.Collections.Generic.List<StudentTypDistribution>
    {
        public StudentTypDistributionCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StdGenderDistribution : ResponeValues
    {
        public int? StdGenderClassId { get; set; }
        public string ClassNameStdGender { get; set; } = "";
        public int? StdGenderBoys { get; set; } 
        public int? StdGenderGirls { get; set; } 
    }
    public class StdGenderDistributionCollections : System.Collections.Generic.List<StdGenderDistribution>
    {
        public StdGenderDistributionCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class CasteStd : ResponeValues
    {
        public int? StdCasteId { get; set; }
        public string StdCasteName { get; set; } = "";
        public int? NoOfCasteStd { get; set; }
    }
    public class CasteStdCollections : System.Collections.Generic.List<CasteStd>
    {
        public CasteStdCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class AgeWiseStd : ResponeValues
    {
        public string StdAgeGp { get; set; } = "";
        public int? StdTotalBoys { get; set; }
        public int? StdTotalGirls { get; set; }
    }
    public class AgeWiseStdCollections : System.Collections.Generic.List<AgeWiseStd>
    {
        public AgeWiseStdCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StdDisabilty : ResponeValues
    {
        public string StdPhysicalDisability { get; set; } = "";
        public bool StdIsPhysicalDisability { get; set; }
        public int? BoysWithDisability { get; set; }
        public int? GirlsWithDisability { get; set; }
    }
    public class StdDisabiltyCollections : System.Collections.Generic.List<StdDisabilty>
    {
        public StdDisabiltyCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    //EMploye Part
    public class DepartmentWiseEmp : ResponeValues
    {
        public int? DepartmentId { get; set; }
        public string WiseEmpDepartment { get; set; } = "";
        public int? DepartmentMaleEmp { get; set; } 
        public int? DepartmentFemaleEmp { get; set; } 
    }
    public class DepartmentWiseEmpCollections : System.Collections.Generic.List<DepartmentWiseEmp>
    {
        public DepartmentWiseEmpCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class Level : ResponeValues
    {
        public int? LevelId { get; set; }
        public string LevelName { get; set; } = "";
        public int? NoOfLevel { get; set; } 
    }
    public class LevelCollections : System.Collections.Generic.List<Level>
    {
        public LevelCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class CasteEmp : ResponeValues
    {
        public int? EmpCasteId { get; set; }
        public string EmpCasteName { get; set; } = "";
        public int? NoOfCasteEmp { get; set; } 
    }
    public class CasteEmpCollections : System.Collections.Generic.List<CasteEmp>
    {
        public CasteEmpCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class AgeWiseEmp : ResponeValues
    {
        public string EmpAgeGp { get; set; } = "";
        public int? EmpTotalMale { get; set; } 
        public int? EmpTotalFemale { get; set; } 
    }
    public class AgeWiseEmpCollections : System.Collections.Generic.List<AgeWiseEmp>
    {
        public AgeWiseEmpCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmpDisabilty : ResponeValues
    {
        public string EmpPhysicalDisability { get; set; } = "";
        public bool EmpIsPhysicalDisability { get; set; }
        public int? MaleWithDisability { get; set; }
        public int? FemaleWithDisability { get; set; }
    }
    public class EmpDisabiltyCollections : System.Collections.Generic.List<EmpDisabilty>
    {
        public EmpDisabiltyCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClassWiseStdEmp : ResponeValues
    {
        public int? StdEmpClassId { get; set; }
        public string StdEmpClassName { get; set; } = "";
        public int? TotalStdClass { get; set; } 
        public int? TotalEmpClass { get; set; } 
    }
    public class ClassWiseStdEmpCollections : System.Collections.Generic.List<ClassWiseStdEmp>
    {
        public ClassWiseStdEmpCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class BirthdaySummary : ResponeValues
    {
        public int? BirthMonth { get; set; }
        public string BDayMonthName { get; set; } = "";
        public int? TotalStdBDay { get; set; } 
        public int? TotalEmpBDay { get; set; } 
    }
    public class BirthdaySummaryCollections : System.Collections.Generic.List<BirthdaySummary>
    {
        public BirthdaySummaryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class RemarkStdEmp : ResponeValues
    {
        public int? StdEmpRemarkMonth { get; set; }
        public string RemarkMonthName { get; set; } = "";
        public int? TotalStdRemark { get; set; } 
        public int? TotalEmpRemark { get; set; } 
    }
    public class RemarkStdEmpCollections : System.Collections.Generic.List<RemarkStdEmp>
    {
        public RemarkStdEmpCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class CretificateSummary : ResponeValues
    {
        public string CertiMonthName { get; set; } = "";
        public int? TrasnferCertificates { get; set; } 
        public int? CharacterCertificates { get; set; } 
        public int? ExtraCertificates { get; set; } 
    }
    public class CretificateSummaryCollections : System.Collections.Generic.List<CretificateSummary>
    {
        public CretificateSummaryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class PTMDetails : ResponeValues
    {
        public int? MeetingMonth { get; set; }
        public int? TotalMeetings { get; set; }
        public string MonthName { get; set; } = "";
    }
    public class PTMDetailsCollections : System.Collections.Generic.List<PTMDetails>
    {
        public PTMDetailsCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}

