using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class HomeWorkChecked
    {
        public int StudentId { get; set; }
        public int HomeWorkId { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }      
        public string[] FilesColl { get; set; }
        public HttpFileCollectionBase SelectedFiles { get; set; }
    }
}