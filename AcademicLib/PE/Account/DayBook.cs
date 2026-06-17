using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Account
{
    public class DayBook : Dynamic.Accounting.IReportLoadObjectData
    {

        public DayBook(List<Dynamic.ReportEntity.Account.DayBook> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<Dynamic.ReportEntity.Account.DayBook> dataColl = null;
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

                //TranId, AutoManualNo, AutoVoucherNo, ManualVoucherNO, VoucherId, VoucherName, CostClassId, CostClassName, VoucherDate, Narration, IsCancel, RefNo, IsPost, PostBy, NY, NM, ND,
                //CurrencyId, CurrencyName, CurRate, LedgerAllocationColl, DocumentColl, CanUpdateDocument, TranType, CreatedBy, CreatedByName, UserDefineFieldsColl, AllowAutoVoucherNoAlter, IsPending, EntryDate, EDNY, EDNM, EDND, BranchId, Prefix, Suffix, IsLocked, IsOpening, LedgerName, Amount, IsVerify, VerifyRemarks, IsReject, RejectRemarks, VerifyDate, RejectDate, VoucherType, VoucherDateTime, LCDetail, CancelRemarks, ReferanceTranId, PurchaseJournalColl, Particulars, PartyLedger, IsParent, ItemAllocationColl, DrAmount, CrAmount, AditionalCostColl, VoucherDateStr, VoucherDateStrNP, IsInQty, VerifyId, RejectId, RefNumber, CancelDateTime, CancelBy, Agent, IsCreditLimitAmtOver, LimitCrossAmt, IsCreditDaysOver, CreditDayOver, Buyes, Address, PanVatNo, TranStatus, TranStatusId, ModifyBy, RefVoucherNo, Ref_RefNo, IsBreakDownParts, BrandName, LogDateTime, PostDateTime, Qty, Rate, ItemAmount, Unit, VerifyMode, ChequeDate, ChequeNo, ChequeRemarks, TransactionAmt, BranchName, Destination, PaymentTerms, ResponseMSG, IsSuccess, RId, CUserId, ResponseId, EntityId, ErrorNumber FROM Dynamic.ReportEntity.Account.DayBook </ CommandText >

                Dynamic.ReportEntity.Account.DayBook ag = (Dynamic.ReportEntity.Account.DayBook)obj;
                row.Data[0] = ag.TranId;
                row.Data[1] = ag.AutoManualNo;
                row.Data[2] = ag.AutoVoucherNo;
                row.Data[3] = ag.ManualVoucherNO;
                row.Data[4] = ag.VoucherId;
                row.Data[5] = ag.VoucherName;
                row.Data[6] = ag.CostClassId;
                row.Data[7] = ag.CostClassName;
                row.Data[8] = ag.VoucherDate;
                row.Data[9] = ag.Narration;
                row.Data[10] = ag.IsCancel;
                row.Data[11] = ag.RefNo;
                row.Data[12] = ag.IsPost;
                row.Data[13] = ag.PostBy;
                row.Data[14] = ag.NY;
                row.Data[15] = ag.NM;
                row.Data[16] = ag.ND;
                row.Data[17] = ag.CurrencyId;
                row.Data[18] = ag.CurrencyName;
                row.Data[19] = ag.CurRate;
                row.Data[20] = ag.LedgerAllocationColl;
                row.Data[21] = ag.DocumentColl;
                row.Data[22] = ag.CanUpdateDocument;
                row.Data[23] = ag.TranType;
                row.Data[24] = ag.CreatedBy;
                row.Data[25] = ag.CreatedByName;
                row.Data[26] = ag.UserDefineFieldsColl;
                row.Data[27] = ag.AllowAutoVoucherNoAlter;
                row.Data[28] = ag.IsPending;
                row.Data[29] = ag.EntryDate;
                row.Data[30] = ag.EDNY;
                row.Data[31] = ag.EDNM;
                row.Data[32] = ag.EDND;
                row.Data[33] = ag.BranchId;
                row.Data[34] = ag.Prefix;
                row.Data[35] = ag.Suffix;
                row.Data[36] = ag.IsLocked;
                row.Data[37] = ag.IsOpening;
                row.Data[38] = ag.LedgerName;
                row.Data[39] = ag.Amount;
                row.Data[40] = ag.IsVerify;
                row.Data[41] = ag.VerifyRemarks;
                row.Data[42] = ag.IsReject;
                row.Data[43] = ag.RejectRemarks;
                row.Data[44] = ag.VerifyDate;
                row.Data[45] = ag.RejectDate;
                row.Data[46] = ag.VoucherType;
                row.Data[47] = ag.VoucherDateTime;
                row.Data[48] = ag.LCDetail;
                row.Data[49] = ag.CancelRemarks;
                row.Data[50] = ag.ReferanceTranId;
                row.Data[51] = ag.PurchaseJournalColl;
                row.Data[52] = ag.Particulars;
                row.Data[53] = ag.PartyLedger;
                row.Data[54] = ag.IsParent;
                row.Data[55] = ag.ItemAllocationColl;
                row.Data[56] = ag.DrAmount;
                row.Data[57] = ag.CrAmount;
                row.Data[58] = ag.AditionalCostColl;
                row.Data[59] = ag.VoucherDateStr;
                row.Data[60] = ag.VoucherDateStrNP;
                row.Data[61] = ag.IsInQty;
                row.Data[62] = ag.VerifyId;
                row.Data[63] = ag.RejectId;
                row.Data[64] = ag.RefNumber;
                row.Data[65] = ag.CancelDateTime;
                row.Data[66] = ag.CancelBy;
                row.Data[67] = ag.Agent;
                row.Data[68] = ag.IsCreditLimitAmtOver;
                row.Data[69] = ag.LimitCrossAmt;
                row.Data[70] = ag.IsCreditDaysOver;
                row.Data[71] = ag.CreditDayOver;
                row.Data[72] = ag.Buyes;
                row.Data[73] = ag.Address;
                row.Data[74] = ag.PanVatNo;
                row.Data[75] = ag.TranStatus;
                row.Data[76] = ag.TranStatusId;
                row.Data[77] = ag.ModifyBy;
                row.Data[78] = ag.RefVoucherNo;
                row.Data[79] = ag.Ref_RefNo;
                row.Data[80] = ag.IsBreakDownParts;
                row.Data[81] = ag.BrandName;
                row.Data[82] = ag.LogDateTime;
                row.Data[83] = ag.PostDateTime;
                row.Data[84] = ag.Qty;
                row.Data[85] = ag.Rate;
                row.Data[86] = ag.ItemAmount;
                row.Data[87] = ag.Unit;
                row.Data[88] = ag.VerifyMode;
                row.Data[89] = ag.ChequeDate;
                row.Data[90] = ag.ChequeNo;
                row.Data[91] = ag.ChequeRemarks;
                row.Data[92] = ag.TransactionAmt;
                row.Data[93] = ag.BranchName;
                row.Data[94] = ag.Destination;
                row.Data[95] = ag.PaymentTerms;
                row.Data[96] = ag.ResponseMSG;
                row.Data[97] = ag.IsSuccess;
                row.Data[98] = ag.RId;
                row.Data[99] = ag.CUserId;
                row.Data[100] = ag.ResponseId;
                row.Data[101] = ag.EntityId;
                row.Data[102] = ag.ErrorNumber;
                 
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
