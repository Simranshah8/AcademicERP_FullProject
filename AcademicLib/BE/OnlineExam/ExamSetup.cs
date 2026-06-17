using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.OnlineExam
{
    public class ExamSetup : ResponeValues
    {
        public int? ExamSetupId { get; set; }
        public int ExamTypeId { get; set; }
        public int ClassId { get; set; }
        public string SectionIdColl { get; set; }
        public int SubjectId { get; set; }
        public string Lesson { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? StartTime { get; set; }
        public int Duration { get; set; }
        public DateTime? ResultDate { get; set; }
        public DateTime? ResultTime { get; set; }
        public double FullMarks { get; set; }
        public double PassMarks { get; set; }
        public string Instruction { get; set; }
        public bool IsAlerttoStudents { get; set; }
        public bool IsIncludeNegativeMark { get; set; }
        public double DeductMark { get; set; }
        public bool ShuffleQuestions { get; set; }

        private List<ExamSetupQuestionModel> _ExamSetupQuestionModelCollections = new List<ExamSetupQuestionModel>();
        public List<ExamSetupQuestionModel> QuestionModelColl
        {
            get { return _ExamSetupQuestionModelCollections; }
            set { _ExamSetupQuestionModelCollections = value; }
        }
    }
    public class ExamSetupCollections : List<ExamSetup> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


    public class ExamSetupQuestionModel
    {        
        public int CategoryId { get; set; }
        public int NoOfQuestion { get; set; }
        public double Marks { get; set; }
        public double Total { get; set; }


        public int ExamSetupId { get; set; }
        public int SNo { get; set; }
        public string CategoryName { get; set; }
        public int ExamModal { get; set; }
    }
       
}
