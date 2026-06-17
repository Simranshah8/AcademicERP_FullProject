using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{

    public class AssignmentVal
    {

    }

    public class AssignmentColl
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int Total { get; set; }
        public List<Assignment> DataColl { get; set; }

    }
    public class AssignmentType  
    {
        public int AssignmentTypeId { get; set; }
        public int OrderNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class AssignmentChecked
    {
        public int StudentId { get; set; }
        public int HomeWorkId { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public string[] FilesColl { get; set; }
        public HttpFileCollectionBase SelectedFiles { get; set; }
        public string ObtainGrade { get; set; }
        public double ObtainMark { get; set; }
    }
    public class AssignmentList
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int Total { get; set; }

        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

        private List<Assignment> _DataCollection = new List<Assignment>();
        public List<Assignment> DataColl
        {
            get { return _DataCollection; }
            set { _DataCollection = value; }
        }
    }

    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int AssignmentTypeId { get; set; }    
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string SectionIdColl { get; set; }
        public int SubjectId { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? DeadlineTime { get; set; }
        //not correct data type  DeadlineforRedo and DeadlineforRedoTime 
        public DateTime? DeadlineforRedo { get; set; }
        public DateTime? DeadlineforRedoTime { get; set; }
        //   public int? DeadlineForReDoTime { get; set; }
        public bool IsAllowLateSibmission { get; set; }
        //
        public HttpPostedFileBase file1 { get; set; }
        public HttpFileCollectionBase SelectedFiles { get; set; }       
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public string AssignmentType { get; set; }
        public string Lession { get; set; }
        public string TeacherName { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherContactNo { get; set; }
        public int NoOfAttachment { get; set; }
        public string Attachments { get; set; }
        public DateTime AsignDateTime_AD { get; set; }
        public string AsignDateTime_BS { get; set; }
        public DateTime DeadlineDate_AD { get; set; }
        public string DeadlineDate_BS { get; set; }
        public int TotalStudent { get; set; }
        public int NoOfSubmit { get; set; }
        public int TotalChecked { get; set; }
        public string HomeWorkStatus { get; set; }

        public bool SubmissionsRequired { get; set; }

        public string RegNo { get; set; }
        public List<string> AttachmentColl { get; set; }
        public List<string> StudentAttachmentColl { get; set; }

        public int MarkScheme { get; set; }
        public double Marks { get; set; }
        public double ObtainMark { get; set; }
        public string ObtainGrade { get; set; }

        public string OM { get; set; }
    }


    public class AssignmentById
    {
        public int AssignmentId { get; set; }
        public string AssignmentType { get; set; }
        public DateTime AssignDateTime_AD { get; set; }
        public string AssignDate_BS { get; set; }
        public DateTime DeadlineDate_AD { get; set; }
        public string DeadlineDate_BS { get; set; }
        public string DeadlineTime { get; set; }
        public string Topic { get; set; }
        public string Title { get; set; }
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
        public string UserName { get; set; }
        public string SubjectName { get; set; }

        public string RegNo { get; set; }

        public int MarkScheme { get; set; }
        public double Marks { get; set; }
        public double ObtainMark { get; set; }
        public string ObtainGrade { get; set; }
        public string OM { get; set; }
    }




}
