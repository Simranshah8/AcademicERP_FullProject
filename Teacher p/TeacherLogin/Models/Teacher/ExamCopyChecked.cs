using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class ExamCopyChecked
    {
        public int? OETranId { get; set; }
        public double ObtainMark { get; set; }
        public string Remarks { get; set; }
        public string[] FilesColl { get; set; }
        public HttpFileCollectionBase SelectedFiles { get; set; }
    }
}