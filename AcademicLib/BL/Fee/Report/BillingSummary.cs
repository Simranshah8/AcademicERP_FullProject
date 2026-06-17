using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Fee.Report
{
    public class BillingSummary
    {
        DA.Fee.Report.BillingSummaryDB db = null;

        int _UserId = 0;

        public BillingSummary(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Report.BillingSummaryDB(hostName, dbName);
        }
        public RE.Fee.BillingSummaryCollection GetBillingSummaryList(DateTime? FromDate, DateTime? ToDate, string BillingType, int ReportType, bool? IsCancel)
        {
            return db.GetBillingSummaryList(_UserId, FromDate, ToDate, BillingType, ReportType, IsCancel);
        }

    }
}