using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models
{
    public class RemarkType
    {
        public int RemarksTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}