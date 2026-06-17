using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class SalesMaterializeView : Dynamic.Accounting.IReportLoadObjectData
    {

        public SalesMaterializeView(List<Dynamic.ReportEntity.Inventory.SalesMaterializedView> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.ReportEntity.Inventory.SalesMaterializedView> dataColl = null;
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

            try
            {

                //TranId, VouherId, CostClassId, NY, NM, ND, VoucherDate, VoucherDateBS, EnterBy, PrintBy, IsActive, IsPrinted, NoOfCopy, PrintDateTime, PrintDateTimeBS,
                //BillNo, PartyName, PanVatNo, Address, TotalAmount, Discount, Vat, TaxAbleAmount, FYear, IsRealTime, SyncWithIRD, RefBillNo, RefTranId, Branch,
                //PaymentMethod, VatRefundAmtIfAny, TransactionId  

                Dynamic.ReportEntity.Inventory.SalesMaterializedView ag = (Dynamic.ReportEntity.Inventory.SalesMaterializedView)obj;

                row.Data[0] = ag.TranId;
                row.Data[1] = ag.VouherId;
                row.Data[2] = ag.CostClassId;
                row.Data[3] = ag.NY;
                row.Data[4] = ag.NM;
                row.Data[5] = ag.ND;
                row.Data[6] = ag.VoucherDate;
                row.Data[7] = ag.VoucherDateBS;
                row.Data[8] = ag.EnterBy;
                row.Data[9] = ag.PrintBy;
                row.Data[10] = ag.IsActive;
                row.Data[11] = ag.IsPrinted;
                row.Data[12] = ag.NoOfCopy;
                row.Data[13] = ag.PrintDateTime;
                row.Data[14] = ag.PrintDateTimeBS;
                row.Data[15] = ag.BillNo;
                row.Data[16] = ag.PartyName;
                row.Data[17] = ag.PanVatNo;
                row.Data[18] = ag.Address;
                row.Data[19] = ag.TotalAmount;
                row.Data[20] = ag.Discount;
                row.Data[21] = ag.Vat;
                row.Data[22] = ag.TaxAbleAmount;
                row.Data[23] = ag.FYear;
                row.Data[24] = ag.IsRealTime;
                row.Data[25] = ag.SyncWithIRD;
                row.Data[26] = ag.RefBillNo;
                row.Data[27] = ag.RefTranId;

                row.Data[28] = ag.Branch;
                row.Data[29] = ag.PaymentMethod;
                row.Data[30] = ag.VatRefundAmtIfAny;
                row.Data[31] = ag.TransactionId;

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
