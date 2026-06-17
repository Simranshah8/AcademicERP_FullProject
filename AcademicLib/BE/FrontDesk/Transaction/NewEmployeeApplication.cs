using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class NewEmployeeApplication : ResponeValues
    {
        public int? EmployeeId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime ApplictionDate { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateTime DOB { get; set; }
        public string ContactNo { get; set; }
        public int Religion { get; set; }
        public int Nationality { get; set; }
        public string Email { get; set; }
        public int MaritalStatus { get; set; }
        public string FullAddress { get; set; }
        public bool ISTeacher { get; set; }
        public int Source { get; set; }
        public decimal SalaryExpectation { get; set; }
        public int SubjectId { get; set; }
        public int LevelId { get; set; }
        private NewEmployeeQualificationCollection _NewEmployeeQualificationCollection = new NewEmployeeQualificationCollection();
        public NewEmployeeQualificationCollection NewEmployeeQualificationColl
        {
            get
            {
                return _NewEmployeeQualificationCollection;
            }
            set
            {
                _NewEmployeeQualificationCollection = value;
            }
        }
        private NewEmployeeWorkExperienceCollections _NewEmployeeWorkExperienceCollections = new NewEmployeeWorkExperienceCollections();
        public NewEmployeeWorkExperienceCollections NewEmployeeWorkExperienceColl
        {
            get
            {
                return _NewEmployeeWorkExperienceCollections;
            }
            set
            {
                _NewEmployeeWorkExperienceCollections = value;
            }
        }
        private NewEmployeeReferenceCollections _NewEmployeeReferenceCollections = new NewEmployeeReferenceCollections();
        public NewEmployeeReferenceCollections NewEmployeeReferenceColl
        {
            get
            {
                return _NewEmployeeReferenceCollections;
            }
            set
            {
                _NewEmployeeReferenceCollections = value;
            }
        }
        private NewEmployeeAttachDocumentCollections _NewEmployeeAttachDocumentCollections = new NewEmployeeAttachDocumentCollections();
        public NewEmployeeAttachDocumentCollections NewEmployeeAttachDocumentColl
        {
            get
            {
                return _NewEmployeeAttachDocumentCollections;
            }
            set
            {
                _NewEmployeeAttachDocumentCollections = value;
            }
        }

    }
    public class NewEmployeeApplicationCollections : List<NewEmployeeApplication> {
        public NewEmployeeApplicationCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class NewEmployeeQualification {
    public int TranId { get; set; }
    public int NewEmployeeId { get; set; }
    public string Name { get; set; }
    public string BoardUniversity { get; set; }
    public string PassedYear { get; set; }
    public string GradePercentage { get; set; }
    }
    public class NewEmployeeQualificationCollection : List<NewEmployeeQualification> { }

    public class NewEmployeeWorkExperience
    {
        public int TranId { get; set; }
        public int NewEmployeeId { get; set; }
        public string Organization { get; set; }
        public int DepartmentId { get; set; }
        public string JobTitle { get; set; }
        public string WorkDuration { get; set; }
        public string Remarks { get; set; }
    }
    public class NewEmployeeWorkExperienceCollections : List<NewEmployeeWorkExperience> { }
    public class NewEmployeeReference
    {
        public int TranId { get; set; }
        public int NewEmployeeId { get; set; }
        public string ReferencePerson { get; set; }
        public int DesignationId { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Organiozation { get; set; }

    }
    public class NewEmployeeReferenceCollections : List<NewEmployeeReference> { }
    public class NewEmployeeAttachDocument
    {
        public int TranId { get; set; }
        public int NewEmployeeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string AttachDocumet { get; set; }
        public string AttachDocumentPath { get; set; }
        public string Description { get; set; }
    }
    public class NewEmployeeAttachDocumentCollections : List<NewEmployeeAttachDocument> { }
}
