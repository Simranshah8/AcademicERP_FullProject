using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class StudentRemarks 
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; } 
        public DateTime ForDate { get; set; }
        public string ForMiti { get; set; }
        public string RemarsType { get; set; }
        public string Remarks { get; set; }
        public string FilePath { get; set; }
        public string UserName { get; set; }
        public string UserPhotoPath { get; set; }
        public string RemarksFor { get; set; }
        public double Point { get; set; }
        public int NY { get; set; }
        public string NM { get; set; }
        public int ND { get; set; }
        public DateTime? LogDate { get; set; }
        public string LogMiti { get; set; }
        public string MonthName { get; set; }
        public string RemarksBy { get; set; }

    }
    public class StudentRemarksCollections : System.Collections.Generic.List<StudentRemarks>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
