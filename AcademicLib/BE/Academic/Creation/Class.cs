using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class Class : Common
    {
        public Class()
        {
            ClassYearIdColl = new List<int>();
            ClassSemesterIdColl = new List<int>();
        }
        public int? ClassId { get; set; }    
        public int id
        {
            get
            {
                if(ClassId.HasValue)
                    return ClassId.Value;
                return 0;
            }
        }
     
        public bool ForOnlineRegistration { get; set; }      
        public bool IsPassOut { get; set; }
        public int StartMonthId { get; set; }
        public int EndMonthId { get; set; }
        public string StartMonth { get; set; }
        public string EndMonth { get; set; }
        public int? FacultyId { get; set; }
        public int ClassType { get; set; }

        public string FacultyName { get; set; }

        public int? LevelId { get; set; }        
        public string LevelName { get; set; }

        public List<int> ClassYearIdColl { get; set; }
        public List<int> ClassSemesterIdColl { get; set; }
        public int? RunningAcademicYearId { get; set; }
        public string RunningAcademicYear { get; set; } = "";

        public bool ActiveFeeMapppingMonth { get; set; }
        public List<ClassWiseAcademicMonth> AcademicMonthColl { get; set; } = new List<ClassWiseAcademicMonth>();

        public int? BatchId { get; set; }
        public string Batch { get; set; }
        public int? SemesterId { get; set; }
        public string Semester { get; set; }
        public int? ClassYearId { get; set; }
        public string ClassYear { get; set; }
        public bool IsActive { get; set; }
        public string Board { get; set; }

        public int? AcademicYearId { get; set; }


        public int? ugc_universityId { get; set; }
        public int? ugc_campusId { get; set; }
        public int? ugc_levelId { get; set; }
        public string ugc_levelName { get; set; }
        public int? ugc_facultyId { get; set; }
        public string ugc_facultyName { get; set; }
        public string ugc_programType { get; set; }
        public int? ugc_programId { get; set; }
        public string ugc_programName { get; set; }
        public string ugc_duration { get; set; }
        public string Faculty { get; set; }

        public bool SubjectTeacher { get; set; }
        public bool ClassTeacher { get; set; }
        public bool CoOrdinator { get; set; }
        public bool HOD { get; set; }
        public string Role { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
    public class ClassCollections : System.Collections.Generic.List<Class>
    {
        public ClassCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class ClassSection
    {
        public ClassSection()
        {
            ClassName = "";
            SectionName = "";
        }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int ClassSNo { get; set; }
        public int SectionSNo { get; set; }

        public string text
        {
            get
            {
                //string BatchDet = "";
                //if (!string.IsNullOrEmpty(Batch))
                //    BatchDet = Batch;

                //if (!string.IsNullOrEmpty(Semester))
                //    BatchDet = BatchDet+" "+Semester;

                //if (!string.IsNullOrEmpty(ClassYear))
                //    BatchDet = BatchDet+" "+ClassYear;

                //return (ClassName.Trim() + (SectionName.Trim().Length>0 ? "-" + SectionName : "")+" "+BatchDet).Trim();

                return ClassName.Trim() + (SectionName.Trim().Length > 0 ? "-" + SectionName : ""); 
            }
        }
        public int NoOfStudent { get; set; }
        public bool FilterSection { get; set; }
        public int ClassType { get; set; }

        public int? BatchId { get; set; }
        public string Batch { get; set; }
        public int? SemesterId { get; set; }
        public string Semester { get; set; }
        public int? ClassYearId { get; set; }
        public string ClassYear { get; set; }

        public bool SubjectTeacher { get; set; }
        public bool ClassTeacher { get; set; }
        public bool CoOrdinator { get; set; }
        public bool HOD { get; set; }
        public string Role { get; set; } = "";
    }
    public class ClassSectionCollections : System.Collections.Generic.List<ClassSection>
    {
        public ClassSectionCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class ClassSectionList
    {
        public ClassCollections ClassList { get; set; }
        public ClassSectionCollections SectionList { get; set; }

        public ClassSectionCollections SectionListWithClass { get; set; }

        public ClassSectionCollections SectionListOnly { get; set; }

        public ClassSectionList()
        {
            SectionListWithClass = new ClassSectionCollections();
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


    public class ClassWiseAcademicMonth
    {
        public int MonthId { get; set; }
        public string Name { get; set; }
        public int MSNo { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
