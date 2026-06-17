using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.HomeWork
{
    public class Assignment
    {
        public Assignment()
        {
            SectionName = "";
            SubjectName = "";
            AssignmentType = "";
            Title = "";
            Weblink = "";
            Description = "";
            TeacherName = "";
            TeacherAddress = "";
            TeacherContactNo = "";
            Attachments = "";
            AssignmentStatus = "";

            StudentAttachments = "";
            StudentNotes = "";
            ReStudentNotes = "";
            CheckedRemarks = "";
            ReCheckedRemarks = "";

            AttachmentColl = new List<string>();
            StudentAttachmentColl = new List<string>();
            reStudentAttachmentColl = new List<string>();
    }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int AssignmentId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public string AssignmentType { get; set; }
        public string Title { get; set; }
        public string Weblink { get; set; }
        public string Description { get; set; }
        public string TeacherName { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherContactNo { get; set; }
        public int NoOfAttachment { get; set; }
        public string Attachments { get; set; }
        public DateTime AsignDateTime_AD { get; set; }
        public string AsignDateTime_BS { get; set; }
        public DateTime DeadlineDate_AD { get; set; }
        public string DeadlineDate_BS { get; set; }
        public string DeadlineTime { get; set; }
        public int TotalStudent { get; set; }
        public int NoOfSubmit { get; set; }
        public int TotalChecked { get; set; }
        public string AssignmentStatus { get; set; }

        public DateTime? SubmitDateTime_AD { get; set; }
        public string SubmitDate_BS { get; set; }
        public int MarkScheme { get; set; }
        public double Marks { get; set; }
        public double ObtainMark { get; set; }
        public string ObtainGrade { get; set; }

        public int TotalDone { get; set; }
        public int TotalNotDone { get; set; }
        public bool SubmissionsRequired { get; set; }
        public string reSubmitStudentAttachments { get; set; }

        public DateTime? ReSubmitDateTime_AD { get; set; }
        public string ReSubmitDateTime_BS { get; set; }

        public DateTime? DeallineForRedo_AD { get; set; }
        public string DeadlineForRedo_BS { get; set; }
        public DateTime? DeadlineforRedoTime { get; set; }
        public bool IsAllowLateSibmission { get; set; }
        public string StudentAttachments { get; set; }
        public string StudentNotes { get; set; }
        public string ReStudentNotes { get; set; }
        public string CheckedRemarks { get; set; }
        public string ReCheckedRemarks { get; set; }

        public List<string> AttachmentColl { get; set; }
        public List<string> StudentAttachmentColl { get; set; }
        public List<string> reStudentAttachmentColl { get; set; }

        public string Lesson { get; set; }
        public string Topic { get; set; } 
    }

    public class AssignmentCollections : System.Collections.Generic.List<Assignment>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
