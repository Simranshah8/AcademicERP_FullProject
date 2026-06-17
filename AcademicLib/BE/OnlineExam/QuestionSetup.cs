using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.OnlineExam
{
    public class QuestionSetup : ResponeValues
    {
        public QuestionSetup()
        {
            Question = "";
            QuestionPath = "";
            CategoryName = "";
        }
        public int TranId { get; set; }
        public int ExamSetupId { get; set; }
        public int CategoryId { get; set; }
        public int QNo { get; set; }
        public double Marks { get; set; }
        public int QuestionTitle { get; set; }
        public int AnswerTitle { get; set; }
        public string Question { get; set; }
        public string QuestionPath { get; set; }
        public int AnswerSNo { get; set; }

        private List<QuestionSetupDetails> _DetailsColl = new List<QuestionSetupDetails>();
        public List<QuestionSetupDetails> DetailsColl
        {
            get { return _DetailsColl; }
            set { _DetailsColl = value; }
        }

        public string CategoryName { get; set; }
        public int ExamModal { get; set; }

        public int? SubmitType { get; set; }
        public string QuestionRemarks { get; set; } 
        public int? StudentAnswerNo { get; set; }
        public string AnswerText { get; set; }

        public string FileType { get; set; }
        public int FileCount { get; set; }

        public string StudentDocsPath { get; set; }
        public List<string> StudentDocColl { get; set; }

        public string TeacherName { get; set; }

        public bool IsCorrect { get; set; }

        public int? OETranId { get; set; }

        public double ObtainMark { get; set; }
        public string Remarks { get; set; }

        public bool IsChecked { get; set; }
    }
    public class QuestionSetupCollections : List<QuestionSetup> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class QuestionSetupDetails
    {
        public QuestionSetupDetails()
        {
            Answer = "";
            FilePath = "";
            AnswerTitle = 1;
        }
        public int SNo { get; set; }
        public string Answer { get; set; }
        public string FilePath { get; set; }
        public bool IsRightAnswer { get; set; }

        public int AnswerTitle { get; set; }
        public string SNo_Str { get; set; }
        public bool IsCorrect { get; set; }
        public int? OETranId { get; set; }
        public string FileType { get; set; }
        public int FileCount { get; set; }
    }

    public enum QUESTIONTITLES
    {
        TEXT=1,
        IMAGE=2,
        AUDIO=3,
        VEDIO=4
    }
}
