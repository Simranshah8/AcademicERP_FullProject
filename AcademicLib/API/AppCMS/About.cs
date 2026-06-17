using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.AppCMS
{
    public class About : ResponeValues
    {
        public About()
        {
            Name = "";
            Address = "";
            PhoneNo = "";
            FaxNo = "";
            EmalId = "";
            WebSite = "";
            LogoPath = "";
            ImagePath = "";
            BannerPath = "";
            Content = "";
            AffiliatedLogo = "";
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string EmalId { get; set; }
        public string WebSite { get; set; }
        public string LogoPath { get; set; }
        public string ImagePath { get; set; }
        public string BannerPath { get; set; }
        public string Content { get; set; }
        public string Country { get; set; } = "";
        public string AffiliatedLogo { get; set; } = "";
        public string Affiliated { get; set; } = "";
        public string Slogan { get; set; } = "";
        public string Established { get; set; } = "";
        public string SchoolPhoto { get; set; } = "";

        public string HD_LogoPhoto { get; set; } = "";
        public string HD_CompanyName { get; set; } = "";
        public string HD_Slogan { get; set; } = "";
        public bool? HD_HeaderIsActive { get; set; }
        public bool? HD_NameIsActive { get; set; }
        public bool? HD_SloganIsActive { get; set; }
    }
}
