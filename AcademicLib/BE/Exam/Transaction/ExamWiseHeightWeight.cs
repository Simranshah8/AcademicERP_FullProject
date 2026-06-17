using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Exam.Transaction
{
    public class ExamWiseHeightWeight : ResponeValues
    {
        public int StudentId { get; set; }
        public int ExamTypeId { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Remarks { get; set; }
        public int TranId { get; set; }
        public int FromExamTypeId { get; set; }
        public int ToExamTypeId { get; set; }
        public int UserId { get; set; }
        public string FromExamType { get; set; }
        public string ToExamType { get; set; }
        public string UserName { get; set; }
        public DateTime TransferDate { get; set; }
        public string TransferDateBS { get; set; }
    }
    public class ExamWiseHeightWeightCollections : System.Collections.Generic.List<ExamWiseHeightWeight>
    {
        public ExamWiseHeightWeightCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}