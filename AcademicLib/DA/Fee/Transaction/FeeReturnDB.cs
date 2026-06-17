using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Fee.Transaction
{
    internal class FeeReturnDB
    {
        DataAccessLayer1 dal = null;
        public FeeReturnDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int AcademicYearId, BE.Fee.Transaction.FeeReceipt beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AutoVoucherNo", beData.AutoVoucherNo);
            cmd.Parameters.AddWithValue("@AutoManualNo", beData.AutoManualNo);
            cmd.Parameters.AddWithValue("@VoucherDate", beData.VoucherDate);
            cmd.Parameters.AddWithValue("@Narration", beData.Narration);
            cmd.Parameters.AddWithValue("@RefNo", beData.RefNo);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
            cmd.Parameters.AddWithValue("@StudentName", beData.StudentName);
            cmd.Parameters.AddWithValue("@PaidUpToMonth", beData.PaidUpToMonth);
            cmd.Parameters.AddWithValue("@PreviousDues", beData.PreviousDues);
            cmd.Parameters.AddWithValue("@CurrentDues", beData.CurrentDues);
            cmd.Parameters.AddWithValue("@TotalDues", beData.TotalDues);
            cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
            cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
            cmd.Parameters.AddWithValue("@FineAmt", beData.FineAmt);
            cmd.Parameters.AddWithValue("@ReceivableAmt", beData.ReceivableAmt);
            cmd.Parameters.AddWithValue("@ReceivedAmt", beData.ReceivedAmt);
            cmd.Parameters.AddWithValue("@AfterReceivedDues", beData.AfterReceivedDues);
            cmd.Parameters.AddWithValue("@AdvanceAmt", beData.AdvanceAmt);
            cmd.Parameters.AddWithValue("@CostClassId", beData.CostClassId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateFeeReturn";
            }
            else
            {
                cmd.Parameters[23].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddFeeReturn";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[24].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[25].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[26].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@ReturnAsLedgerId", beData.ReceiptAsLedgerId);
            cmd.Parameters.AddWithValue("@ManualBillingTranId", beData.ManualBillingTranId);
            cmd.Parameters.AddWithValue("@ClassName", beData.ClassName);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@AdmissionEnquiryId", beData.AdmissionEnquiryId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@RegistrationId", beData.RegistrationId);
            cmd.Parameters.AddWithValue("@Waiver", beData.Waiver);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[23].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[23].Value);

                if (!(cmd.Parameters[24].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[24].Value);

                if (!(cmd.Parameters[25].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[25].Value);

                if (!(cmd.Parameters[26].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[26].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveFeeReturnDetails(beData.CUserId, beData.StudentId, resVal.RId, beData.DetailsColl, beData.MonthWise);
                    SavePaymentModeDetails(beData.CUserId, resVal.RId, beData.PaymentModeColl);

                    var sales = SaveFeeReturnToReceipt(beData.CUserId, AcademicYearId, resVal.RId);
                    if (!sales.IsSuccess)
                    {
                        resVal.ResponseMSG = sales.ResponseMSG;
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        private void SaveFeeReturnDetails(int UserId, int? StudentId, int TranId, List<BE.Fee.Transaction.FeeReceiptDetails> beDataColl, bool monthWise)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Fee.Transaction.FeeReceiptDetails beData in beDataColl)
            {
                if (beData.StudentId > 0)
                    StudentId = beData.StudentId;
                else if (StudentId.HasValue)
                    StudentId = StudentId.Value;
                else
                    StudentId = null;

                //if(beData.FeeItemId.HasValue && beData.FeeItemId.Value > 0 && ( beData.FineAmt>0 || beData.DiscountAmt>0 || beData.ReceivedAmt>0 ) )
                if (beData.FeeItemId.HasValue && beData.FeeItemId.Value > 0)
                {
                    if (!monthWise)
                    {
                        if (beData.FineAmt == 0 && beData.DiscountAmt == 0 && beData.ReceivedAmt == 0 && beData.Waiver == 0)
                            continue;
                    }

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@StudentId", StudentId);
                    cmd.Parameters.AddWithValue("@SNo", sno);
                    cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                    cmd.Parameters.AddWithValue("@FeeItemName", beData.FeeItemName);
                    cmd.Parameters.AddWithValue("@PreviousDues", beData.PreviousDues);
                    cmd.Parameters.AddWithValue("@CurrentDues", beData.CurrentDues);
                    cmd.Parameters.AddWithValue("@TotalDues", beData.TotalDues);
                    cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
                    cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
                    cmd.Parameters.AddWithValue("@FineAmt", beData.FineAmt);
                    cmd.Parameters.AddWithValue("@ReceivableAmt", beData.ReceivableAmt);
                    cmd.Parameters.AddWithValue("@ReceivedAmt", beData.ReceivedAmt);
                    cmd.Parameters.AddWithValue("@AfterReceivedDues", beData.AfterReceivedDues);
                    cmd.Parameters.AddWithValue("@AdvanceAmt", beData.AdvanceAmt);
                    cmd.Parameters.AddWithValue("@IsManual", beData.IsManual);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                    cmd.Parameters.AddWithValue("@MonthId", beData.MonthId);
                    cmd.Parameters.AddWithValue("@PrintName", beData.PrintName);
                    cmd.Parameters.AddWithValue("@Waiver", beData.Waiver);
                    cmd.Parameters.AddWithValue("@IsAdvance", beData.IsAdvance);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddFeeReturnDetails";
                    cmd.ExecuteNonQuery();
                    sno++;
                }

            }

            var query = from det in beDataColl
                        group det by det.StudentId into g
                        select new
                        {
                            StudentId = g.Key,
                            UserId = UserId,
                            TranId = TranId,
                            PreviousDues = g.Sum(p1 => p1.PreviousDues),
                            CurrentDues = g.Sum(p1 => p1.CurrentDues),
                            TotalDues = g.Sum(p1 => p1.TotalDues),
                            DiscountAmt = g.Sum(p1 => p1.DiscountAmt),
                            FineAmt = g.Sum(p1 => p1.FineAmt),
                            ReceivableAmt = g.Sum(p1 => p1.ReceivableAmt),
                            ReceivedAmt = g.Sum(p1 => p1.ReceivedAmt),
                            AfterReceivedDues = g.Sum(p1 => p1.AfterReceivedDues),
                            DiscountPer = g.Average(p1 => p1.DiscountPer),
                            AdvanceAmt = g.Sum(p1 => p1.AdvanceAmt)
                        };

            if (query.Count() > 1)
            {

                foreach (var beData in query)
                {

                    if (!monthWise)
                    {
                        if (beData.FineAmt == 0 && beData.DiscountAmt == 0 && beData.ReceivedAmt == 0)
                            continue;
                    }

                    if (beData.StudentId > 0)
                        StudentId = beData.StudentId;
                    else if (StudentId.HasValue)
                        StudentId = StudentId.Value;
                    else
                        StudentId = null;

                    if (StudentId.HasValue && StudentId > 0)
                    {
                        System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@TranId", TranId);
                        cmd.Parameters.AddWithValue("@StudentId", StudentId);
                        cmd.Parameters.AddWithValue("@PreviousDues", beData.PreviousDues);
                        cmd.Parameters.AddWithValue("@CurrentDues", beData.CurrentDues);
                        cmd.Parameters.AddWithValue("@TotalDues", beData.TotalDues);
                        cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
                        cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
                        cmd.Parameters.AddWithValue("@FineAmt", beData.FineAmt);
                        cmd.Parameters.AddWithValue("@ReceivableAmt", beData.ReceivableAmt);
                        cmd.Parameters.AddWithValue("@ReceivedAmt", beData.ReceivedAmt);
                        cmd.Parameters.AddWithValue("@AfterReceivedDues", beData.AfterReceivedDues);
                        cmd.Parameters.AddWithValue("@AdvanceAmt", beData.AdvanceAmt);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "sp_AddFeeReturnStudent";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void SavePaymentModeDetails(int UserId, int TranId, List<BE.Fee.Transaction.FeePaymentMode> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Fee.Transaction.FeePaymentMode beData in beDataColl)
            {

                if (beData.LedgerId > 0 && beData.Amount > 0)
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@LedgerId", beData.LedgerId);
                    cmd.Parameters.AddWithValue("@Amount", beData.Amount);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddFeeReturnPaymentMode";
                    cmd.ExecuteNonQuery();
                    sno++;
                }
            }

        }

        private ResponeValues SaveFeeReturnToReceipt(int UserId, int AcademicYearId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FeeTranId", TranId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GenerateReceiptFromFeeReturn";
            cmd.ExecuteNonQuery();
            if (!(cmd.Parameters[2].Value is DBNull))
                resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

            if (!(cmd.Parameters[3].Value is DBNull))
                resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

            if (!(cmd.Parameters[4].Value is DBNull))
                resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

            if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            return resVal;
        }

        private ResponeValues GenerateSalesInvoice(int UserId, int FeeTranId)
        {
            ResponeValues resVal = new ResponeValues();

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FeeTranId", FeeTranId);
            cmd.CommandText = "usp_GenerateSalesInvoiceFromFeeReturn";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();


                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return resVal;
        }

        public ResponeValues GenerateStudentCostCenter(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GenerateStudentCostCenter";

            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "CostCenter Generated Success";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public ResponeValues GenerateFeeReturnToJournal(int UserId, int AcademicYearId, bool IsReGenerate)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@IsReGenerate", IsReGenerate);
            cmd.CommandText = "usp_ConvertAllFeeReturnToJournal";

            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[3].Value);
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
    

        public ResponeValues Cancel(BE.Fee.Transaction.FeeReceipt beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@Remarks", beData.CancelRemarks);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            cmd.CommandText = "usp_CancelFeeReturn";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }
        public BE.Fee.Transaction.FeeReceiptNo getAutoNo(BE.Fee.Transaction.FeeReceiptNo beData)
        {
            BE.Fee.Transaction.FeeReceiptNo resVal = new BE.Fee.Transaction.FeeReceiptNo();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.Add("@AutoNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@AutoManualNo", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);

            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            //cmd.CommandText = "usp_GetAutoNumberOfFeeReturn";
            cmd.CommandText = "usp_GetFeeReturnAutoManualNo";
            cmd.Parameters.AddWithValue("@CostClassId", beData.CostClassId);
            cmd.Parameters.AddWithValue("@AcademicYearId", beData.AcademicYearId);
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.AutoNumber = Convert.ToInt32(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.AutoManualNo = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                //if (resVal.IsSuccess)
                //    resVal.AutoManualNo = resVal.AutoNumber.ToString();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

   
        public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getDuesDetails(int UserId, int AcademicYearId, int StudentId, int? PaidUpToMonth, string PaidUpMonthColl, int? SemesterId = null, int? ClassYearId = null)
        {
            AcademicLib.BE.Fee.Transaction.StudentFeeReceipt beData = new AcademicLib.BE.Fee.Transaction.StudentFeeReceipt();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PaidUpToMonth", PaidUpToMonth);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@PaidUpMonthColl", PaidUpMonthColl);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetFeeDetailsForReturn";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.BillGenerateUpToMonth = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) beData.BillAmt = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) beData.TotalDebit = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.TotalCredit = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.PreviousDues = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.CurrentDues = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.TotalDues = Convert.ToDouble(reader[6]);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.FeeReceiptDetails Det = new AcademicLib.BE.Fee.Transaction.FeeReceiptDetails();
                    if (!(reader[0] is System.DBNull)) Det.FeeItemId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Det.FeeItemName = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Det.PreviousDues = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is System.DBNull)) Det.CurrentDues = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is System.DBNull)) Det.TotalDues = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is System.DBNull)) Det.DebitRate = Math.Round(Convert.ToDouble(reader[5]), 2);
                    if (!(reader[6] is System.DBNull)) Det.Rate = Math.Round(Convert.ToDouble(reader[6]), 2);

                    try
                    {
                        if (!(reader[7] is System.DBNull)) Det.MonthId = Convert.ToInt32(reader[7]);
                        if (!(reader[8] is System.DBNull)) Det.PrintName = Convert.ToString(reader[8]);
                        if (!(reader[9] is System.DBNull)) Det.PeriodName = Convert.ToString(reader[9]);
                        if (!(reader[10] is System.DBNull)) Det.Concession = Convert.ToDouble(reader[10]);
                        if (!(reader[11] is System.DBNull)) Det.PartialPaidAmt = Convert.ToDouble(reader[11]);
                    }
                    catch { }

                    beData.FeeItemWiseDuesColl.Add(Det);
                }
                reader.NextResult();

                if (reader.Read())
                {
                    if (!(reader[0] is System.DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) beData.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is System.DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is System.DBNull)) beData.FatherName = reader.GetString(6);
                    if (!(reader[7] is System.DBNull)) beData.ContactNo = reader.GetString(7);
                    if (!(reader[8] is System.DBNull)) beData.Address = reader.GetString(8);
                    if (!(reader[9] is System.DBNull)) beData.PhotoPath = reader.GetString(9);
                    if (!(reader[10] is System.DBNull)) beData.TransportRoute = reader.GetString(10);
                    if (!(reader[11] is System.DBNull)) beData.TransportPoint = reader.GetString(11);
                    if (!(reader[12] is System.DBNull)) beData.Hostel = reader.GetString(12);
                    try
                    {
                        if (!(reader[13] is System.DBNull)) beData.DisRemarks = reader.GetString(13);
                    }
                    catch { }

                }
                reader.NextResult();

                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.Receipt Det = new BE.Fee.Transaction.Receipt();
                    if (!(reader[0] is System.DBNull)) Det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Det.AutoManualNo = Convert.ToString(reader[1]);
                    if (!(reader[2] is System.DBNull)) Det.RefNo = Convert.ToString(reader[2]);
                    if (!(reader[3] is System.DBNull)) Det.VoucherDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is System.DBNull)) Det.VoucherDate_BS = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) Det.ReceivedAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is System.DBNull)) Det.DiscountAmt = Convert.ToDouble(reader[6]);
                    Det.TranType = 1;
                    beData.ReceiptColl.Add(Det);
                }
                //reader.NextResult();
                //while (reader.Read())
                //{
                //    AcademicLib.BE.Fee.Transaction.Receipt Det = new BE.Fee.Transaction.Receipt();
                //    if (!(reader[0] is System.DBNull)) Det.TranId = reader.GetInt32(0);
                //    if (!(reader[1] is System.DBNull)) Det.AutoManualNo = reader.GetString(1);
                //    if (!(reader[2] is System.DBNull)) Det.RefNo = reader.GetString(2);
                //    if (!(reader[3] is System.DBNull)) Det.VoucherDate_AD = reader.GetDateTime(3);
                //    if (!(reader[4] is System.DBNull)) Det.VoucherDate_BS = reader.GetString(4);
                //    if (!(reader[5] is System.DBNull)) Det.ReceivedAmt = Convert.ToDouble(reader[5]);
                //    if (!(reader[6] is System.DBNull)) Det.DiscountAmt = Convert.ToDouble(reader[6]);
                //    Det.TranType = 2;
                //    beData.ReceiptColl.Add(Det);
                //}
                reader.NextResult();
                beData.SiblingStudentColl = new List<BE.Fee.Transaction.SiblingStudent>();
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.SiblingStudent Det = new BE.Fee.Transaction.SiblingStudent();
                    if (!(reader[0] is System.DBNull)) Det.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Det.RegdNo = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Det.ClassName = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Det.SectionName = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Det.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is System.DBNull)) Det.Name = Convert.ToString(reader[5]);
                    if (!(reader[6] is System.DBNull)) Det.DuesAmt = Convert.ToDouble(reader[6]);
                    beData.SiblingStudentColl.Add(Det);
                }
                reader.Close();


                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }

     
        public AcademicLib.BE.Fee.Transaction.FeeReceiptCollections getAllFeeReceipt(int UserId, int AcademicYearId, int EntityId)
        {
            AcademicLib.BE.Fee.Transaction.FeeReceiptCollections dataColl = new AcademicLib.BE.Fee.Transaction.FeeReceiptCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAllFeeReturn";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.FeeReceipt beData = new AcademicLib.BE.Fee.Transaction.FeeReceipt();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoVoucherNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AutoManualNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.VoucherDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.Narration = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RefNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.StudentId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ClassId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.StudentName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.PaidUpToMonth = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.PreviousDues = reader.GetFloat(10);
                    if (!(reader[11] is DBNull)) beData.CurrentDues = reader.GetFloat(11);
                    if (!(reader[12] is DBNull)) beData.TotalDues = reader.GetFloat(12);
                    if (!(reader[13] is DBNull)) beData.DiscountPer = reader.GetFloat(13);
                    if (!(reader[14] is DBNull)) beData.DiscountAmt = reader.GetFloat(14);
                    if (!(reader[15] is DBNull)) beData.FineAmt = reader.GetFloat(15);
                    if (!(reader[16] is DBNull)) beData.ReceivableAmt = reader.GetFloat(16);
                    if (!(reader[17] is DBNull)) beData.ReceivedAmt = reader.GetFloat(17);
                    if (!(reader[18] is DBNull)) beData.AfterReceivedDues = reader.GetFloat(18);
                    if (!(reader[19] is DBNull)) beData.AdvanceAmt = reader.GetFloat(19);
                    if (!(reader[20] is DBNull)) beData.CancelDateTime = reader.GetDateTime(20);
                    if (!(reader[21] is DBNull)) beData.CancelBy = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.CancelRemarks = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.CostClassId = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.AcademicYearId = reader.GetInt32(24);

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
        public AcademicLib.BE.Fee.Transaction.FeeReceipt getFeeReceiptById(int UserId, int EntityId, int TranId)
        {
            AcademicLib.BE.Fee.Transaction.FeeReceipt beData = new AcademicLib.BE.Fee.Transaction.FeeReceipt();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetFeeReturnById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Fee.Transaction.FeeReceipt();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoVoucherNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AutoManualNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.VoucherDate = reader.GetDateTime(3);
                    if (!(reader[4] is DBNull)) beData.Narration = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RefNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.StudentId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.ClassId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.StudentName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.PaidUpToMonth = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.PreviousDues = reader.GetFloat(10);
                    if (!(reader[11] is DBNull)) beData.CurrentDues = reader.GetFloat(11);
                    if (!(reader[12] is DBNull)) beData.TotalDues = reader.GetFloat(12);
                    if (!(reader[13] is DBNull)) beData.DiscountPer = reader.GetFloat(13);
                    if (!(reader[14] is DBNull)) beData.DiscountAmt = reader.GetFloat(14);
                    if (!(reader[15] is DBNull)) beData.FineAmt = reader.GetFloat(15);
                    if (!(reader[16] is DBNull)) beData.ReceivableAmt = reader.GetFloat(16);
                    if (!(reader[17] is DBNull)) beData.ReceivedAmt = reader.GetFloat(17);
                    if (!(reader[18] is DBNull)) beData.AfterReceivedDues = reader.GetFloat(18);
                    if (!(reader[19] is DBNull)) beData.AdvanceAmt = reader.GetFloat(19);
                    if (!(reader[20] is DBNull)) beData.CancelDateTime = reader.GetDateTime(20);
                    if (!(reader[21] is DBNull)) beData.CancelBy = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.CancelRemarks = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.CostClassId = reader.GetInt32(23);
                    if (!(reader[24] is DBNull)) beData.AcademicYearId = reader.GetInt32(24);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.FeeReceiptDetails Det = new AcademicLib.BE.Fee.Transaction.FeeReceiptDetails();

                    if (!(reader[0] is System.DBNull)) Det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Det.SNo = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) Det.FeeItemId = reader.GetInt32(2);
                    if (!(reader[3] is System.DBNull)) Det.FeeItemName = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) Det.PreviousDues = reader.GetFloat(4);
                    if (!(reader[5] is System.DBNull)) Det.CurrentDues = reader.GetFloat(5);
                    if (!(reader[6] is System.DBNull)) Det.TotalDues = reader.GetFloat(6);
                    if (!(reader[7] is System.DBNull)) Det.DiscountPer = reader.GetFloat(7);
                    if (!(reader[8] is System.DBNull)) Det.DiscountAmt = reader.GetFloat(8);
                    if (!(reader[9] is System.DBNull)) Det.FineAmt = reader.GetFloat(9);
                    if (!(reader[10] is System.DBNull)) Det.ReceivableAmt = reader.GetFloat(10);
                    if (!(reader[11] is System.DBNull)) Det.ReceivedAmt = reader.GetFloat(11);
                    if (!(reader[12] is System.DBNull)) Det.AfterReceivedDues = reader.GetFloat(12);
                    if (!(reader[13] is System.DBNull)) Det.AdvanceAmt = reader.GetFloat(13);
                    if (!(reader[14] is System.DBNull)) Det.IsManual = reader.GetBoolean(14);
                    if (!(reader[15] is System.DBNull)) Det.Remarks = reader.GetString(15);
                    if (!(reader[16] is System.DBNull)) Det.MonthId = reader.GetInt32(16);
                    if (!(reader[17] is System.DBNull)) Det.PrintName = reader.GetString(17);

                    beData.DetailsColl.Add(Det);
                }

                reader.Close();
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelFeeReturnById";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[5].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

      
        public AcademicLib.RE.Fee.FeeReceiptCollections getFeeReceiptCollection(int UserId, int AcademicYearId, DateTime? dateFrom, DateTime? dateTo, bool showCancel, int? fromReceipt, int? toReceipt, ref double openingAmt, ref double openingDisAmt)
        {
            AcademicLib.RE.Fee.FeeReceiptCollections dataColl = new RE.Fee.FeeReceiptCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@ShowCancel", showCancel);
            cmd.Parameters.Add("@Opening", System.Data.SqlDbType.Float);
            cmd.Parameters.Add("@OpeningDisAmt", System.Data.SqlDbType.Float);
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@FromReceiptNo", fromReceipt);
            cmd.Parameters.AddWithValue("@ToReceiptNo", toReceipt);
            cmd.CommandText = "usp_GetFeeReturn";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeReceipt beData = new RE.Fee.FeeReceipt();
                    beData.IsParent = true;
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.ClassName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.SectionName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.AutoVoucherNo = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.AutoManualNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.RefNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Narration = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.PaidUpToMonth = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.TotalDues = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.ReceivableAmt = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.ReceivedAmt = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.AfterReceivedDues = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) beData.FatherName = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.F_ContactNo = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.MotherName = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.M_ContactNo = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.Address = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.DOB_AD = reader.GetDateTime(23);
                    if (!(reader[24] is DBNull)) beData.DOB_BS = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.UserName = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.LogDateTime = reader.GetDateTime(26);
                    if (!(reader[27] is DBNull)) beData.IsCancel = reader.GetBoolean(27);
                    if (!(reader[28] is DBNull)) beData.CancelBy = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.CancelDateTime = reader.GetDateTime(29);
                    if (!(reader[30] is DBNull)) beData.CancelRemarks = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.VoucherDate_AD = reader.GetDateTime(31);
                    if (!(reader[32] is DBNull)) beData.VoucherDate_BS = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.PaidUpToMonthName = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.BranchName = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.CostClass = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.ReceiptAsLedger = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.JVNo = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.Level = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.Faculty = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.Semester = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.ClassYear = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.Batch = reader.GetString(42);
                    if (!(reader[43] is DBNull)) beData.AcademicYearName = reader.GetString(43);
                    if (!(reader[44] is DBNull)) beData.FeeCategory = reader.GetString(44);
                    if (!(reader[45] is DBNull)) beData.FeeCategorySNo = reader.GetInt32(45);
                    if (!(reader[46] is DBNull)) beData.Waiver = Convert.ToDouble(reader[46]);
                    if (!(reader[47] is DBNull)) beData.IsNewStudent = Convert.ToBoolean(reader[47]);
                    if (!(reader[48] is DBNull)) beData.LedgerPanaNo = Convert.ToString(reader[48]);
                    dataColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeReceipt beData = new RE.Fee.FeeReceipt();
                    beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.TotalDues = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.ReceivableAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.ReceivedAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.AfterReceivedDues = Convert.ToDouble(reader[8]);

                    var findItem = dataColl.Find(p1 => p1.TranId == beData.TranId);
                    if (findItem != null)
                        findItem.DetailsColl.Add(beData);
                }
                reader.Close();

                if (!(cmd.Parameters[4].Value is DBNull))
                    openingAmt = Convert.ToDouble(cmd.Parameters[4].Value);

                if (!(cmd.Parameters[5].Value is DBNull))
                    openingDisAmt = Convert.ToDouble(cmd.Parameters[5].Value);

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
