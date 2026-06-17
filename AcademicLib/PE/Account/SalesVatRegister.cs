using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class SalesVatRegister : Dynamic.Accounting.IReportLoadObjectData
    {

        public SalesVatRegister(List<Dynamic.ReportEntity.Account.NewSalesVatRegister> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.ReportEntity.Account.NewSalesVatRegister> dataColl = null;
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
                Dynamic.ReportEntity.Account.NewSalesVatRegister ag = (Dynamic.ReportEntity.Account.NewSalesVatRegister)obj;
                row.Data[0] = ag.IsParent;
                row.Data[1] = ag.TranId;
                row.Data[2] = ag.VoucherId;
                row.Data[3] = ag.CostClassId;
                row.Data[4] = ag.VoucherType;
                row.Data[5] = ag.VoucherDate_AD;
                row.Data[6] = ag.VoucherDate_BS;
                row.Data[7] = ag.AutoVoucherNo;
                row.Data[8] = ag.VoucherNo;
                row.Data[9] = ag.PartyName;
                row.Data[10] = ag.PanVat;
                row.Data[11] = ag.AccessableValue;
                row.Data[12] = ag.VatRate;
                row.Data[13] = ag.VatAmount;
                row.Data[14] = ag.InvoiceAmount;
                row.Data[15] = ag.IsExport;
                row.Data[16] = ag.ExportCountry;
                row.Data[17] = ag.PPNo;
                row.Data[18] = ag.PPDate_AD;
                row.Data[19] = ag.PPDate_BS;
                row.Data[20] = ag.ProductName;
                row.Data[21] = ag.ProductAlias;
                row.Data[22] = ag.ProductCode;
                row.Data[23] = ag.ProductDescription;
                row.Data[24] = ag.ItemDescription;
                row.Data[25] = ag.ActualQty;
                row.Data[26] = ag.BilledQty;
                row.Data[27] = ag.Unit;
                row.Data[28] = ag.ProductRate;
                row.Data[29] = ag.ProductAmount;
                row.Data[30] = ag.ProductVatRate;
                row.Data[31] = ag.ProductVatAmount;
                row.Data[32] = ag.TaxableSales;
                row.Data[33] = ag.NonTaxableSales;
                row.Data[34] = ag.ExportSales;
                row.Data[35] = ag.Branch;
                row.Data[36] = ag.ExciseDuty;
                row.Data[37] = ag.ChieldColl;
                row.Data[38] = ag.SNo;

                // LedgerName, LedgerAlias, LedgerCode, ProductGroup, ProductCategories, ProductType 
                row.Data[39] = ag.LedgerName;
                row.Data[40] = ag.LedgerAlias;
                row.Data[41] = ag.LedgerCode;
                row.Data[42] = ag.ProductGroup;
                row.Data[43] = ag.ProductCategories;
                row.Data[44] = ag.ProductType;
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
