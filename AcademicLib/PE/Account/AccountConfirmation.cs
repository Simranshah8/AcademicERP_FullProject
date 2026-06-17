using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class AccountConfirmation : Dynamic.Accounting.IReportLoadObjectData
    {

        public AccountConfirmation(List<Dynamic.ReportEntity.Account.AccountConfirmationLetter> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.ReportEntity.Account.AccountConfirmationLetter> dataColl = null;
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

            Dynamic.ReportEntity.Account.AccountConfirmationLetter ag = (Dynamic.ReportEntity.Account.AccountConfirmationLetter)obj;
            //LedgerId, Name, Address, GroupName, Agent, PanVatNo, Opening, DrAmount, CrAmount, Closing, SalesInvoiceAmount, SalesDiscount, SalesExDuty, SalesVat, SalesSchame, 
            //SalesFreight, PurchaseInvoiceAmount, PurchaseDiscount, PurchaseExDuty, PurchaseVat, PurchaseSchame, PurchaseFreight, ActualSales, ActualVat 

            //row.Data[0] = ag.LedgerId;
            //row.Data[1] = ag.Name;
            //row.Data[2] = ag.Address;
            //row.Data[3] = ag.GroupName;
            //row.Data[4] = ag.Agent;
            //row.Data[5] = ag.PanVatNo;
            //row.Data[6] = ag.Opening;
            //row.Data[7] = ag.DrAmount;
            //row.Data[8] = ag.CrAmount;
            //row.Data[9] = ag.Closing;
            //row.Data[10] = ag.SalesInvoiceAmount;
            //row.Data[11] = ag.SalesDiscount;
            //row.Data[12] = ag.SalesExDuty;
            //row.Data[13] = ag.SalesVat;
            //row.Data[14] = ag.SalesSchame;
            //row.Data[15] = ag.SalesFreight;
            //row.Data[16] = ag.PurchaseInvoiceAmount;
            //row.Data[17] = ag.PurchaseDiscount;
            //row.Data[18] = ag.PurchaseExDuty;
            //row.Data[19] = ag.PurchaseVat;

            //try
            //{
            //    row.Data[20] = ag.PurchaseSchame;
            //    row.Data[21] = ag.PurchaseFreight;
            //    row.Data[22] = ag.ActualSales;
            //    row.Data[23] = ag.ActualVat;
            //    row.Data[24] = ag.TaxAbleSales;
            //    row.Data[25] = ag.NonTaxAbleSales;
            //}
            //catch { }
            



        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
