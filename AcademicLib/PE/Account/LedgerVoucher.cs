using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class LedgerVoucher : Dynamic.Accounting.IReportLoadObjectData
    {

        public LedgerVoucher(List<Dynamic.ReportEntity.Account.LedgerVoucher> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.ReportEntity.Account.LedgerVoucher> dataColl = null;
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

            Dynamic.ReportEntity.Account.LedgerVoucher ag = (Dynamic.ReportEntity.Account.LedgerVoucher)obj;
            try
            {
                row.Data[0] = ag.TranId;
                row.Data[1] = ag.VoucherId;
                row.Data[2] = ag.VoucherName;
                row.Data[3] = ag.CostClassId;
                row.Data[4] = ag.CostClassName;
                row.Data[5] = ag.VoucherType;
                row.Data[6] = ag.VoucherDate;
                row.Data[7] = ag.VoucherDateStr;
                row.Data[8] = ag.NVoucherDate;
                row.Data[9] = ag.NY;
                row.Data[10] = ag.NM;
                row.Data[11] = ag.ND;
                row.Data[12] = ag.DebitAmt;
                row.Data[13] = ag.CreditAmt;
                row.Data[14] = ag.AutoManualNo;
                row.Data[15] = ag.AutoVoucherNo;
                row.Data[16] = ag.Narration;
                row.Data[17] = ag.RefNo;
                row.Data[18] = ag.UserName;
                row.Data[19] = ag.HaveDocument;
                row.Data[20] = ag.Particulars;
                row.Data[21] = ag.IsParent;

                row.Data[22] = ag.LedgerId;
                row.Data[23] = ag.LedgerName;
                row.Data[24] = ag.DrCr;
                row.Data[25] = ag.CostCenterColl;
                row.Data[26] = ag.ChieldColl;
                row.Data[27] = ag.InventoryDetailsColl;
                row.Data[28] = ag.CurrentClosing;
                row.Data[29] = ag.LedgerNarration;

                //AccountBillDetailsColl, IsLocked, ChequeNo, ChequeRemarks, RefNumber, CurRate, CurName, VoucherAge, ModifyBy, Branch, TermsOfPayment_BankName, DeliveryThrow_LC_Do_No, DeliveryDocNo_LC_Do_Date, 
                //OtherRef_Insurance, AQty, BQty, Rate, Amount, Unit 

                row.Data[30] = ag.AccountBillDetailsColl;
                row.Data[31] = ag.IsLocked;
                row.Data[32] = ag.ChequeNo;
                row.Data[33] = ag.ChequeRemarks;

                row.Data[34] = ag.RefNumber;
                row.Data[35] = ag.CurRate;
                row.Data[36] = ag.CurName;


                row.Data[37] = ag.VoucherAge;
                row.Data[38] = ag.ModifyBy;
                row.Data[39] = ag.Branch;
                row.Data[40] = ag.TermsOfPayment_BankName;
                row.Data[41] = ag.DeliveryThrow_LC_Do_No;
                row.Data[42] = ag.DeliveryDocNo_LC_Do_Date;
                row.Data[43] = ag.OtherRef_Insurance;
                row.Data[44] = ag.AQty;
                row.Data[45] = ag.BQty;
                row.Data[46] = ag.Rate;
                row.Data[47] = ag.Amount;
                row.Data[48] = ag.Unit;
                row.Data[49] = ag.ProductName;
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
