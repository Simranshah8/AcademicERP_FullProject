using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class SalesInvoiceDetails : Dynamic.Accounting.IReportLoadObjectData
    {

        public SalesInvoiceDetails(List<Dynamic.ReportEntity.Inventory.SalesInvoiceDetails> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.ReportEntity.Inventory.SalesInvoiceDetails> dataColl = null;
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
                Dynamic.ReportEntity.Inventory.SalesInvoiceDetails ag = (Dynamic.ReportEntity.Inventory.SalesInvoiceDetails)obj;

                //TranId, InvoiceNo, VoucherDate, VoucherDateBS, SalesLedger, PartyLedger, PartyLedgerGroup, SalesTaxNo, DI_Name, Area, InvoiceAmount, ProductName,
                //ProductAlias, ProductCode, ProductGroup, ProductCategory, ProductType, Qty, Rate, Amount, Unit, Discount, SchameAmount, PartyArea, Unit1, Unit2, Unit3,
                //Qty1, Qty2, Qty3, PartyCostCenter, RefNo, VoucherName, PartyAddress, VoucherId, CostClassId, PartyCode, DI_Code, ProductCompany, ProductDivision,
                //SalesLedgerGroup, CategorySNO, TotalAmount, TranCostCenter, PartyCostCategory, TranCostCategory, ProductDescription, DI_Id, DI_Email, SO_Id, SO_Email,
                //ASM_Id, ASM_Email, TermsOfPayment, CashBank, ProductBrandName, BrandSNo, PartyLedgerGroup1, PartyLedgerGroup2, SO_Name, ASM_Name, RSM_Name, NSM_Name,
                //SD_Name, MD_Name, RSM_Email, NSM_Email, SD_Email, MD_Email, MonthName, MonthSNo, NY, OrderDate, OrderMiti, OrderApprovedDate, OrderApprovedMiti, GapDays,
                //OrderRefNo, SalesRefNo 

                row.Data[0] = ag.TranId;
                row.Data[1] = ag.InvoiceNo;
                row.Data[2] = ag.VoucherDate;
                row.Data[3] = ag.VoucherDateBS;
                row.Data[4] = ag.SalesLedger;
                row.Data[5] = ag.PartyLedger;
                row.Data[6] = ag.PartyLedgerGroup;
                row.Data[7] = ag.SalesTaxNo;
                row.Data[8] = ag.DI_Name;
                row.Data[9] = ag.Area;
                row.Data[10] = ag.InvoiceAmount;
                row.Data[11] = ag.ProductName;
                row.Data[12] = ag.ProductAlias;
                row.Data[13] = ag.ProductCode;
                row.Data[14] = ag.ProductGroup;
                row.Data[15] = ag.ProductCategory;
                row.Data[16] = ag.ProductType;
                row.Data[17] = ag.Qty;
                row.Data[18] = ag.Rate;
                row.Data[19] = ag.Amount;
                row.Data[20] = ag.Unit;
                row.Data[21] = ag.Discount;
                row.Data[22] = ag.SchameAmount;
                row.Data[23] = ag.PartyArea;
                row.Data[24] = ag.Unit1;
                row.Data[25] = ag.Unit2;
                row.Data[26] = ag.Unit3;
                row.Data[27] = ag.Qty1;
                row.Data[28] = ag.Qty2;
                row.Data[29] = ag.Qty3;
                row.Data[30] = ag.PartyCostCenter;
                row.Data[31] = ag.RefNo;
                row.Data[32] = ag.VoucherName;
                row.Data[33] = ag.PartyAddress;
                row.Data[34] = ag.VoucherId;
                row.Data[35] = ag.CostClassId;
                row.Data[36] = ag.PartyCode;
                row.Data[37] = ag.DI_Code;
                row.Data[38] = ag.ProductCompany;
                row.Data[39] = ag.ProductDivision;


                row.Data[40] = ag.SalesLedgerGroup;
                row.Data[41] = ag.CategorySNO;
                row.Data[42] = ag.TotalAmount;
                row.Data[43] = ag.TranCostCenter;
                row.Data[44] = ag.PartyCostCategory;
                row.Data[45] = ag.TranCostCategory;
                row.Data[46] = ag.ProductDescription;
                row.Data[47] = ag.DI_Id;
                row.Data[48] = ag.DI_Email;
                row.Data[49] = ag.SO_Id;
                row.Data[50] = ag.SO_Email;
                row.Data[51] = ag.ASM_Id;
                row.Data[52] = ag.ASM_Email;
                row.Data[53] = ag.TermsOfPayment;
                row.Data[54] = ag.CashBank;
                row.Data[55] = ag.ProductBrandName;
                row.Data[56] = ag.BrandSNo;
                row.Data[57] = ag.PartyLedgerGroup1;
                row.Data[58] = ag.PartyLedgerGroup2;
                row.Data[59] = ag.SO_Name;
                row.Data[60] = ag.ASM_Name;
                row.Data[61] = ag.RSM_Name;
                row.Data[62] = ag.NSM_Name;
                row.Data[63] = ag.SD_Name;
                row.Data[64] = ag.MD_Name;
                row.Data[65] = ag.RSM_Email;
                row.Data[66] = ag.NSM_Email;
                row.Data[67] = ag.SD_Email;
                row.Data[68] = ag.MD_Email;
                row.Data[69] = ag.MonthName;
                row.Data[70] = ag.MonthSNo;

                row.Data[71] = ag.NY;
                row.Data[72] = ag.OrderDate;
                row.Data[73] = ag.OrderMiti;
                row.Data[74] = ag.OrderApprovedDate;
                row.Data[75] = ag.OrderApprovedMiti;
                row.Data[76] = ag.GapDays;
                row.Data[77] = ag.OrderRefNo;
                row.Data[78] = ag.SalesRefNo;

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
