using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models
{
    public class ReportTempletes 
    {

        public string EntityName { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public bool IsDefault { get; set; }
        public string APIPath { get; set; }
        public int SNO { get; set; }
        public bool IsTransaction { get; set; }
        public int RptTranId { get; set; }
        public string FileName { get; set; }
        public bool ForEmail { get; set; }
        public bool IsActive { get; set; }
        public int? Rpt_Type { get; set; }
    }
}