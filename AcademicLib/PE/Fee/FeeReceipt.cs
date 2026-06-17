using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Fee
{
    public class FeeReceipt : Dynamic.Accounting.IReportLoadObjectData
    {

        public FeeReceipt(List<AcademicLib.RE.Fee.FeeReceipt> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Fee.FeeReceipt> dataColl = null;
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
            //DiscountPer, DiscountAmt, FineAmt, ReceivableAmt, ReceivedAmt,
            //AfterReceivedDues, FatherName,
            //F_ContactNo, MotherName, M_ContactNo, Address, DOB_AD, DOB_BS, UserName, LogDateTime, IsCancel, CancelBy, CancelDateTime,
            //CancelRemarks, VoucherDate_AD, VoucherDate_BS, PaidUpToMonthName, DetailsColl

           //IsParent, TranId, RegdNo, Name, RollNo, ClassName, SectionName, AutoVoucherNo, AutoManualNo, RefNo, Narration, PaidUpToMonth, TotalDues,
           //DiscountPer, DiscountAmt, FineAmt, ReceivableAmt, ReceivedAmt, AfterReceivedDues, FatherName, F_ContactNo, MotherName, M_ContactNo, Address,
           //DOB_AD, DOB_BS, UserName, LogDateTime, IsCancel, CancelBy, CancelDateTime, CancelRemarks, VoucherDate_AD, VoucherDate_BS, PaidUpToMonthName,
           //DetailsColl,
           //BranchName, CostClass, ReceiptAsLedger, JVNo, Level, Faculty, Semester, ClassYear, Batch, AcademicYearName 

               AcademicLib.RE.Fee.FeeReceipt csl = (AcademicLib.RE.Fee.FeeReceipt)obj;
            row.Data[0] = csl.IsParent;
            row.Data[1] = csl.TranId;
            row.Data[2] = csl.RegdNo;
            row.Data[3] = csl.Name;
            row.Data[4] = csl.RollNo;
            row.Data[5] = csl.ClassName;
            row.Data[6] = csl.SectionName;
            row.Data[7] = csl.AutoVoucherNo;
            row.Data[8] = csl.AutoManualNo;
            row.Data[9] = csl.RefNo;
            row.Data[10] = csl.Narration;
            row.Data[11] = csl.PaidUpToMonth;
            row.Data[12] = csl.TotalDues;
            row.Data[13] = csl.DiscountPer;
            row.Data[14] = csl.DiscountAmt;
            row.Data[15] = csl.FineAmt;
            row.Data[16] = csl.ReceivableAmt;
            row.Data[17] = csl.ReceivedAmt;            
            row.Data[18] = csl.AfterReceivedDues;
            row.Data[19] = csl.FatherName;
            row.Data[20] = csl.F_ContactNo;
            row.Data[21] = csl.MotherName;
            row.Data[22] = csl.M_ContactNo;
            row.Data[23] = csl.Address;
            row.Data[24] = csl.DOB_AD;
            row.Data[25] = csl.DOB_BS;
            row.Data[26] = csl.UserName;
            row.Data[27] = csl.LogDateTime;
            row.Data[28] = csl.IsCancel;
            row.Data[29] = csl.CancelBy;
            row.Data[30] = csl.CancelDateTime;
            row.Data[31] = csl.CancelRemarks;
            row.Data[32] = csl.VoucherDate_AD;
            row.Data[33] = csl.VoucherDate_BS;
            row.Data[34] = csl.PaidUpToMonthName;
            row.Data[35] = csl.DetailsColl;

            try
            {
                //BranchName, CostClass, ReceiptAsLedger, JVNo, Level, Faculty, Semester, ClassYear, Batch, AcademicYearName 
                row.Data[36] = csl.BranchName;
                row.Data[37] = csl.CostClass;
                row.Data[38] = csl.ReceiptAsLedger;
                row.Data[39] = csl.JVNo;
                row.Data[40] = csl.Level;
                row.Data[41] = csl.Faculty;
                row.Data[42] = csl.Semester;
                row.Data[43] = csl.ClassYear;
                row.Data[44] = csl.Batch;
                row.Data[45] = csl.AcademicYearName;

                row.Data[46] = csl.FeeCategory;
                row.Data[47] = csl.FeeCategorySNo;
                row.Data[48] = csl.Waiver;
                row.Data[49] = csl.IsNewStudent;
                row.Data[50] = csl.LedgerPanaNo; 
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
