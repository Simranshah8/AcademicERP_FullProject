using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.AppCMS.Creation
{
   public class AboutUs : ResponeValues
    {
        public AboutUs()
        {
            ImagePath = "";
            BannerPath = "";
            Content = "";
            LogoPath = "";
            AffiliatedLogo = "";
        }
        public int? AboutUsId { get; set; }        
        public string LogoPath { get; set; }        
        public string ImagePath { get; set; }        
        public string BannerPath { get; set; }
        public string Content { get; set; }        
        public string AffiliatedLogo { get; set; }
        public string GuId { get; set; }
        public string SchoolPhoto { get; set; } = "";
    }
    public class AboutUsCollections : List<AboutUs> 
    {
        public AboutUsCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}