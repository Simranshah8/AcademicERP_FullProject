using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.HomeWork
{
   public class Assignment: ResponeValues
    {
        public int? AssignmentId { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string SectionIdColl { get; set; }
        public int SubjectId { get; set; }
        public int AssignmentTypeId { get; set; }
        public string Title { get; set; }
        public string Weblink { get; set; }
        public string Description { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? DeadlineTime { get; set; }
        public DateTime? DeadlineforRedo { get; set; }
        public DateTime? DeadlineforRedoTime { get; set; }
        public int MarkScheme { get; set; }
        public double Marks { get; set; }
      
        public bool IsAllowLateSibmission { get; set; }
        public Assignment()
        {
            AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }

        public string Lesson { get; set; }
        public string Topic { get; set; }
        public bool SubmissionsRequired { get; set; }
        public string Grade { get; set; }

        //Added By Suresh on Falgun 1 starts
        public int? BatchId { get; set; }
        public int? ClassYearId { get; set; }
        public int? SemesterId { get; set; }
        //Added
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string AssignmentTypeName { get; set; }
        public string SubjectName { get; set; }
        public string BatchName { get; set; }
        public string ClassYearName { get; set; }
        public string SemesterName { get; set; }
        public string SectionListName { get; set; }
    }
    public class AssignmentCollections : List<Assignment> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
