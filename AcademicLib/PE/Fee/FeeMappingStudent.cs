using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Fee
{
     public class FeeMappingStudent : Dynamic.Accounting.IReportLoadObjectData
    {

        public FeeMappingStudent(List<AcademicLib.RE.Fee.FeeMappingStudent> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Fee.FeeMappingStudent> dataColl = null;
        public System.Collections.IEnumerable DataColl
        {
            get
            {
                return dataColl;
            }
        }

        public string ReportPath
        {
            get
            {
                return "";
            }
        }
        public void GetDataOfCurrentRow(object obj, Dynamic.ReportEngine.RDL.Row row)
        {
             

        AcademicLib.RE.Fee.FeeMappingStudent csl = (AcademicLib.RE.Fee.FeeMappingStudent)obj;
            row.Data[0] = csl.StudentId;
            row.Data[1] = csl.AutoNumber;
            row.Data[2] = csl.RegNo;
            row.Data[3] = csl.RollNo;
            row.Data[4] = csl.ClassName;
            row.Data[5] = csl.SectionName;
            row.Data[6] = csl.Name;
            row.Data[7] = csl.FatherName;
            row.Data[8] = csl.ContactNo;
            row.Data[9] = csl.Address;
            row.Data[10] = csl.FeeItem;
            row.Data[11] = csl.Nature;  

        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
