using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.AppCMS.Creation
{
    public class CommitteHierarchy : ResponeValues
    {
        public CommitteHierarchy()
        {
            FullName = "";
            Designation = "";
            ContactNo = "";
            Email = "";
            Message = "";
            ImagePath = "";
            Department = "";
            Qualification = "";
            SocialMediaColl = new FounderSocialMediaCollections();
        }
        public int? CommitteHierarchyId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string ImagePath { get; set; }
        public string Department { get; set; }
        public int OrderNo { get; set; }

        public string Qualification { get; set; }
        public FounderSocialMediaCollections SocialMediaColl { get; set; }
        public string GuId { get; set; }
    }
    public class CommitteHierarchyCollections : System.Collections.Generic.List<CommitteHierarchy>
    {
        public CommitteHierarchyCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

 
}
