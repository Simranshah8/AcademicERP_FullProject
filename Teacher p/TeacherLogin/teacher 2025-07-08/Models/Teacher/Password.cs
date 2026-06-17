using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class Password
    {
        public string oldPwd { get; set; }
        public string newPwd { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}