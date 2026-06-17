using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class StudentForMarkEntry
    {
        public StudentForMarkEntry()
        {
            Name = "";
            RegdNo = "";
            BoardRegdNo = "";
            SymbolNo = "";
            PhotoPath = "";
            Remarks = "";
            SubjectRemarks = "";
        }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegdNo { get; set; }
        public string SymbolNo { get; set; }
        public string PhotoPath { get; set; }
        public double CRTH { get; set; }
        public double CRPR { get; set; }
        public double FMTH { get; set; }
        public double FMPR { get; set; } 
        public double PMTH { get; set; }
        public double PMPR { get; set; }
        public int PaperType { get; set; }
        public string ObtainMarkTH { get; set; }
        public string ObtainMarkPR { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public string Remarks { get; set; }
        public string SubjectRemarks { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }

        public int SubjectType { get; set; }
        public string Batch { get; set; }
        public string Faculty { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }

        public int OTH { get; set; }
        public int OPR { get; set; }
        public bool IsInclude { get; set; }
    }

    public class StudentForMarkEntryCollections : System.Collections.Generic.List<StudentForMarkEntry>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
