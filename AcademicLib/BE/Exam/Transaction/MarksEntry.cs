using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
   public class MarksEntry : ResponeValues
    {
        public int MarksEntryId { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public DateTime TestDate { get; set; }
        public bool IsColumnwiseFocus { get; set; }
        public int SubjectId { get; set; }
        public int FullMarksTH { get; set; }
        public int FullMarksPR { get; set; }
        public int PassMarksTH { get; set; }
        public int PassMarksPR { get; set; }

        private List<MarksEntryDetails> _MarksEntryDetails = new List<MarksEntryDetails>();
            public List<MarksEntryDetails> MarksEntryDetailsColl
        { get { return _MarksEntryDetails; } set { _MarksEntryDetails = value; } }

    }
    public class MarksEntryCollections : List<MarksEntry> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public  class MarksEntryDetails
    {
        public int TranId { get; set; }
        public int MarksEntryId { get; set; }
        public int StudentId { get; set; }
        public float ObtainMarksTH { get; set; }
        public float ObtainMarksPR { get; set; }

    }

    public class ImportMarkEntry
    {
        public string ExamType { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegdNo { get; set; }
        public string SymbolNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string SubjectName { get; set; }
        public string PaperType { get; set; }
        public string ObtainMarkTH { get; set; }
        public string ObtainMarkPR { get; set; }
        public string GraceMarkTH { get; set; }
        public string GraceMarkPR { get; set; }
        public string Remarks { get; set; }
    }

    public class ImportAllSubjectMarkEntry
    {
        public string RegdNo { get; set; }
        public string SymbolNo { get; set; }
        public string BoardRegdNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string ExamType { get; set; } 
        public string Sub_TH1 { get; set; }
        public string Sub_PR1 { get; set; }
        public string Sub_TH2 { get; set; }
        public string Sub_PR2 { get; set; } 
        public string Sub_TH3 { get; set; }
        public string Sub_PR3 { get; set; }
        public string Sub_TH4 { get; set; }
        public string Sub_PR4 { get; set; }
        public string Sub_TH5 { get; set; }
        public string Sub_PR5 { get; set; }
        public string Sub_TH6 { get; set; }
        public string Sub_PR6 { get; set; } 
        public string Sub_TH7 { get; set; } 
        public string Sub_PR7 { get; set; }
        public string Sub_TH8 { get; set; }
        public string Sub_PR8 { get; set; }
        public string Sub_TH9 { get; set; } 
        public string Sub_PR9 { get; set; } 
        public string Sub_TH10 { get; set; }
        public string Sub_PR10 { get; set; }
        public string Sub_TH11 { get; set; } 
        public string Sub_PR11 { get; set; }
        public string Sub_TH12 { get; set; }
        public string Sub_PR12 { get; set; }

    }
}
