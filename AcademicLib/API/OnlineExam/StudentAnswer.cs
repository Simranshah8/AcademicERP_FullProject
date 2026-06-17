using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.OnlineExam
{
    public class StudentAnswer : ResponeValues
    {

        public int ExamSetupId { get; set; }        
        public int TranId { get; set; }
        public string Location { get; set; }
        public double Lat { get; set; }
        public double Lan { get; set; }
        public string IPAddress { get; set; }
        public string QuestionRemarks { get; set; }
        public int? AnswerSNo { get; set; }
        public string AnswerText { get; set; }
        
        public int? SubmitType { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
        public List<string> StudentDocColl { get; set; }
    }
    
}
