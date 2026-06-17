using Dynamic.Dashboard.BE;
using Dynamic.DataAccess.Global;
using Dynamic.ReportEntity.SSF.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AcademicLib.DA.Fee.Transaction
{
    internal class FeeReceiptDB : Dynamic.DataAccess.Common.CommonDB
    {
        DataAccessLayer1 dal = null;
        public FeeReceiptDB(string hostName, string dbName)
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
                cmd.CommandText = "usp_UpdateFeeReceipt";
            }
            else
            {
                cmd.Parameters[23].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddFeeReceipt";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[24].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[25].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[26].Direction = System.Data.ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@ReceiptAsLedgerId", beData.ReceiptAsLedgerId);
            cmd.Parameters.AddWithValue("@ManualBillingTranId", beData.ManualBillingTranId);
            cmd.Parameters.AddWithValue("@ClassName", beData.ClassName);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@AdmissionEnquiryId", beData.AdmissionEnquiryId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@RegistrationId", beData.RegistrationId);
            cmd.Parameters.AddWithValue("@Waiver", beData.Waiver);
            cmd.Parameters.AddWithValue("@Opening", beData.Opening);
            cmd.Parameters.AddWithValue("@TenderAmount", beData.TenderAmount);
            cmd.Parameters.Add("@Gui_Id", System.Data.SqlDbType.NVarChar,100).Direction=System.Data.ParameterDirection.Output;

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
                
                if (!(cmd.Parameters["@Gui_Id"].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters["@Gui_Id"].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";

                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveFeeReceiptDetails(beData.CUserId,beData.StudentId, resVal.RId, beData.DetailsColl,beData.MonthWise);
                    SavePaymentModeDetails(beData.CUserId, resVal.RId, beData.PaymentModeColl);

                    var sales = SaveFeeReceiptToReceipt(beData.CUserId,AcademicYearId, resVal.RId);
                    if(!sales.IsSuccess)
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
        private void SaveFeeReceiptDetails(int UserId,int? StudentId, int TranId, List<BE.Fee.Transaction.FeeReceiptDetails> beDataColl,bool monthWise)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Fee.Transaction.FeeReceiptDetails beData in beDataColl)
            {
                if (beData.StudentId > 0)
                    StudentId = beData.StudentId;
                else if (StudentId.HasValue)
                {
                    StudentId = StudentId.Value;
                    beData.StudentId = beData.StudentId;
                }                    
                else
                    StudentId = null;

                //if(beData.FeeItemId.HasValue && beData.FeeItemId.Value > 0 && ( beData.FineAmt>0 || beData.DiscountAmt>0 || beData.ReceivedAmt>0 ) )
                if (beData.FeeItemId.HasValue && beData.FeeItemId.Value > 0)
                {
                    if (!monthWise)
                    {
                        if (beData.FineAmt == 0 && beData.DiscountAmt == 0 && beData.ReceivedAmt == 0 && beData.Waiver==0)
                            continue;
                    }

                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@StudentId", StudentId);
                    cmd.Parameters.AddWithValue("@SNo",sno);
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
                    cmd.Parameters.AddWithValue("@IsOpening", beData.IsOpening);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddFeeReceiptDetails";
                    cmd.ExecuteNonQuery();
                    sno++;
                }
                
            }

            foreach(var dc in beDataColl)
            {
                if (dc.StudentId == 0 && StudentId.HasValue)
                    dc.StudentId = StudentId.Value;
            }

            var query = from det in beDataColl where det.StudentId>0
                        group det by det.StudentId into g                        
                        select new
                        {
                            StudentId=g.Key,
                            UserId=UserId,
                            TranId=TranId,
                            PreviousDues=g.Sum(p1=>p1.PreviousDues), 
                            CurrentDues = g.Sum(p1 => p1.CurrentDues),
                            TotalDues = g.Sum(p1 => p1.TotalDues),
                            DiscountAmt = g.Sum(p1 => p1.DiscountAmt),
                            FineAmt = g.Sum(p1 => p1.FineAmt),
                            ReceivableAmt = g.Sum(p1 => p1.ReceivableAmt),
                            ReceivedAmt = g.Sum(p1 => p1.ReceivedAmt),
                            AfterReceivedDues = g.Sum(p1 => p1.AfterReceivedDues),
                            DiscountPer = g.Average(p1 => p1.DiscountPer),
                            AdvanceAmt=g.Sum(p1=>p1.AdvanceAmt)
                        };

            if (query.Count() > 1)
            {
                
                foreach (var  beData in query)
                {

                    if (!monthWise)
                    {
                        if (beData.FineAmt == 0 && beData.DiscountAmt == 0 && beData.ReceivedAmt == 0 )
                            continue;
                    }

                    if (beData.StudentId > 0)
                        StudentId = beData.StudentId;
                    else if (StudentId.HasValue)
                        StudentId = StudentId.Value;
                    else
                        StudentId = null;

                    if(StudentId.HasValue && StudentId > 0)
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
                        cmd.CommandText = "sp_AddFeeReceiptStudent";
                        cmd.ExecuteNonQuery();
                    }                    
                }
            }
        }

        private void SavePaymentModeDetails(int UserId,  int TranId, List<BE.Fee.Transaction.FeePaymentMode> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            int sno = 1;
            foreach (BE.Fee.Transaction.FeePaymentMode beData in beDataColl)
            {
                
                if (beData.LedgerId>0 && beData.Amount  > 0)
                {
                    System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TranId", TranId);
                    cmd.Parameters.AddWithValue("@LedgerId", beData.LedgerId);
                    cmd.Parameters.AddWithValue("@Amount", beData.Amount);
                    cmd.Parameters.AddWithValue("@Remarks", beData.Remarks); 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AddFeeReceiptPaymentMode";
                    cmd.ExecuteNonQuery();
                    sno++;
                }
            } 
           
        }

        private ResponeValues SaveFeeReceiptToReceipt(int UserId,int AcademicYearId, int TranId)
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
            cmd.CommandText = "usp_GenerateReceiptFromFeeReceipt";
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
            cmd.CommandText = "usp_GenerateSalesInvoiceFromFeeReceipt";
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
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.CommandText = "usp_GenerateStudentCostCenter";
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);
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
        public ResponeValues GenerateFeeReceiptToJournal(int UserId,int AcademicYearId,bool IsReGenerate)
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
            cmd.CommandText = "usp_ConvertAllFeeReceiptToJournal";

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
        public ResponeValues GenerateEmployeeCostCenter(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_GenerateEmployeeCostCenter";

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

        public ResponeValues Cancel(BE.Fee.Transaction.FeeReceipt beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@Remarks", beData.CancelRemarks);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);            
            cmd.CommandText = "usp_CancelFeeReceipt";
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
            cmd.Parameters.Add("@AutoManualNo", System.Data.SqlDbType.NVarChar,100);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            //cmd.CommandText = "usp_GetAutoNumberOfFeeReceipt";
            cmd.CommandText = "usp_GetFeeReceiptAutoManualNo";
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

        public AcademicLib.BE.Fee.Transaction.CostCenterGenerateLogCollections getCostCenterGenerateLog(int UserId)
        {
            AcademicLib.BE.Fee.Transaction.CostCenterGenerateLogCollections dataCOll = new BE.Fee.Transaction.CostCenterGenerateLogCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetCostCenterGenerateLog";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.CostCenterGenerateLog beData = new BE.Fee.Transaction.CostCenterGenerateLog();
                    if (!(reader[0] is DBNull)) beData.ForCostCenter = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserName = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) beData.LogDateTime_AD = Convert.ToDateTime(reader[2]);
                    if (!(reader[3] is DBNull)) beData.LogDateTime_BS = Convert.ToString(reader[3]);
                    dataCOll.Add(beData);
                }
              
                reader.Close();


                dataCOll.IsSuccess = true;
                dataCOll.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                dataCOll.IsSuccess = false;
                dataCOll.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return dataCOll;
        }

        public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getDuesDetails(int UserId,int AcademicYearId, int StudentId, int? PaidUpToMonth,string PaidUpMonthColl,int? SemesterId=null,int? ClassYearId=null, int? BatchId = null)
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
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.CommandText = "usp_GetFeeDetailsForReceipt";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {                    
                    if (!(reader[0] is DBNull)) beData.BillGenerateUpToMonth = Convert.ToString(reader[0]);
                    if (!(reader[1] is DBNull)) beData.BillAmt =Convert.ToDouble(reader[1]);
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
                    if (!(reader[5] is System.DBNull)) Det.DebitRate =Math.Round( Convert.ToDouble(reader[5]),2);
                    if (!(reader[6] is System.DBNull)) Det.Rate = Math.Round(Convert.ToDouble(reader[6]),2);

                    try
                    {
                        if (!(reader[7] is System.DBNull)) Det.MonthId = Convert.ToInt32(reader[7]);
                        if (!(reader[8] is System.DBNull)) Det.PrintName = Convert.ToString(reader[8]);
                        if (!(reader[9] is System.DBNull)) Det.PeriodName = Convert.ToString(reader[9]);
                        if (!(reader[10] is System.DBNull)) Det.Concession = Convert.ToDouble(reader[10]);
                        if (!(reader[11] is System.DBNull)) Det.PartialPaidAmt = Convert.ToDouble(reader[11]);
                        if (!(reader[12] is System.DBNull)) Det.IsOpening = Convert.ToBoolean(reader[12]);
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
                        if (!(reader[14] is System.DBNull)) beData.Batch = reader.GetString(14);
                        if (!(reader[15] is System.DBNull)) beData.Faculty = reader.GetString(15);
                        if (!(reader[16] is System.DBNull)) beData.Semester = reader.GetString(16);
                        if (!(reader[17] is System.DBNull)) beData.ClassYear = reader.GetString(17);
                        if (!(reader[18] is System.DBNull)) beData.Remarks = reader.GetString(18);
                        if (!(reader[19] is System.DBNull)) beData.BookLimit = Convert.ToInt32(reader[19]);
                        if (!(reader[20] is System.DBNull)) beData.BookCreditDays = Convert.ToInt32(reader[20]);
                        if (!(reader[21] is System.DBNull)) beData.LedgerPanaNo = reader.GetString(21);

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

        public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getDuesForAdmission(int UserId, int AcademicYearId,int ClassId, int StudentId, int PaidUpToMonth, int? SemesterId, int? ClassYearId,int? RouteId,int? PointId,int? BedId)
        {
            AcademicLib.BE.Fee.Transaction.StudentFeeReceipt beData = new AcademicLib.BE.Fee.Transaction.StudentFeeReceipt();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PaidUpToMonth", PaidUpToMonth);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId); 
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@RouteId", RouteId);
            cmd.Parameters.AddWithValue("@PointId", PointId);
            cmd.Parameters.AddWithValue("@BedId", BedId);
            cmd.CommandText = "usp_GetFeeDetailsForAdmissionReceipt";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();               
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

        public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getSiblingDuesDetails(int UserId, int AcademicYearId, int StudentId, int? PaidUpToMonth)
        {
            AcademicLib.BE.Fee.Transaction.StudentFeeReceipt beData = new AcademicLib.BE.Fee.Transaction.StudentFeeReceipt();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@PaidUpToMonth", PaidUpToMonth);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetSiblingFeeDetailsForReceipt";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) beData.BillGenerateUpToMonth = reader.GetString(0);
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
                    if (!(reader[0] is System.DBNull)) Det.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Det.FeeItemId = reader.GetInt32(1);
                    if (!(reader[2] is System.DBNull)) Det.FeeItemName = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Det.PreviousDues = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is System.DBNull)) Det.CurrentDues = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is System.DBNull)) Det.TotalDues = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is System.DBNull)) Det.DebitRate = Math.Round(Convert.ToDouble(reader[6]), 2);
                    if (!(reader[7] is System.DBNull)) Det.Rate = Math.Round(Convert.ToDouble(reader[7]), 2);
                    beData.FeeItemWiseDuesColl.Add(Det);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    BE.Fee.Transaction.SiblingStudent student = new BE.Fee.Transaction.SiblingStudent();
                    if (!(reader[0] is System.DBNull)) student.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) student.RegdNo = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) student.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is System.DBNull)) student.ClassName = reader.GetString(3);
                    if (!(reader[4] is System.DBNull)) student.SectionName = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) student.Name = reader.GetString(5);
                    if (!(reader[6] is System.DBNull)) student.FatherName = reader.GetString(6);
                    if (!(reader[7] is System.DBNull)) student.ContactNo = reader.GetString(7);
                    if (!(reader[8] is System.DBNull)) student.Address = reader.GetString(8);
                    if (!(reader[9] is System.DBNull)) student.PhotoPath = reader.GetString(9);
                    if (!(reader[10] is System.DBNull)) student.IsParent = reader.GetBoolean(10);
                    if (!(reader[11] is System.DBNull)) student.DuesAmt = Convert.ToDouble(reader[11]);
                    beData.SiblingStudentColl.Add(student);

                    if (student.IsParent)
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
                    }
                    
                }
                reader.NextResult();

                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.Receipt Det = new BE.Fee.Transaction.Receipt();
                    if (!(reader[0] is System.DBNull)) Det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Det.AutoManualNo = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Det.RefNo = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Det.VoucherDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is System.DBNull)) Det.VoucherDate_BS = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) Det.ReceivedAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is System.DBNull)) Det.DiscountAmt = Convert.ToDouble(reader[6]);
                    Det.TranType = 1;
                    beData.ReceiptColl.Add(Det);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.Receipt Det = new BE.Fee.Transaction.Receipt();
                    if (!(reader[0] is System.DBNull)) Det.TranId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Det.AutoManualNo = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Det.RefNo = reader.GetString(2);
                    if (!(reader[3] is System.DBNull)) Det.VoucherDate_AD = reader.GetDateTime(3);
                    if (!(reader[4] is System.DBNull)) Det.VoucherDate_BS = reader.GetString(4);
                    if (!(reader[5] is System.DBNull)) Det.ReceivedAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is System.DBNull)) Det.DiscountAmt = Convert.ToDouble(reader[6]);
                    Det.TranType = 2;
                    beData.ReceiptColl.Add(Det);
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

        public AcademicLib.BE.Fee.Transaction.StudentFeeReceipt getDuesDetailsForWallet(int UserId,int AcademicYearId,int? UptoMonthId)
        {
            AcademicLib.BE.Fee.Transaction.StudentFeeReceipt beData = new AcademicLib.BE.Fee.Transaction.StudentFeeReceipt();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@PaidUpToMonth", UptoMonthId);
            cmd.Parameters.Add("@StartMonthId", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@EndMonthId", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_GetFeeDetailsForAutoReceipt";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();                
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Transaction.FeeReceiptDetails Det = new AcademicLib.BE.Fee.Transaction.FeeReceiptDetails();
                    if (!(reader[0] is System.DBNull)) Det.FeeItemId = reader.GetInt32(0);
                    if (!(reader[1] is System.DBNull)) Det.FeeItemName = reader.GetString(1);
                    if (!(reader[2] is System.DBNull)) Det.TotalDues = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is System.DBNull)) Det.StudentId = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is System.DBNull)) Det.PaidUpToMonthId = Convert.ToInt32(reader[4]);
                    beData.FeeItemWiseDuesColl.Add(Det);
                }
                reader.NextResult();
                while(reader.Read())
                {
                    if (!(reader[0] is System.DBNull)) beData.StudentId = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is System.DBNull)) beData.PaidUpToMonthId = Convert.ToInt32(reader[1]);
                    if (!(reader[2] is System.DBNull)) beData.BatchId = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is System.DBNull)) beData.SemesterId = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is System.DBNull)) beData.ClassYearId = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is System.DBNull)) beData.ClassId = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is System.DBNull)) beData.ClassName = Convert.ToString(reader[6]);
                    if (!(reader[7] is System.DBNull)) beData.Name = Convert.ToString(reader[7]);
                }
                reader.Close();

                if(beData.FeeItemWiseDuesColl!=null && beData.FeeItemWiseDuesColl.Count>0)
                    beData.TotalDues = beData.FeeItemWiseDuesColl.Sum(p1 => p1.TotalDues);

                if (!(cmd.Parameters[3].Value is DBNull))
                    beData.StartMonthId = Convert.ToInt32(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    beData.EndMonthId = Convert.ToInt32(cmd.Parameters[4].Value);

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
            cmd.CommandText = "usp_GetAllFeeReceipt";
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
            cmd.CommandText = "usp_GetFeeReceiptById";
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
            cmd.CommandText = "usp_DelFeeReceiptById";
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

        public ResponeValues SendEmail(int UserId,  int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId); 
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_SendEmailOfFeeReceipt";            
            try
            {
                cmd.ExecuteNonQuery();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Re-Email Sended";
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
        public AcademicLib.RE.Fee.FeeReceiptCollections getFeeReceiptCollection(int UserId,int AcademicYearId, DateTime? dateFrom,DateTime? dateTo,bool showCancel, int? fromReceipt, int? toReceipt, ref double openingAmt,ref double openingDisAmt)
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
            cmd.CommandText = "usp_GetFeeReceipt";
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
                    if(findItem!=null)
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

        public AcademicLib.RE.Fee.FeeSummaryCollections getFeeSummary(int UserId,int AcademicYearId, int fromMonthId,int toMontherId,int forStudent, string feeItemIdColl,int? classId,int? sectionId,int? batchId,int? semesterId,int? classYearId, bool ForPaymentFollowup,int FollowupType,DateTime? dateFrom,DateTime? dateTo)
        {
            AcademicLib.RE.Fee.FeeSummaryCollections dataColl = new RE.Fee.FeeSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromMonthId", fromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", toMontherId);
            cmd.Parameters.AddWithValue("@ForStudent", forStudent);
            cmd.Parameters.AddWithValue("@FeeItemIdColl", feeItemIdColl);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", batchId);
            cmd.Parameters.AddWithValue("@SemesterId", semesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", classYearId);
            cmd.Parameters.AddWithValue("@ForPaymentFollowup", ForPaymentFollowup);
            cmd.Parameters.AddWithValue("@FollowupType", FollowupType);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetFeeSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeSummary beData = new RE.Fee.FeeSummary();                    
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FatherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.F_ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MotherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.M_ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Address = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.IsLeft = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.IsFixedStudent = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.IsHostel = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.IsNewStudent = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.IsTransport = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.Opening = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.DrAmt = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) beData.DrDiscountAmt = Convert.ToDouble(reader[18]);
                    if (!(reader[19] is DBNull)) beData.DrFineAmt = Convert.ToDouble(reader[19]);
                    if (!(reader[20] is DBNull)) beData.DrTax = Convert.ToDouble(reader[20]);
                    if (!(reader[21] is DBNull)) beData.DrTotal = Convert.ToDouble(reader[21]);
                    if (!(reader[22] is DBNull)) beData.CrAmt = Convert.ToDouble(reader[22]);
                    if (!(reader[23] is DBNull)) beData.CrDiscountAmt = Convert.ToDouble(reader[23]);
                    if (!(reader[24] is DBNull)) beData.CrFineAmt = Convert.ToDouble(reader[24]);
                    if (!(reader[25] is DBNull)) beData.TotalDebit = Convert.ToDouble(reader[25]);
                    if (!(reader[26] is DBNull)) beData.TotalCredit = Convert.ToDouble(reader[26]);
                    if (!(reader[27] is DBNull)) beData.TotalDues = Convert.ToDouble(reader[27]);
                    if (!(reader[28] is DBNull)) beData.UserId = reader.GetInt32(28);
                    if (!(reader[29] is DBNull)) beData.MonthName = reader.GetString(29);

                    if (!(reader[30] is DBNull)) beData.CardNo = reader.GetInt64(30);
                    if (!(reader[31] is DBNull)) beData.EnrollNo = reader.GetInt32(31);
                    if (!(reader[32] is DBNull)) beData.LedgerPanaNo= reader.GetString(32);

                    if (!(reader[33] is DBNull)) beData.ClassOrderNo = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.FeeItemName = reader.GetString(34);

                    if (!(reader[35] is DBNull)) beData.RouteName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.PointName = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.BoardersName = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.AutoNumber = reader.GetInt32(38);

                    if (!(reader[39] is DBNull)) beData.LastReceiptDate = reader.GetDateTime(39);
                    if (!(reader[40] is DBNull)) beData.LastReceiptMiti = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.LastReceiptNo = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.LastReceiptAmt = Convert.ToDouble(reader[42]);
                    if (!(reader[43] is DBNull)) beData.FutureDR = Convert.ToDouble(reader[43]);
                    if (!(reader[44] is DBNull)) beData.FutureCR = Convert.ToDouble(reader[44]);

                    if (!(reader[45] is DBNull)) beData.Level = reader.GetString(45);
                    if (!(reader[46] is DBNull)) beData.Faculty = reader.GetString(46);
                    if (!(reader[47] is DBNull)) beData.Semester = reader.GetString(47);
                    if (!(reader[48] is DBNull)) beData.ClassYear = reader.GetString(48);
                    if (!(reader[49] is DBNull)) beData.Batch = reader.GetString(49);
                    if (!(reader[50] is DBNull)) beData.Email = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.IsDefaulter = reader.GetBoolean(51);

                    if (ForPaymentFollowup)
                    {
                        if (!(reader[52] is DBNull)) beData.RefTranId = reader.GetInt32(52);
                        if (!(reader[53] is DBNull)) beData.FollowupRemarks = reader.GetString(53);
                        if (!(reader[54] is DBNull)) beData.NextFollowupDateTime = reader.GetDateTime(54);
                        if (!(reader[55] is DBNull)) beData.NextFollowupMiti = reader.GetString(55);
                        if (!(reader[56] is DBNull)) beData.NextFollowupBy = reader.GetString(56);
                        if (!(reader[57] is DBNull)) beData.ClosedRemarks = reader.GetString(57);
                        if (!(reader[58] is DBNull)) beData.ClosedBy = reader.GetString(58);
                        if (!(reader[59] is DBNull)) beData.ClosedDate = reader.GetDateTime(59);
                        if (!(reader[60] is DBNull)) beData.ClosedMiti = reader.GetString(60);
                    }

                    try
                    {
                        
                        if (!(reader[61] is DBNull)) beData.LeftDate = reader.GetDateTime(61);
                        if (!(reader[62] is DBNull)) beData.LeftMiti= reader.GetString(62);
                        if (!(reader[63] is DBNull)) beData.LeftReason = reader.GetString(63);

                        if (!(reader[64] is DBNull)) beData.HouseName = reader.GetString(64);
                        if (!(reader[65] is DBNull)) beData.HouseDress = reader.GetString(65);
                        if (!(reader[66] is DBNull)) beData.VehicleName = reader.GetString(66);
                        if (!(reader[67] is DBNull)) beData.VehicleNumber = reader.GetString(67);
                        if (!(reader[68] is DBNull)) beData.ParentStudentId = reader.GetInt32(68);
                        if (!(reader[69] is DBNull)) beData.Gender = Convert.ToString(reader[69]);
                        if (!(reader[70] is DBNull)) beData.FollowupStatus = Convert.ToString(reader[70]);
                        if (!(reader[71] is DBNull)) beData.ContactNo = Convert.ToString(reader[71]);
                        if (!(reader[72] is DBNull)) beData.G_ContactNo = Convert.ToString(reader[72]);
                    }
                    catch { }

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

        public AcademicLib.RE.Fee.FeeSummaryCollections getFeeSummaryDateWise(int UserId, int? AcademicYearId,   string feeItemIdColl, DateTime? dateFrom, DateTime? dateTo)
        {
            AcademicLib.RE.Fee.FeeSummaryCollections dataColl = new RE.Fee.FeeSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId); 
            cmd.Parameters.AddWithValue("@FeeItemIdColl", feeItemIdColl); 
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId); 
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.CommandText = "usp_GetFeeSummaryDateWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeSummary beData = new RE.Fee.FeeSummary();
                    if (!(reader[0] is DBNull)) beData.AcademicYear = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Name = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.Address = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.RegdNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FeeItemName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Opening = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.DrAmt = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.CrAmt = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Closing = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.DrDiscountAmt = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.CrDiscountAmt = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.FeeOrderNo = Convert.ToInt32(reader[14]);
                    if (!(reader[15] is DBNull)) beData.AutoNumber = Convert.ToInt32(reader[15]);

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

        public AcademicLib.RE.Fee.FeeSummary_PCCollections getFeeSummary_PC(int UserId, int AcademicYearId, int fromMonthId, int toMontherId, int forStudent, string feeItemIdColl, int? classId, int? sectionId)
        {
            AcademicLib.RE.Fee.FeeSummary_PCCollections dataColl = new RE.Fee.FeeSummary_PCCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromMonthId", fromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", toMontherId);
            cmd.Parameters.AddWithValue("@ForStudent", forStudent);
            cmd.Parameters.AddWithValue("@FeeItemIdColl", feeItemIdColl);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeSummaryPreviousCurrent";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeSummary_PC beData = new RE.Fee.FeeSummary_PC();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegdNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FatherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.F_ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MotherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.M_ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Address = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.IsLeft = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.IsFixedStudent = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.IsHostel = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.IsNewStudent = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.IsTransport = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.Opening_P = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.DrAmt_P = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) beData.DrDiscountAmt_P = Convert.ToDouble(reader[18]);
                    if (!(reader[19] is DBNull)) beData.DrFineAmt_P = Convert.ToDouble(reader[19]);
                    if (!(reader[20] is DBNull)) beData.DrTax_P = Convert.ToDouble(reader[20]);
                    if (!(reader[21] is DBNull)) beData.DrTotal_P = Convert.ToDouble(reader[21]);
                    if (!(reader[22] is DBNull)) beData.CrAmt_P = Convert.ToDouble(reader[22]);
                    if (!(reader[23] is DBNull)) beData.CrDiscountAmt_P = Convert.ToDouble(reader[23]);
                    if (!(reader[24] is DBNull)) beData.CrFineAmt_P = Convert.ToDouble(reader[24]);
                    if (!(reader[25] is DBNull)) beData.TotalDebit_P = Convert.ToDouble(reader[25]);
                    if (!(reader[26] is DBNull)) beData.TotalCredit_P = Convert.ToDouble(reader[26]);
                    if (!(reader[27] is DBNull)) beData.TotalDues_P = Convert.ToDouble(reader[27]);
                    if (!(reader[28] is DBNull)) beData.UserId = reader.GetInt32(28);
                    if (!(reader[29] is DBNull)) beData.MonthName = reader.GetString(29);

                    if (!(reader[30] is DBNull)) beData.CardNo = reader.GetInt64(30);
                    if (!(reader[31] is DBNull)) beData.EnrollNo = reader.GetInt32(31);
                    if (!(reader[32] is DBNull)) beData.LedgerPanaNo = reader.GetString(32);

                    if (!(reader[33] is DBNull)) beData.ClassOrderNo = reader.GetInt32(33);
                    if (!(reader[34] is DBNull)) beData.FeeItemName = reader.GetString(34);

                    if (!(reader[35] is DBNull)) beData.RouteName = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.PointName = reader.GetString(36);
                    if (!(reader[37] is DBNull)) beData.BoardersName = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.AutoNumber = reader.GetInt32(38);


                    if (!(reader[39] is DBNull)) beData.DrAmt_C = Convert.ToDouble(reader[39]);
                    if (!(reader[40] is DBNull)) beData.DrDiscountAmt_C = Convert.ToDouble(reader[40]);
                    if (!(reader[41] is DBNull)) beData.DrFineAmt_C = Convert.ToDouble(reader[41]);
                    if (!(reader[42] is DBNull)) beData.DrTax_C = Convert.ToDouble(reader[42]);
                    if (!(reader[43] is DBNull)) beData.DrTotal_C = Convert.ToDouble(reader[43]);
                    if (!(reader[44] is DBNull)) beData.CrAmt_C = Convert.ToDouble(reader[44]);
                    if (!(reader[45] is DBNull)) beData.CrDiscountAmt_C = Convert.ToDouble(reader[45]);
                    if (!(reader[46] is DBNull)) beData.CrFineAmt_C = Convert.ToDouble(reader[46]);
                    if (!(reader[47] is DBNull)) beData.TotalDebit_C = Convert.ToDouble(reader[47]);
                    if (!(reader[48] is DBNull)) beData.TotalCredit_C = Convert.ToDouble(reader[48]);
                    if (!(reader[49] is DBNull)) beData.TotalDues_C = Convert.ToDouble(reader[49]);

                    if (!(reader[50] is DBNull)) beData.Level = reader.GetString(50);
                    if (!(reader[51] is DBNull)) beData.Faculty = reader.GetString(51);
                    if (!(reader[52] is DBNull)) beData.Semester = reader.GetString(52);
                    if (!(reader[53] is DBNull)) beData.ClassYear = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.Batch = reader.GetString(54);
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

        public AcademicLib.RE.Fee.ReminderSlipCollections getReminderSlip(int UserId, int AcademicYearId, int UptoMonthId, int forStudent,int? classId,int? sectionId, int? BatchId, int? ClassYearId, int? SemesterId)
        {
            AcademicLib.RE.Fee.ReminderSlipCollections dataColl = new RE.Fee.ReminderSlipCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@UptoMonthId", UptoMonthId);            
            cmd.Parameters.AddWithValue("@ForStudent", forStudent);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.CommandText = "usp_GetFeeReminderSummary";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.ReminderSlip beData = new RE.Fee.ReminderSlip();
                    beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.SectionName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.FatherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.F_ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.MotherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.M_ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Address = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.IsLeft = reader.GetBoolean(11);
                    if (!(reader[12] is DBNull)) beData.IsFixedStudent = reader.GetBoolean(12);
                    if (!(reader[13] is DBNull)) beData.IsHostel = reader.GetBoolean(13);
                    if (!(reader[14] is DBNull)) beData.IsNewStudent = reader.GetBoolean(14);
                    if (!(reader[15] is DBNull)) beData.IsTransport = reader.GetBoolean(15);                    
                    if (!(reader[16] is DBNull)) beData.Debit = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.Credit = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) beData.Balance = Convert.ToDouble(reader[18]);
                    if (!(reader[19] is DBNull)) beData.UserId = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.TransportRoute = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.TransportPoint = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.RoomName = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.ReminderNotes = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.CompName = reader.GetString(24);
                    if (!(reader[25] is DBNull)) beData.CompAddress = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.CompPhoneNo = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.CompFaxNo = reader.GetString(27);
                    if (!(reader[28] is DBNull)) beData.CompEmailId = reader.GetString(28);
                    if (!(reader[29] is DBNull)) beData.CompWebSite = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.CompLogoPath = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.CompImgPath = reader.GetString(31);
                    if (!(reader[32] is DBNull)) beData.CompBannerPath = reader.GetString(32);
                    if (!(reader[33] is DBNull)) beData.CompanyRegdNo = reader.GetString(33);
                    if (!(reader[34] is DBNull)) beData.CompanyPanVat = reader.GetString(34);
                    if (!(reader[35] is DBNull)) beData.UptoMonth = reader.GetString(35);
                    if (!(reader[36] is DBNull)) beData.ClassOrderNo = reader.GetInt32(36);
                    if (!(reader[37] is DBNull)) beData.Level = reader.GetString(37);
                    if (!(reader[38] is DBNull)) beData.Faculty = reader.GetString(38);
                    if (!(reader[39] is DBNull)) beData.Semester = reader.GetString(39);
                    if (!(reader[40] is DBNull)) beData.ClassYear = reader.GetString(40);
                    if (!(reader[41] is DBNull)) beData.Batch = reader.GetString(41);
                    if (!(reader[42] is DBNull)) beData.Email = reader.GetString(42);
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


        public AcademicLib.API.Student.Fee getStudentFee(int UserId,int? AcademicYearId )
        {
            AcademicLib.API.Student.Fee fee = new API.Student.Fee();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeDetailsForApp";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    fee = new API.Student.Fee();                                       
                    if (!(reader[0] is DBNull)) fee.Opening = Convert.ToDouble(reader[0]);
                    if (!(reader[1] is DBNull)) fee.FeeAmt = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) fee.DiscountAmt = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) fee.PaidAmt = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) fee.DuesAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) fee.MonthId = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) fee.MonthName = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) fee.SemesterId = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) fee.ClassYearId = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) fee.Semester = Convert.ToString(reader[9]);
                    if (!(reader[10] is DBNull)) fee.ClassYear = Convert.ToString(reader[10]);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.Student.MonthlySummary beData = new API.Student.MonthlySummary();
                    if (!(reader[0] is DBNull)) beData.MonthId = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.MonthName = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Opening = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.FeeAmt = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.PaidAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.SemesterId = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) beData.ClassYearId = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Semester = Convert.ToString(reader[9]);
                    if (!(reader[10] is DBNull)) beData.ClassYear = Convert.ToString(reader[10]);
                    fee.MonthlySummaryColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.Student.FeeItem beData = new API.Student.FeeItem();
                    if (!(reader[0] is DBNull)) beData.MonthId = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.FeeItemName = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Amount = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.SemesterId = Convert.ToInt32(reader[3]);
                    if (!(reader[4] is DBNull)) beData.ClassYearId = Convert.ToInt32(reader[4]);
                    if (!(reader[5] is DBNull)) beData.Semester = Convert.ToString(reader[5]);
                    if (!(reader[6] is DBNull)) beData.ClassYear = Convert.ToString(reader[6]);
                    var find = fee.MonthlySummaryColl.Find(p1 => p1.MonthId == beData.MonthId && p1.SemesterId == beData.SemesterId && p1.ClassYearId == beData.ClassYearId);
                    if(find!=null && find.FeeItemColl!=null)
                        find.FeeItemColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.Student.Receipt beData = new API.Student.Receipt();
                    if (!(reader[0] is DBNull)) beData.MonthId = Convert.ToInt32(reader[0]);
                    if (!(reader[1] is DBNull)) beData.MonthName = Convert.ToString(reader[1]);
                    if (!(reader[2] is DBNull)) beData.TranId = Convert.ToInt32(reader[2]);
                    if (!(reader[3] is DBNull)) beData.VoucherDate_AD = Convert.ToDateTime(reader[3]);
                    if (!(reader[4] is DBNull)) beData.VoucherDate_BS = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) beData.ReceiptNo = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) beData.RefNo = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) beData.ReceiptAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Narration = Convert.ToString(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Dues = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.SemesterId = Convert.ToInt32(reader[11]);
                    if (!(reader[12] is DBNull)) beData.ClassYearId = Convert.ToInt32(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Semester = Convert.ToString(reader[13]);
                    if (!(reader[14] is DBNull)) beData.ClassYear = Convert.ToString(reader[14]);
                    var find = fee.MonthlySummaryColl.Find(p1 => p1.MonthId == beData.MonthId && p1.SemesterId == beData.SemesterId && p1.ClassYearId == beData.ClassYearId);
                    if(find!=null && find.ReceiptColl!=null)
                        find.ReceiptColl.Add(beData);
                    else
                    {
                        fee.MonthlySummaryColl.Add(new API.Student.MonthlySummary()
                        {
                            MonthId = beData.MonthId,
                            MonthName = beData.MonthName,
                            SemesterId = beData.SemesterId,
                            ClassYearId = beData.ClassYearId,
                            Semester = beData.Semester,
                            ClassYear = beData.ClassYear,
                            ReceiptColl = new List<API.Student.Receipt>() { beData }
                        });
                    }

                }

                reader.Close();
                fee.IsSuccess = true;
                fee.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                fee.IsSuccess = false;
                fee.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return fee;
        }
        public AcademicLib.API.Student.Fee getStudentFeeForWallet(int UserId, int AcademicYearId)
        {
            AcademicLib.API.Student.Fee fee = new API.Student.Fee();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeDuesForWallet";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    fee = new API.Student.Fee();
                    if (!(reader[0] is DBNull)) fee.Opening = Convert.ToDouble(reader[0]);
                    if (!(reader[1] is DBNull)) fee.FeeAmt = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) fee.DiscountAmt = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) fee.PaidAmt = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) fee.DuesAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) fee.MonthId = Convert.ToInt32(reader[5]);
                    if (!(reader[6] is DBNull)) fee.MonthName = Convert.ToString(reader[6]);
                }
                reader.Close();
                fee.IsSuccess = true;
                fee.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                fee.IsSuccess = false;
                fee.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return fee;
        }
        public AcademicLib.RE.Fee.DateWiseIncomeCollections getDateWiseFeeIncome(int UserId,int AcademicYearId, int? MonthId,DateTime? dateFrom,DateTime? dateTo,int? fromReceipt,int? toReceipt)
        {
            AcademicLib.RE.Fee.DateWiseIncomeCollections dataColl = new RE.Fee.DateWiseIncomeCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@MonthId", MonthId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@FromReceiptNo", fromReceipt);
            cmd.Parameters.AddWithValue("@ToReceiptNo", toReceipt);
            cmd.CommandText = "usp_GetDateWiseFeeIncome";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.DateWiseIncome fee = new RE.Fee.DateWiseIncome();
                    if (!(reader[0] is DBNull)) fee.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) fee.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) fee.Name = reader.GetString(2);
                    if (!(reader[3] is DBNull)) fee.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) fee.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) fee.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) fee.FatherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) fee.F_ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) fee.VoucherDate_AD = reader.GetDateTime(8);
                    if (!(reader[9] is DBNull)) fee.VoucherDate_BS = reader.GetString(9);
                    if (!(reader[10] is DBNull)) fee.AutoVoucherNo = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) fee.AutoManualNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) fee.FeeSNo = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) fee.FeeItemName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) fee.HeadFor =((BE.Fee.Creation.HEADFOR)reader.GetInt32(14)).ToString();
                    if (!(reader[15] is DBNull)) fee.ReceivedAmt = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) fee.DiscountAmt = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) fee.FineAmt = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) fee.CreateBy = Convert.ToString(reader[18]);
                    if (!(reader[19] is DBNull)) fee.RefNo = reader.GetString(19);
                    if (!(reader[20] is DBNull)) fee.Narration = reader.GetString(20);

                    if (!(reader[21] is DBNull)) fee.PaidUpToMonth = reader.GetString(21);
                    if (!(reader[22] is DBNull)) fee.ReceiptMonth = reader.GetString(22);
                    if (!(reader[23] is DBNull)) fee.Address = reader.GetString(23);
                    if (!(reader[24] is DBNull)) fee.TransportPoint = reader.GetString(24);
                    if (!(reader[25] is DBNull)) fee.ReceiptAs = reader.GetString(25);
                    if (!(reader[26] is DBNull)) fee.JvNo = reader.GetString(26);
                    if (!(reader[27] is DBNull)) fee.Level = reader.GetString(27);
                    if (!(reader[28] is DBNull)) fee.Faculty = reader.GetString(28);
                    if (!(reader[29] is DBNull)) fee.Semester = reader.GetString(29);
                    if (!(reader[30] is DBNull)) fee.ClassYear = reader.GetString(30);
                    if (!(reader[31] is DBNull)) fee.Batch = reader.GetString(31);
                    if (!(reader[32] is DBNull)) fee.AfterReceivedDues = Convert.ToDouble(reader[32]);
                    dataColl.Add(fee);
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

        public AcademicLib.RE.Fee.StudentVoucher getStudentVoucher(int UserId, int AcademicYearId, int StudentId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.RE.Fee.StudentVoucher voucher = new RE.Fee.StudentVoucher();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetStudentVoucher";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) voucher.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) voucher.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) voucher.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) voucher.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) voucher.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) voucher.FatherName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) voucher.MotherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) voucher.ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) voucher.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) voucher.PhotoPath = reader.GetString(9);
                    if (!(reader[10] is DBNull)) voucher.BillUpToMonth = reader.GetString(10);
                    if (!(reader[11] is DBNull)) voucher.Name = reader.GetString(11);
                    if (!(reader[12] is DBNull)) voucher.Level = reader.GetString(12);
                    if (!(reader[13] is DBNull)) voucher.Faculty = reader.GetString(13);
                    if (!(reader[14] is DBNull)) voucher.Semester = reader.GetString(14);
                    if (!(reader[15] is DBNull)) voucher.ClassYear = reader.GetString(15);
                    if (!(reader[16] is DBNull)) voucher.Batch = reader.GetString(16);
                    try
                    {
                        if (!(reader[17] is DBNull)) voucher.AcademicYear = reader.GetString(17);
                    }
                    catch { }
                }
                reader.NextResult();
                double curClosing = 0;
                while (reader.Read())
                {
                    RE.Fee.Voucher beData = new RE.Fee.Voucher();
                    if (!(reader[0] is DBNull)) beData.TranType = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.TranId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.VoucherDate_AD = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.VoucherDate_BS = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.VoucherType = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.VoucherNo = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.RefNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.Particulars = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.Amount = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.DisAmt = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.Debit = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Credit = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.Narration = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.UserName = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.LogDateTime = reader.GetDateTime(14);
                    if (!(reader[15] is DBNull)) beData.ATranId = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.AVoucherNo = reader.GetString(16);
                    try
                    {
                        if (!(reader["Semester"] is DBNull)) beData.Semester = Convert.ToString(reader["Semester"]);
                        if (!(reader["ClassYear"] is DBNull)) beData.ClassYear = Convert.ToString(reader["ClassYear"]);
                    }
                    catch { }
                    curClosing += beData.Debit - beData.Credit;
                    beData.CurClosing = curClosing;
                    voucher.VoucherColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    RE.Fee.VoucherDetails beData = new RE.Fee.VoucherDetails();
                    if (!(reader[0] is DBNull)) beData.FeeItem = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Amount = Convert.ToDouble(reader[1]);

                    try
                    {
                        voucher.VoucherColl.Find(p1 => p1.TranType == 1).DetailsColl.Add(beData);
                    }
                    catch { }
                    
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int tranId = 0;
                    RE.Fee.VoucherDetails beData = new RE.Fee.VoucherDetails();
                    if (!(reader[0] is DBNull)) tranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FeeItem = reader.GetString(1);                    
                    if (!(reader[2] is DBNull)) beData.Qty = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Rate = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Amount = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.DisAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.FeeSource = reader.GetString(9);
                    try
                    {
                        voucher.VoucherColl.Find(p1 => p1.TranType == 2 && p1.TranId == tranId).DetailsColl.Add(beData);
                    }
                    catch { }
                    
                }
                reader.NextResult();
                while (reader.Read())
                {
                    int tranId = 0;
                    RE.Fee.VoucherDetails beData = new RE.Fee.VoucherDetails();
                    if (!(reader[0] is DBNull)) tranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FeeItem = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.DisAmt = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Amount = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.IsManual = Convert.ToString(reader[4]);
                    if (!(reader[5] is DBNull)) beData.Remarks = Convert.ToString(reader[5]);
                    try
                    {
                        voucher.VoucherColl.Find(p1 => p1.TranType == 3 && p1.TranId == tranId).DetailsColl.Add(beData);
                    }
                    catch { }
                 
                }

                reader.NextResult();
                while (reader.Read())
                {
                    int tranId = 0;
                    RE.Fee.VoucherDetails beData = new RE.Fee.VoucherDetails();
                    if (!(reader[0] is DBNull)) tranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FeeItem = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.Qty = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Rate = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.Amount = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.DisAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.TaxAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.FineAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.FeeSource = reader.GetString(9);
                    try
                    {
                        voucher.VoucherColl.Find(p1 => (p1.TranType == 4 || p1.TranType == 5 || p1.TranType == 6) && p1.TranId == tranId).DetailsColl.Add(beData);
                    }
                    catch { }
                    
                }

                voucher.OpeningAmt = voucher.VoucherColl.Where(p1=>p1.TranType==1).Sum(p1 => p1.Debit);
                voucher.FeeAmt = voucher.VoucherColl.Where(p1 => p1.TranType == 2 || p1.TranType == 4 || p1.TranType == 5 || p1.TranType == 6).Sum(p1 => p1.Debit-p1.Credit);
                voucher.DiscountAmt = voucher.VoucherColl.Where(p1 => p1.TranType == 3).Sum(p1 => p1.DisAmt);
                voucher.PaidAmt = voucher.VoucherColl.Where(p1 => p1.TranType == 3).Sum(p1 => p1.Amount);
                voucher.BalanceAmt = voucher.VoucherColl.Sum(p1 => p1.Debit - p1.Credit);

                reader.Close();
                voucher.IsSuccess = true;
                voucher.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                voucher.IsSuccess = false;
                voucher.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return voucher;
        }

        public AcademicLib.RE.Fee.StudentVoucher getStudentLedger(int UserId, int AcademicYearId, int StudentId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.RE.Fee.StudentVoucher voucher = new RE.Fee.StudentVoucher();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_GetStudentLedger";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!(reader[0] is DBNull)) voucher.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) voucher.RegNo = reader.GetString(1);
                    if (!(reader[2] is DBNull)) voucher.RollNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) voucher.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) voucher.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) voucher.FatherName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) voucher.MotherName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) voucher.ContactNo = reader.GetString(7);
                    if (!(reader[8] is DBNull)) voucher.Address = reader.GetString(8);
                    if (!(reader[9] is DBNull)) voucher.PhotoPath = reader.GetString(9);
                    if (!(reader[10] is DBNull)) voucher.BillUpToMonth = reader.GetString(10);
                    if (!(reader[11] is DBNull)) voucher.Name = reader.GetString(11);
                    if (!(reader[12] is DBNull)) voucher.OpeningAmt = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) voucher.FeeAmt = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) voucher.PaidAmt = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) voucher.DiscountAmt = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) voucher.BalanceAmt = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) voucher.Level = reader.GetString(17);
                    if (!(reader[18] is DBNull)) voucher.Faculty = reader.GetString(18);
                    if (!(reader[19] is DBNull)) voucher.Semester = reader.GetString(19);
                    if (!(reader[20] is DBNull)) voucher.ClassYear = reader.GetString(20);
                    if (!(reader[21] is DBNull)) voucher.Batch = reader.GetString(21);
                    try
                    {
                        if (!(reader[22] is DBNull)) voucher.AcademicYear = reader.GetString(22);
                    }
                    catch { }
                }

                reader.NextResult();                
                while (reader.Read())
                {
                    RE.Fee.LedgerDetails beData = new RE.Fee.LedgerDetails();
                    if (!(reader[0] is DBNull)) beData.YearId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.MonthId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.TranType = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.Particular =beData.MonthId.ToString()+" "+ reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.FeeHeading = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.PDues = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Debit = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Credit = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.Paid = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.Discount = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.VoucherNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.RefNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.VoucherDate = reader.GetString(12);                    
                    if (!(reader[13] is DBNull)) beData.Details = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.FeeItemOrderNo = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.Narration = reader.GetString(15);
                    beData.BalanceAmt = beData.PDues + beData.Debit - beData.Credit;
                    voucher.LedgerDetailsColl.Add(beData);
                }

               
                reader.Close();
                voucher.IsSuccess = true;
                voucher.ResponseMSG = GLOBALMSG.SUCCESS;

            }
            catch (Exception ee)
            {
                voucher.IsSuccess = false;
                voucher.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return voucher;
        }

        public AcademicLib.RE.Fee.FeeSummaryClassWiseCollections getFeeSummaryClassWise(int UserId,int AcademicYearId, int fromMonthId, int toMontherId, int forStudent)
        {
            AcademicLib.RE.Fee.FeeSummaryClassWiseCollections dataColl = new RE.Fee.FeeSummaryClassWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromMonthId", fromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", toMontherId);
            cmd.Parameters.AddWithValue("@ForStudent", forStudent);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeSummaryClassWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeSummaryClassWise beData = new RE.Fee.FeeSummaryClassWise();                    
                    if (!(reader[0] is DBNull)) beData.ClassOrderNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.FeeItemOrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.FeeItemName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.OpeningAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.Debit = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Credit = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.Discount = Convert.ToDouble(reader[8]);                    
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

        public AcademicLib.RE.Fee.FeeSummaryStudentWiseCollections getFeeSummaryStudentWise(int UserId,int AcademicYearId, int ClassId,int? SectionId, int fromMonthId, int toMontherId, int forStudent)
        {
            AcademicLib.RE.Fee.FeeSummaryStudentWiseCollections dataColl = new RE.Fee.FeeSummaryStudentWiseCollections();

            if (SectionId == 0)
                SectionId = null;

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromMonthId", fromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", toMontherId);
            cmd.Parameters.AddWithValue("@ForStudent", forStudent);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeSummaryStudentWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeSummaryStudentWise beData = new RE.Fee.FeeSummaryStudentWise();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);                    
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.F_ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.FeeItemOrderNo = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.FeeItemName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.OpeningAmt = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Debit = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.Credit = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.Discount = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.Level = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Faculty = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Semester = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.ClassYear = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.Batch = reader.GetString(20);
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

        public AcademicLib.RE.Fee.FeeIncomeClassWiseCollections getFeeIncomeClassWise(int UserId,int AcademicYearId, DateTime dateFrom, DateTime dateTo,string feeItemIdColl)
        {
            AcademicLib.RE.Fee.FeeIncomeClassWiseCollections dataColl = new RE.Fee.FeeIncomeClassWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@FeeItemIdColl", feeItemIdColl);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeIncomeClassWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeIncomeClassWise beData = new RE.Fee.FeeIncomeClassWise();
                    if (!(reader[0] is DBNull)) beData.ClassOrderNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.FeeItemOrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.FeeItemName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ReceivedAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[6]);
                  
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

        public AcademicLib.RE.Fee.FeeIncomeStudentWiseCollections getFeeIncomeStudentWise(int UserId,int AcademicYearId, int ClassId, int? SectionId, DateTime dateFrom, DateTime dateTo, string feeItemIdColl)
        {
            AcademicLib.RE.Fee.FeeIncomeStudentWiseCollections dataColl = new RE.Fee.FeeIncomeStudentWiseCollections();

            if (SectionId == 0)
                SectionId = null;

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@FeeItemIdColl", feeItemIdColl);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeIncomeStudentWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.FeeIncomeStudentWise beData = new RE.Fee.FeeIncomeStudentWise();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Name = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.F_ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.Address = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.FeeItemOrderNo = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.FeeItemName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ReceivedAmt = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.VoucherNo = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.RefNo = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.VoucherDate = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Details = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Level = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Faculty = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.Semester = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.ClassYear = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.Batch = reader.GetString(22);
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

        public AcademicLib.RE.Fee.OnlinePaymentCollections getOnlinePaymentList(int UserId,int AcademicYearId, DateTime dateFrom, DateTime dateTo)
        {
            AcademicLib.RE.Fee.OnlinePaymentCollections dataColl = new RE.Fee.OnlinePaymentCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetOnlinePaymentList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.OnlinePayment beData = new RE.Fee.OnlinePayment();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SourceName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RegdNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RollNo = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.FatherName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Amount = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.RefId = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.MobileNo = Convert.ToString(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Notes = Convert.ToString(reader[13]);
                    if (!(reader[14] is DBNull)) beData.FromReq = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.LogDateTime_AD = reader.GetDateTime(15);
                    if (!(reader[16] is DBNull)) beData.LogDateTime_BS = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Level = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.Faculty = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Semester = reader.GetString(19);
                    if (!(reader[20] is DBNull)) beData.ClassYear = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.Batch = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.ReceiptNo = Convert.ToString(reader[22]);
                    if (!(reader[23] is DBNull)) beData.ReceiptAsLedger = Convert.ToString(reader[23]);
                    if (!(reader[24] is DBNull)) beData.PaidUptoMonth = Convert.ToString(reader[24]);
                    beData.LogDateTime_BS = beData.LogDateTime_BS + " " + beData.LogDateTime_AD.ToString("HH:mm tt");
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

        public AcademicLib.API.Admin.FeeDuesCollections admin_DuesList(int UserId,int AcademicYearId, int? classId,int? sectionId, int UpToMonthId=0,int ForStudent=0,int? BatchId=null,int? SemesterId=null,int? ClassYearId=null)
        {
            AcademicLib.API.Admin.FeeDuesCollections dataColl = new API.Admin.FeeDuesCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@UpToMonthId", UpToMonthId);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@ForStudent", ForStudent);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_admin_DuesList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.FeeDues beData = new API.Admin.FeeDues();
                    if (!(reader[0] is DBNull)) beData.StudentId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.UserId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.RegNo = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassName = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.SectionName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.RollNo = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.FatherName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.ContactNo = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.IsLeft = reader.GetBoolean(9);
                    if (!(reader[10] is DBNull)) beData.Debit = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Credit = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.Dues = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.DrDiscountAmt = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.CrDiscountAmt = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.PhotoPath = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.Semester = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.ClassYear = reader.GetString(17);

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

        public AcademicLib.API.Admin.DailyFeeReceipt admin_DailyCollection(int UserId,int AcademicYearId,DateTime? dateFrom,DateTime? dateTo)
        {
            AcademicLib.API.Admin.DailyFeeReceipt resVal = new API.Admin.DailyFeeReceipt();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_admin_DailyCollection";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.FeeItem beData = new API.Admin.FeeItem();
                    if (!(reader[0] is DBNull)) beData.SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.FeeItemId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.FeeHeading = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ReceivedAmt = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[4]);
                    resVal.FeeHeadingWiseColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.FeeReceipt beData = new API.Admin.FeeReceipt();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.StudentId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.AutoVoucherNo = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.RefNo = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.VoucherDateAD = reader.GetDateTime(4);
                    if (!(reader[5] is DBNull)) beData.VoucherDateBS = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Name = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SectionName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.RollNo = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.RegNo = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.FatherName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ContactNo = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[13]);                    
                    if (!(reader[14] is DBNull)) beData.ReceivedAmt = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.PhotoPath = reader.GetString(16);
                    resVal.ReceiptColl.Add(beData);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.FeeItem beData = new API.Admin.FeeItem();
                    int tranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.SNo = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.FeeItemId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.FeeHeading = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.ReceivedAmt = Convert.ToDouble(reader[5]);
                    resVal.ReceiptColl.Find(p1=>p1.TranId==tranId).FeeHeadingWiseColl.Add(beData);
                }
                reader.Close();
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;

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

        public AcademicLib.API.Admin.ClassWiseFeeSummaryCollections admin_ClassWiseFeeSummary(int UserId,int? StudentId, int fromMonthId,int toMonthId,int ForStudent,int? ClassId,int? SectionId,int? BatchId,int? SemesterId,int? ClassYearId)
        {
            AcademicLib.API.Admin.ClassWiseFeeSummaryCollections dataColl = new API.Admin.ClassWiseFeeSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromMonthId", fromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", toMonthId);
            cmd.Parameters.AddWithValue("@ForStudent", ForStudent);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            cmd.Parameters.AddWithValue("@SectionId", SectionId);
            cmd.Parameters.AddWithValue("@BatchId", BatchId);
            cmd.Parameters.AddWithValue("@SemesterId", SemesterId);
            cmd.Parameters.AddWithValue("@ClassYearId", ClassYearId);
            cmd.CommandText = "usp_admin_GetFeeSummaryClassWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.ClassWiseFeeSummary beData = new API.Admin.ClassWiseFeeSummary();
                    if (!(reader[0] is DBNull)) beData.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.NoOfStudent = reader.GetInt32(2);                    
                    if (!(reader[3] is DBNull)) beData.PreviousDues = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.CurrentDues = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.PaidAmount = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.BalanceAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.SectionId = Convert.ToInt32(reader[8]);
                    if (!(reader[9] is DBNull)) beData.ClassYearId = Convert.ToInt32(reader[9]);
                    if (!(reader[10] is DBNull)) beData.SemesterId = Convert.ToInt32(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Section = Convert.ToString(reader[11]);
                    if (!(reader[12] is DBNull)) beData.ClassYear = Convert.ToString(reader[12]);
                    if (!(reader[13] is DBNull)) beData.Semester = Convert.ToString(reader[13]);
                    if (!(reader[14] is DBNull)) beData.C_SNo = Convert.ToInt32(reader[14]);
                    if (!(reader[15] is DBNull)) beData.R_SNo = Convert.ToInt32(reader[15]);

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

        public AcademicLib.API.Admin.FeeHeadingWiseFeeSummaryCollections admin_FeeHeadingWiseFeeSummary(int UserId, int uptoMonthId, int? classId,int? sectionId,int? AcademicYearId)
        {
            AcademicLib.API.Admin.FeeHeadingWiseFeeSummaryCollections dataColl = new API.Admin.FeeHeadingWiseFeeSummaryCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@UpToMonthId", uptoMonthId);
            cmd.Parameters.AddWithValue("@ClassId", classId);
            cmd.Parameters.AddWithValue("@SectionId", sectionId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_admin_FeeItemWiseDuesList";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.API.Admin.FeeHeadingWiseFeeSummary beData = new API.Admin.FeeHeadingWiseFeeSummary();
                    if (!(reader[0] is DBNull)) beData.FeeItemName = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Debit = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) beData.Credit = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) beData.Dues = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) beData.DrDiscountAmt = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.CrDiscountAmt = Convert.ToDouble(reader[5]);
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

        public AcademicLib.BE.Fee.Transaction.FeeReceipt_SENT getFeeForSMS(int UserId,int AcademicYearId, int TranId)
        {
            AcademicLib.BE.Fee.Transaction.FeeReceipt_SENT beData = new AcademicLib.BE.Fee.Transaction.FeeReceipt_SENT();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeReceiptForSMS";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Fee.Transaction.FeeReceipt_SENT();                   
                    if (!(reader[0] is DBNull)) beData.ReceiptNo = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.ReceiptDate = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ReceiptMiti = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.Narration = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RefNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.StudentName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.RegdNo = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.ClassName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.SectionName = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.RollNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.FatherName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ContactNo = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ReceiptAmt = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.DiscountAmt = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.AfterReceivedDues = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.FeeItemDetails = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.PaidUpToMonth = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.UserId = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.EmailId = reader.GetString(18);
                }
              
                reader.Close();
               
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                dal.CloseConnection();
            }
            return beData;
        }


        public AcademicLib.RE.Fee.ClassAndFeeItemWiseCollections getFeeSummaryClassAndFeeItemWise(int UserId, int AcademicYearId, int FromMonthId,int ToMonthId,int ForStudent)
        {
            AcademicLib.RE.Fee.ClassAndFeeItemWiseCollections dataColl = new RE.Fee.ClassAndFeeItemWiseCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;            
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@FromMonthId", FromMonthId);
            cmd.Parameters.AddWithValue("@ToMonthId", ToMonthId);
            cmd.Parameters.AddWithValue("@ForStudent", ForStudent);
            cmd.CommandText = "usp_GetFeeSummaryClassAndFeeWise";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.RE.Fee.ClassAndFeeItemWise beData = new RE.Fee.ClassAndFeeItemWise();                    
                    if (!(reader[0] is DBNull)) beData.ClassOrderNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.ClassName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.SectionName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.FeeOrderNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.FeeItemName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Opening = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) beData.DrAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.DrDiscountAmt = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.DrTax = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.DrFineAmt = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.DrTotal = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.CrDiscountAmt = Convert.ToDouble(reader[11]);
                    if (!(reader[12] is DBNull)) beData.CrAmt = Convert.ToDouble(reader[12]);
                    if (!(reader[13] is DBNull)) beData.CrFineAmt = Convert.ToDouble(reader[13]);
                    if (!(reader[14] is DBNull)) beData.CrTotal = Convert.ToDouble(reader[14]);
                    beData.BalanceAmt = beData.DrTotal - beData.CrTotal;

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

        public ResponeValues ImportFeeReceipt(int UserId, List<BE.Fee.Transaction.ImportFeeReceipt> dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            dal.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 800);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;

            if (dataColl != null)
            {
                System.Data.DataTable table = new System.Data.DataTable();

                table.Columns.Add("FiscalYear", typeof(string));
                table.Columns.Add("Branch", typeof(string));
                table.Columns.Add("ReceiptNo", typeof(int));
                table.Columns.Add("AcademicYear", typeof(string));
                table.Columns.Add("MonthId", typeof(int)); 
                table.Columns.Add("RegdNo", typeof(string));
                table.Columns.Add("ReceiptDate_AD", typeof(DateTime));
                table.Columns.Add("ReceiptDate_BS", typeof(string));
                table.Columns.Add("ReceiptAs", typeof(string));
                table.Columns.Add("FeeItem", typeof(string));
                table.Columns.Add("Amount", typeof(double));
                table.Columns.Add("DisPer", typeof(double));
                table.Columns.Add("DisAmt", typeof(double));
                table.Columns.Add("TaxAmt", typeof(double));
                table.Columns.Add("FineAmt", typeof(double));
                table.Columns.Add("TotalAmt", typeof(double));
                table.Columns.Add("Remarks", typeof(string));

                table.Columns.Add("StudentName", typeof(string));
                table.Columns.Add("Address", typeof(string));
                table.Columns.Add("RefNo", typeof(string));
                table.Columns.Add("ClassName", typeof(string));
                table.Columns.Add("ReceiptBy", typeof(string));
                foreach (var v in dataColl)
                {
                    var row = table.NewRow();
                    row["FiscalYear"] =IsDBNull(v.FiscalYear);
                    row["Branch"] = IsDBNull(v.Branch);
                    row["ReceiptNo"] = IsDBNull(v.ReceiptNo);
                    row["AcademicYear"] = IsDBNull(v.AcademicYear);
                    row["MonthId"] = IsDBNull(v.MonthId);
                    row["RegdNo"] = IsDBNull(v.RegdNo);
                    row["ReceiptDate_AD"] = IsDBNull(v.ReceiptDate_AD);
                    row["ReceiptDate_BS"] = IsDBNull(v.ReceiptDate_BS);
                    row["ReceiptAs"] = IsDBNull(v.ReceiptAs);
                    row["FeeItem"] = IsDBNull(v.FeeItem);
                    row["Amount"] = ZeroTo(v.Amount);
                    row["DisPer"] = ZeroTo(v.DisPer);
                    row["DisAmt"] = ZeroTo(v.DisAmt);
                    row["TaxAmt"] = ZeroTo(v.TaxAmt);
                    row["FineAmt"] = ZeroTo(v.FineAmt);
                    row["TotalAmt"] = ZeroTo(v.TotalAmt);
                    row["Remarks"] =IsDBNull(v.Remarks);

                    row["StudentName"] = IsDBNull(v.StudentName);
                    row["Address"] = IsDBNull(v.Address);
                    row["RefNo"] = IsDBNull(v.RefNo);
                    row["ClassName"] = IsDBNull(v.ClassName);
                    row["ReceiptBy"] = IsDBNull(v.ReceiptBy);
                    table.Rows.Add(row);
                }
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@RecColl", table);
                sqlParam.SqlDbType = SqlDbType.Structured;

            }

            cmd.CommandText = "usp_ImportFeeReceipt";
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (resVal.IsSuccess)
                    dal.CommitTransaction();
                else
                    dal.RollbackTransaction();  

                //resVal.IsSuccess = true;
                //resVal.ResponseMSG = "Bill Generate Success";
            }
            catch (System.Data.SqlClient.SqlException ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            catch (Exception ee)
            {
                dal.RollbackTransaction();
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return resVal;
        }

        public ResponeValues UpdateFeeReceiptRef(int UserId,double Amount,string RefNo)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@RefNo", RefNo);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_CheckFeeReceiptStatus";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);            
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;            

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[4].Value);  
                
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
    }
}
