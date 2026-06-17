using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class StudentForEvaluate
    {
        public int StudentId { get; set; }
        public string RegNo { get; set; }
        public string BoardRegNo { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int QuestionAttampt { get; set; }
        public double Objective_OM { get; set; }
        public double Subjective_OM { get; set; }
        public double Total_OM { get; set; }
        public string Location { get; set; }
        public string IPAddress { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public DateTime? LastSumitDateTime { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public int NoOfFiles { get; set; }
    }
    public class StudentForEvaluateCollections : System.Collections.Generic.List<StudentForEvaluate>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}