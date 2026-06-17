using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.OnlineExam
{
    public class ExamSetup
    {
        public int ExamSetupId { get; set;  }
        public string ExamTypeName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public string Lesson { get; set; }
        public DateTime ExamDate_AD { get; set; }
        public string ExamDate_BS { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public DateTime ResultDate_AD { get; set; }
        public string ResultDate_BS { get; set; }
        public DateTime ResultTime { get; set; }
        public double FullMark { get; set; }
        public double PassMark { get; set; } 
        public string Instruction { get; set; }
        public bool IsAlterToStudents { get; set; }
        public bool IsIncludeNegativeMark { get; set; }
        public double DeductMark { get; set; }
        public string TeacherName { get; set; }
        public bool ShuffleQuestions { get; set; }

    }

    public class ExamSetupCollections : System.Collections.Generic.List<ExamSetup>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

   

}
