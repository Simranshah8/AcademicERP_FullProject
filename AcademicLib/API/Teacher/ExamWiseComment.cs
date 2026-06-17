using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class ExamWiseComment
    {
        public int StudentId { get; set; }
        public int ExamTypeId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string PhotoPath { get; set; }
        public string SymbolNo { get; set; }
        public double FM { get; set; }
        public double PM { get; set; }
        public double ObtainMark { get; set; }
        public double TotalFail { get; set; }
        public double ObtainPer { get; set; }
        public double AVGGP { get; set; }
        public string Division { get; set; }
        public string Grade { get; set; }
        public string GP_Grade { get; set; }
        public bool IsFail { get; set; }
        public int RankInClass { get; set; }
        public int RankInSection { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public string Result { get; set; }
        public string TeacherComment { get; set; }
    }

     public class ExamWiseCommentCollections : System.Collections.Generic.List<ExamWiseComment>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
