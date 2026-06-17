using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Fee
{
    public class DiscountStudent : Dynamic.Accounting.IReportLoadObjectData
    {

        public DiscountStudent(List<AcademicLib.RE.Fee.DiscountStudent> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Fee.DiscountStudent> dataColl = null;
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
            //IsParent, TranId, RegdNo, Name, RollNo, ClassName, SectionName, AutoVoucherNo, AutoManualNo, RefNo, Narration, PaidUpToMonth, TotalDues,
          
            AcademicLib.RE.Fee.DiscountStudent csl = (AcademicLib.RE.Fee.DiscountStudent)obj;
            row.Data[0] = csl.StudentId;
            row.Data[1] = csl.AutoNumber;
            row.Data[2] = csl.RegNo;
            row.Data[3] = csl.Name;
            row.Data[4] = csl.RollNo;
            row.Data[5] = csl.ClassName;
            row.Data[6] = csl.SectionName;
            row.Data[7] = csl.FatherName;
            row.Data[8] = csl.F_ContactNo;
            row.Data[9] = csl.Address;
            row.Data[10] = csl.Caste;
            row.Data[11] = csl.TranType;
            row.Data[12] = csl.DiscountType;
            row.Data[13] = csl.Remarks;
            row.Data[14] = csl.Details;
            row.Data[15] = csl.TransportPoint;
            row.Data[16] = csl.TransportRoute;
            row.Data[17] = csl.RoomName;
            row.Data[18] = csl.IsLeft;
            row.Data[19] = csl.ClassSec;

        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
