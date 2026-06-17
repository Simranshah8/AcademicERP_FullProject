using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class StudentDetailsForLeft
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }

        public int UserId { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public bool IsLeft { get; set; }
        public string BoardRegdNo { get; set; }
        public int? BoardTypeId { get; set; }
        public string BoardName { get; set; }
        public DateTime? LeftDate_AD { get; set; }
        public string LeftDate_BS { get; set; }
        public string LeftRemarks { get; set; }

        public string FatherName { get; set; }
        public string FatherContact { get; set; }

        public string Address { get; set; }
        public string Gender { get; set; }

        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Batch { get; set; }
        public int? BatchId { get; set; }
        public int? StatusId { get; set; }
    }
    public class StudentDetailsForLeftCollections : System.Collections.Generic.List<StudentDetailsForLeft>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
