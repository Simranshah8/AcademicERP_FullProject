using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models.Students
{
    public class AssignmentVal
    {

    }
    public class SubmitAssignment
    {
        public int AssignmentId { get; set; }
        public string Notes { get; set; }
        public HttpPostedFileBase file1 { get; set; }
        public HttpFileCollectionBase SelectedFiles { get; set; }
    }
    public class Assignment
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int AssignmentId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public string AssignmentType { get; set; }        
        public string Title { get; set; }
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
        public DateTime? DeallineForRedo_AD { get; set; }
        public string DeadlineForRedo_BS { get; set; }
        public object DeadlineforRedoTime { get; set; }
        public bool IsAllowLateSibmission { get; set; }
        public string StudentAttachments { get; set; }
        public string StudentNotes { get; set; }
        public object ReStudentNotes { get; set; }
        public string CheckedRemarks { get; set; }
        public object ReCheckedRemarks { get; set; }
        public int MarkScheme { get; set; }
        public double Marks { get; set; }
        public double ObtainMark { get; set; }
        public object ObtainGrade { get; set; }
        public string OM { get; set; }
    }
}