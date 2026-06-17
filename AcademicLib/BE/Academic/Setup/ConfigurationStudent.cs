using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Setup
{
    public class ConfigurationStudent:ResponeValues
    {        
        public ConfigurationStudent()
        {
            StudentRefAs = 1;
        }
        public int RegdNumberingMethod { get; set; } 
        public string RegdPrefix { get; set; } 
        public string RegdSuffix { get; set; } 
        public bool AutoGenerateRollNo { get; set; } 
        public bool ShowLeftStudentinTC_CC { get; set; }

        public int StartNo { get; set; }
        public int NumericalPartWidth { get; set; }
        public bool AllowReGenerateUserPwd { get; set; }
        public int LeftStudentConfig { get; set; }

        public int? BranchId { get; set; }
        public int StudentRefAs { get; set; }
        public bool StudentRefFeeDebit { get; set; }

        public bool ShowBillingInAdmission { get; set; }
        public bool FilterStudentForBorders { get; set; }

    }
  
}
