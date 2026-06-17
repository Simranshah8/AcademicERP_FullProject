using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class PurchaseVatRegister : Dynamic.Accounting.IReportLoadObjectData
    {

        public PurchaseVatRegister(List<Dynamic.ReportEntity.Account.NewPurchaseVatRegister> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.ReportEntity.Account.NewPurchaseVatRegister> dataColl = null;
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
                Dynamic.ReportEntity.Account.NewPurchaseVatRegister ag = (Dynamic.ReportEntity.Account.NewPurchaseVatRegister)obj;
                row.Data[0] = ag.IsParent;
                row.Data[1] = ag.TranId;
                row.Data[2] = ag.VoucherId;
                row.Data[3] = ag.CostClassId;
                row.Data[4] = ag.VoucherType;
                row.Data[5] = ag.VoucherDate_AD;
                row.Data[6] = ag.VoucherDate_BS;
                row.Data[7] = ag.AutoVoucherNo;
                row.Data[8] = ag.VoucherNo;
                row.Data[9] = ag.RefNo;
                row.Data[10] = ag.PartyName;
                row.Data[11] = ag.PanVat;
                row.Data[12] = ag.AccessableValue;
                row.Data[13] = ag.VatRate;
                row.Data[14] = ag.VatAmount;
                row.Data[15] = ag.InvoiceAmount;
                row.Data[16] = ag.IsImport;
                row.Data[17] = ag.IsCapital;
                row.Data[18] = ag.ImportCountry;
                row.Data[19] = ag.PPNo;
                row.Data[20] = ag.PPDate_AD;
                row.Data[21] = ag.PPDate_BS;
                row.Data[22] = ag.ProductName;
                row.Data[23] = ag.ProductAlias;
                row.Data[24] = ag.ProductCode;
                row.Data[25] = ag.ProductDescription;
                row.Data[26] = ag.ItemDescription;
                row.Data[27] = ag.ActualQty;
                row.Data[28] = ag.BilledQty;
                row.Data[29] = ag.Unit;
                row.Data[30] = ag.ProductRate;
                row.Data[31] = ag.ProductAmount;
                row.Data[32] = ag.ProductVatRate;
                row.Data[33] = ag.ProductVatAmount;
                row.Data[34] = ag.TaxablePurchase;
                row.Data[35] = ag.NonTaxablePurchase;
                row.Data[36] = ag.ImportPurchase;
                row.Data[37] = ag.CapitalPurchase;
                row.Data[38] = ag.ImportVat;
                row.Data[39] = ag.CapitalVat;

                row.Data[40] = ag.Branch;
                row.Data[41] = ag.ExciseDuty;
                row.Data[42] = ag.ChieldColl;

                row.Data[43] = ag.LedgerName;
                row.Data[44] = ag.LedgerAlias;
                row.Data[45] = ag.LedgerCode;
                row.Data[46] = ag.ProductGroup;
                row.Data[47] = ag.ProductCategories;
                row.Data[48] = ag.ProductType;
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
