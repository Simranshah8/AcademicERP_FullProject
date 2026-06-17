using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicLib.BE;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Fee.Report
{
    internal class BillingSummaryDB
    {
        DataAccessLayer1 dal = null;

        public BillingSummaryDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }

        public RE.Fee.BillingSummaryCollection GetBillingSummaryList(int UserId, DateTime? FromDate, DateTime? ToDate, string BillingType, int ReportType, bool? IsCancel)
        {
            RE.Fee.BillingSummaryCollection dataColl = new RE.Fee.BillingSummaryCollection();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", FromDate);
            cmd.Parameters.AddWithValue("@DateTo", ToDate);
            cmd.Parameters.AddWithValue("@BillingType", BillingType);
            cmd.Parameters.AddWithValue("@ReportType", ReportType);
            cmd.Parameters.AddWithValue("@IsCancel", IsCancel);
            cmd.CommandText = "usp_GetBillingSummaryDateWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                var Sno = 1;
                while (reader.Read())
                {
                    RE.Fee.BillingSummary beData = new RE.Fee.BillingSummary();

                    if (ReportType == 1)
                    {
                        beData.SNo = Sno;
                        if (!(reader[0] is DBNull)) beData.FeeItemId = reader.GetInt32(0);
                        if (!(reader[1] is DBNull)) beData.OrderNo = reader.GetInt32(1);
                        if (!(reader[2] is DBNull)) beData.FeeItemName = reader.GetString(2);
                        if (!(reader[3] is DBNull)) beData.BillingAmt = reader.GetDouble(3);
                        if (!(reader[4] is DBNull)) beData.DisAmt = reader.GetDouble(4);
                        if (!(reader[5] is DBNull)) beData.PayableAmt = reader.GetDouble(5);
                        //Added by Suresh on 30 bhadra 2082
                        if (!(reader[6] is DBNull)) beData.Ledger = reader.GetString(6);
                        if (!(reader[7] is DBNull)) beData.LedgerGroup = reader.GetString(7);
                        if (!(reader[8] is DBNull)) beData.Product = reader.GetString(8);
                        if (!(reader[9] is DBNull)) beData.ProductType = reader.GetString(9);
                    }
                    else if (ReportType == 2)
                    {
                        beData.SNo = Sno;
                        //if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                        if (!(reader[0] is DBNull)) beData.ClassName = reader.GetString(0);
                        if (!(reader[1] is DBNull)) beData.SectionName = reader.GetString(1);
                        if (!(reader[2] is DBNull)) beData.Batch = reader.GetString(2);
                        if (!(reader[3] is DBNull)) beData.Semester = reader.GetString(3);
                        if (!(reader[4] is DBNull)) beData.ClassYear = reader.GetString(4);
                        if (!(reader[5] is DBNull)) beData.BillingAmt = reader.GetDouble(5);
                        if (!(reader[6] is DBNull)) beData.DisAmt = reader.GetDouble(6);
                        if (!(reader[7] is DBNull)) beData.PayableAmt = reader.GetDouble(7);
                    }
                    Sno++;
                    dataColl.Add(beData);
                }

                reader.Close();
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }

            return dataColl;
        }
    }
}
