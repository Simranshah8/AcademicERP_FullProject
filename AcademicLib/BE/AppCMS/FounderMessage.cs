using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.AppCMS.Creation
{
    public class FounderMessage : ResponeValues
    {
        public FounderMessage()
        {
            Title = "";
            Description = "";
            ImagePath = "";
            Qualification = "";
            FounderSocialMediaColl = new FounderSocialMediaCollections();
        }
        public int? FounderMessageId { get; set; }

        public string Designation { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OrderNo { get; set; }

        public string Qualification { get; set; }
        public string SubmenuTitle { get; set; } = "";
        public string Name { get; set; }
        public FounderSocialMediaCollections FounderSocialMediaColl { get; set; }
        public string GuId { get; set; }

    }
    public class FounderMessageCollections : System.Collections.Generic.List<FounderMessage>
    {
        public FounderMessageCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class FounderSocialMedia
    {
        public int SocialMediaId { get; set; }
        public string UrlPath { get; set; }
        public bool IsActive { get; set; }

        public string Name { get; set; }
        public string IconPath { get; set; }
        public string ThumbnailPath { get; set; }
        public string GuId { get; set; }
    }
    public class FounderSocialMediaCollections : System.Collections.Generic.List<FounderSocialMedia> { }
}
