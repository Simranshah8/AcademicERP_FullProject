using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class TrailBalance : Dynamic.Accounting.IReportLoadObjectData
    {

        public TrailBalance(List<Dynamic.ReportEntity.Account.TrailBalance> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.ReportEntity.Account.TrailBalance> dataColl = null;
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

            Dynamic.ReportEntity.Account.TrailBalance ag = (Dynamic.ReportEntity.Account.TrailBalance)obj;            
            row.Data[0] = ag.IsLedgerGroup;
            row.Data[1] = ag.TotalSpace;
            row.Data[2] = ag.LedgerGroupId;
            row.Data[3] = ag.ParentGroupId;
            row.Data[4] = ag.LedgerId;
            row.Data[5] = ag.LedgerGroupName;
            row.Data[6] = ag.LedgerName;
            row.Data[7] = ag.OpeningDr;
            row.Data[8] = ag.OpeningCr;
            row.Data[9] = ag.TransactionOpeningDr;
            row.Data[10] = ag.TransactionOpeningCr;
            row.Data[11] = ag.TotalOpeningDr;
            row.Data[12] = ag.TotalOpeningCr;
            row.Data[13] = ag.ClosingDr;
            row.Data[14] = ag.ClosingCr;
            row.Data[15] = ag.Opening;
            row.Data[16] = ag.TransactionDr;
            row.Data[17] = ag.TransactionCr;
            row.Data[18] = ag.Transaction;
            row.Data[19] = ag.ClosingDr;
            row.Data[20] = ag.ClosingCr;
            row.Data[21] = ag.Closing;

            try
            {
                row.Data[22] = ag.ChieldColl;
                row.Data[23] = ag.AreaName;
                row.Data[24] = ag.AreaType;
                row.Data[25] = ag.MobileNo1;
                row.Data[26] = ag.MobileNo2;
                row.Data[27] = ag.TelNo1;
                row.Data[28] = ag.TelNo2;
                row.Data[29] = ag.Address;
                row.Data[30] = ag.EmailId;

                row.Data[31] = ag.BranchName;
                row.Data[32] = ag.Code;
                row.Data[33] = ag.ParentCode;
                row.Data[34] = ag.Alias;
                row.Data[35] = ag.SNO;

                row.Data[36] = ag.GroupingId;
                row.Data[37] = ag.GroupingName;

            }
            catch (Exception eee) { }


        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
