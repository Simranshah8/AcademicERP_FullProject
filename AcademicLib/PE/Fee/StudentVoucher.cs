using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Fee
{
    public class StudentVoucher : Dynamic.Accounting.IReportLoadObjectData
    {

        public StudentVoucher(List<AcademicLib.RE.Fee.Voucher> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Fee.Voucher> dataColl = null;
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
         
            AcademicLib.RE.Fee.Voucher csl = (AcademicLib.RE.Fee.Voucher)obj;


            //TranType, TranId, VoucherDate_AD, VoucherDate_BS, VoucherType, VoucherNo, RefNo, Particulars, Amount, DisAmt, Debit, Credit, Narration, UserName, LogDateTime, ATranId, AVoucherNo, DetailsColl, CurClosing, IsParent 

            try
            {
                row.Data[0] = csl.TranType;
                row.Data[1] = csl.TranId;
                row.Data[2] = csl.VoucherDate_AD;
                row.Data[3] = csl.VoucherDate_BS;
                row.Data[4] = csl.VoucherType;
                row.Data[5] = csl.VoucherNo;
                row.Data[6] = csl.RefNo;
                row.Data[7] = csl.Particulars;
                row.Data[8] = csl.Amount;
                row.Data[9] = csl.DisAmt;
                row.Data[10] = csl.Debit;
                row.Data[11] = csl.Credit;
                row.Data[12] = csl.Narration;
                row.Data[13] = csl.UserName;

                row.Data[14] = csl.LogDateTime;
                row.Data[15] = csl.ATranId;
                row.Data[16] = csl.AVoucherNo;
                row.Data[17] = csl.DetailsColl;
                row.Data[18] = csl.CurClosing;
                row.Data[19] = csl.IsParent;
                  
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
