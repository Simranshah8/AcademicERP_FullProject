using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class HostelStudent
    {
        public int SNo { get; set; }
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; } 
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public string FatherName { get; set; } 
        public string ContactNo { get; set; }
        public string RoomName { get; set; }
        public string BedName { get; set; }
        public string BedNo { get; set; }
        public double PayableAmt { get; set; }
        public DateTime? AllotDate_AD { get; set; }
        public string AllotDate_BS { get; set; }
    }
    public class HostelStudentCollections: System.Collections.Generic.List<HostelStudent>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
