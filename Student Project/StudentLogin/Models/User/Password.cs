using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models.User
{
    public class Password
    {
        public string oldPwd { get; set; }
        public string newPwd { get; set; }
        public string Conform { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}