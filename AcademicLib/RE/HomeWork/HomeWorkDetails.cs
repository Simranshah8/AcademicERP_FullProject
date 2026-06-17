using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.HomeWork
{
    public class HomeWorkDetails
    {
        public HomeWorkDetails()
        {
            CheckedRemarks = "";
            Status = "";
            HomeWorkType = "";        
            AssignDate_BS = "";

            DeadlineDate_BS = "";
            DeadlineTime = "";
            Topic = "";
            Description = "";
        Name = "";
        ClassName = "";
        Sectionname = "";
        Address = "";
        FatherName = "";
        F_ContactNo = "";
        PhotoPath = "";
        SubmitStatus = "";
        CheckStatus = "";
        Attachments = "";
        StudentAttachments = "";
        StudentNotes = "";        
        SubmitDate_BS = "";        
        ReSubmitDate_BS = "";        
        CheckeDate_BS = "";        
        ReCheckeDate_BS = "";
        CheckedBy = "";
            UserName = "";
            SubjectName = "";
            ReCheckedRemarks = "";
            ReNotes = "";
            Lesson = "";
            RegNo = "";
            reSubmitStudentAttachments = "";
            AssignDateTime_AD = DateTime.Today;
            DeadlineDate_AD = DateTime.Today;
        }
        public string HomeWorkType { get; set; }
        public DateTime AssignDateTime_AD { get; set; }
        public string AssignDate_BS { get; set; }
        public DateTime DeadlineDate_AD { get; set; }
        public string DeadlineDate_BS { get; set; }
        public string DeadlineTime { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string Sectionname { get; set; }
        public int RollNo { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string PhotoPath { get; set; }
        public int NoOfStudent { get; set; }
        public string SubmitStatus { get; set; }
        public string CheckStatus { get; set; }
        public int NoOfSubmit { get; set; }
        public int NoOfDone { get; set; }
        public int NoOfReDo { get; set; }
        public int TotalChecked { get; set; }
        public int NoOfAttachment { get; set; }
        public string Attachments { get; set; }
        public string StudentAttachments { get; set; }
        public string StudentNotes { get; set; }
        public DateTime? SubmitDateTime_AD { get; set; }
        public string SubmitDate_BS { get; set; }
        public DateTime? ReSubmitDateTime_AD { get; set; }
        public string ReSubmitDate_BS { get; set; }
        public DateTime? CheckedDateTime_AD { get; set; }
        public string CheckeDate_BS { get; set; }
        public DateTime? ReCheckedDateTime_AD { get; set; }
        public string ReCheckeDate_BS { get; set; }
        public string CheckedBy { get; set; }
        public string Status { get; set; }
        public string CheckedRemarks { get; set; }
        public string ReCheckedRemarks { get; set; }
        public string UserName { get; set; }
        public string SubjectName { get; set; }

        public string RegNo { get; set; }
        public string ReNotes { get; set; }
        public string reSubmitStudentAttachments { get; set; }
        public string Lesson { get; set; }

        //Added By Suresh
        public string Batch { get; set; }
        public string ClassYear { get; set; }
        public string Semester { get; set; }
    }
    public class HomeWorkDetailsCollections : System.Collections.Generic.List<HomeWorkDetails>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
