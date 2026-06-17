using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class TeacherLogin
    {
        public string Password { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string userName { get; set; }
        public string userGroup { get; set; }
        public int userId { get; set; }
        public string customerCode { get; set; }

        public string photoPath { get; set; }
        public string name { get; set; }
    }
}