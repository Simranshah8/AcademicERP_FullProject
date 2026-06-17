using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public class ExamRoomAttendance : ResponeValues
    {       
        public int? TranId { get; set; }
        public DateTime ExamDate { get; set; }        
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int? Attendance { get; set; }       
        public string Remarks { get; set; }     
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }       
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Batch { get; set; }
        public string Factulty { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public int? ExamTypeId { get; set; }
        public int? ExamShiftId { get; set; }
        public int? RoomId { get; set; }
        public string SymbolNo { get; set; }
        public string SubjectName { get; set; }
        public string PaperType { get; set; }

    }
    public class ExamRoomAttendanceCollections : List<ExamRoomAttendance>
    {
        public ExamRoomAttendanceCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

   
}
