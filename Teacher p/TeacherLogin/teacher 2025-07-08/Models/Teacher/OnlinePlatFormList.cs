using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class OnlinePlatForm
    {
        public int PlatformType { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Link { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}