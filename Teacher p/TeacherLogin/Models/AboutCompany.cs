using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models
{
    public class AboutCompany
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string EmailId { get; set; }
        public string WebSite { get; set; }
        public string LogoPath { get; set; }
        public string ImagePath { get; set; }
        public string BannerPath { get; set; } 
        public string Content { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}