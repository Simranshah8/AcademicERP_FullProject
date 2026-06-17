using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.AppCMS.Creation
{
    public class Testimonial : ResponeValues
    {
        public Testimonial()
        {
            Title = "";
            Description = "";
            ImagePath = "";
            Qualification = "";
            TestimonialSocialMediaColl = new TestimonialSocialMediaCollections();
        }
        public int? TestimonialId { get; set; }

        public string Designation { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OrderNo { get; set; }

        public string Qualification { get; set; }
        public string SubmenuTitle { get; set; } = "";
        public string Name { get; set; } = "";
        public TestimonialSocialMediaCollections TestimonialSocialMediaColl { get; set; }

    }
    public class TestimonialCollections : System.Collections.Generic.List<Testimonial>
    {
        public TestimonialCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class TestimonialSocialMedia
    {
        public int SocialMediaId { get; set; }

        public string IconPath { get; set; }
        public string ThumbnailPath { get; set; }
        public string UrlPath { get; set; }
        public bool IsActive { get; set; }
    }
    public class TestimonialSocialMediaCollections : System.Collections.Generic.List<TestimonialSocialMedia> { }
}
