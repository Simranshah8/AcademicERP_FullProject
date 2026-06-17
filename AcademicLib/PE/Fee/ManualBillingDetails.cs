using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Fee
{
    public class ManualBillingDetails : Dynamic.Accounting.IReportLoadObjectData
    {

        public ManualBillingDetails(List<AcademicLib.RE.Fee.ManualBillingDetails> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Fee.ManualBillingDetails> dataColl = null;
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

            AcademicLib.RE.Fee.ManualBillingDetails csl = (AcademicLib.RE.Fee.ManualBillingDetails)obj;
            try
            {
               //TranId, AutoNumber, BillingDate, BillingMiti, RegNo, Name, ClassName, SectionName, RollNo, ContactNo, Email, FeeItem, ProductName, ProductCode, Qty, Rate, DiscountAmt, PayableAmt, PaidAmt,
               //DuesAmt, Remarks, RefNo, AcademicYear, UserName, LogDateTime, LogMiti, ForMonth, BillingType 

                row.Data[0] = csl.TranId;
                row.Data[1] = csl.AutoNumber;
                row.Data[2] = csl.BillingDate;
                row.Data[3] = csl.BillingMiti;
                row.Data[4] = csl.RegNo;
                row.Data[5] = csl.Name;
                row.Data[6] = csl.ClassName;
                row.Data[7] = csl.SectionName;
                row.Data[8] = csl.RollNo;
                row.Data[9] = csl.ContactNo;
                row.Data[10] = csl.Email;
                row.Data[11] = csl.FeeItem;

                row.Data[12] = csl.ProductName;
                row.Data[13] = csl.ProductCode;
                row.Data[14] = csl.Qty;
                row.Data[15] = csl.Rate;
                row.Data[16] = csl.DiscountAmt;
                row.Data[17] = csl.PayableAmt;
                row.Data[18] = csl.PaidAmt;
                row.Data[19] = csl.DuesAmt;
                row.Data[20] = csl.Remarks;
                row.Data[21] = csl.RefNo;
                row.Data[22] = csl.AcademicYear;
                row.Data[23] = csl.UserName;
                row.Data[24] = csl.LogDateTime;
                row.Data[25] = csl.LogMiti;
                row.Data[26] = csl.ForMonth;
                row.Data[27] = csl.BillingType;  
            }
            catch { }


        

        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
