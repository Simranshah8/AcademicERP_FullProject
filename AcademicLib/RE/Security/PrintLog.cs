using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.RE
{
    public class ExamTypeDataList: ResponeValues
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public string Name { get; set; } = "";
        public string RegNo { get; set; } = "";
        public int RollNo { get; set; } 
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public string Batch { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string Faculty { get; set; } = "";
        public string Level { get; set; } = "";
        public string PublicIP { get; set; } = "";
        public DateTime LogDate { get; set; }
        public string LogMitiTme { get; set; } = "";
    }
    public class ExamTypeCollection : List<ExamTypeDataList>
    {

        public ExamTypeCollection()
        {
            ResponseMSG = "";
        }

        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
