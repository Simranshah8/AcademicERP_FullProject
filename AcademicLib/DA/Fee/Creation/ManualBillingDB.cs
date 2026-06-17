using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;
namespace AcademicLib.DA.Fee.Creation
{
    internal class ManualBillingDB
    {
        DataAccessLayer1 dal = null;
        public ManualBillingDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(int AcademicYearId,BE.Fee.Creation.ManualBilling beData, bool isModify)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AutoNumber", beData.AutoNumber);
            cmd.Parameters.AddWithValue("@BillingDate", beData.BillingDate);
            cmd.Parameters.AddWithValue("@StudentId", beData.StudentId);
            cmd.Parameters.AddWithValue("@TotalAmount", beData.TotalAmount);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);

            if (isModify)
            {
                cmd.CommandText = "usp_UpdateManualBilling";
            }
            else
            {
                cmd.Parameters[6].Direction = System.Data.ParameterDirection.Output;
                cmd.CommandText = "usp_AddManualBilling";
            }
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[7].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[8].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[9].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
            cmd.Parameters.AddWithValue("@BillingType", beData.BillingType);
            cmd.Parameters.AddWithValue("@ForMonthId", beData.ForMonthId);
            cmd.Parameters.AddWithValue("@IsCash", beData.IsCash);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);

            cmd.Parameters.AddWithValue("@StudentName", beData.StudentName);
            cmd.Parameters.AddWithValue("@ClassName", beData.ClassName);
            cmd.Parameters.AddWithValue("@Address", beData.Address);
            cmd.Parameters.AddWithValue("@AdmissionEnquiryId", beData.AdmissionEnquiryId);
            cmd.Parameters.AddWithValue("@RegistrationId", beData.RegistrationId);
            cmd.Parameters.AddWithValue("@RefNo", beData.RefNo);
            cmd.Parameters.AddWithValue("@Waiver", beData.Waiver);
            cmd.Parameters.AddWithValue("@PaidAmt", beData.PaidAmt);
            cmd.Parameters.AddWithValue("@DuesAmt", beData.DuesAmt);
            cmd.Parameters.AddWithValue("@LedgerId", beData.LedgerId);

            cmd.Parameters.AddWithValue("@ClassYearId", beData.ClassYearId);
            cmd.Parameters.AddWithValue("@SemesterId", beData.SemesterId);
            cmd.Parameters.AddWithValue("@IsRateAsFeeMapping", beData.IsRateAsFeeMapping);

            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[6].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[6].Value);

                if (!(cmd.Parameters[7].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[7].Value);

                if (!(cmd.Parameters[8].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[8].Value);

                if (!(cmd.Parameters[9].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[9].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";


                if (resVal.RId > 0 && resVal.IsSuccess)
                {
                    SaveManualBillingDetailss(beData.CUserId, resVal.RId, beData.ManualBillingDetailsColl);
                    // SaveManualBillingToBillGenerate(beData.CUserId, resVal.RId);
                    var invRes=SaveManualBillingToSalesInvoice(beData.CUserId, resVal.RId);
                    if(!invRes.IsSuccess)
                    {
                        resVal.ResponseMSG = invRes.ResponseMSG;
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

        private void SaveManualBillingToBillGenerate(int UserId, int TranId)
        {
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TranId", TranId);            
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_ManualBillingToBillGenerate";
            cmd.ExecuteNonQuery();

        }
        private ResponeValues SaveManualBillingToSalesInvoice(int UserId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BillTranId", TranId);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_GenerateSalesInvoiceFromManualBillGenerate";
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
        private void SaveManualBillingDetailss(int UserId, int TranId, List<BE.Fee.Creation.ManualBillingDetails> beDataColl)
        {
            if (beDataColl == null || beDataColl.Count == 0 || TranId == 0)
                return;

            foreach (BE.Fee.Creation.ManualBillingDetails beData in beDataColl)
            {

                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TranId", TranId);
                cmd.Parameters.AddWithValue("@SNo", beData.SNo);
                cmd.Parameters.AddWithValue("@FeeItemId", beData.FeeItemId);
                cmd.Parameters.AddWithValue("@Qty", beData.Qty);
                cmd.Parameters.AddWithValue("@Rate", beData.Rate);
                cmd.Parameters.AddWithValue("@DiscountPer", beData.DiscountPer);
                cmd.Parameters.AddWithValue("@DiscountAmt", beData.DiscountAmt);
                cmd.Parameters.AddWithValue("@PayableAmt", beData.PayableAmt);
                cmd.Parameters.AddWithValue("@Remarks", beData.Remarks);
                cmd.Parameters.AddWithValue("@Waiver", beData.Waiver);
                cmd.Parameters.AddWithValue("@PaidAmt", beData.PaidAmt);
                cmd.Parameters.AddWithValue("@DuesAmt", beData.DuesAmt);
                cmd.Parameters.AddWithValue("@TaxRate", beData.TaxRate);
                cmd.Parameters.AddWithValue("@TaxAmt", beData.TaxAmt);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_AddManualBillingDetails";
                cmd.ExecuteNonQuery();
            }

        }


        public AcademicLib.BE.Fee.Creation.ManualBillingCollections getAllManualBilling(int UserId, int AcademicYearId, int EntityId)
        {
            AcademicLib.BE.Fee.Creation.ManualBillingCollections dataColl = new AcademicLib.BE.Fee.Creation.ManualBillingCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetAllManualBilling";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Creation.ManualBilling beData = new AcademicLib.BE.Fee.Creation.ManualBilling();

                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BillingDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.StudentId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.TotalAmount = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) beData.ClassName = Convert.ToString(reader[5]);
                    if (!(reader[6] is DBNull)) beData.SectionName = Convert.ToString(reader[6]);
                    if (!(reader[7] is DBNull)) beData.RollNo = Convert.ToInt32(reader[7]);
                    if (!(reader[8] is DBNull)) beData.StudentName = Convert.ToString(reader[8]);
                    if (!(reader[9] is DBNull)) beData.BillingDate_BS = Convert.ToString(reader[9]);
                    if (!(reader[10] is DBNull)) beData.RegdNo = Convert.ToString(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Remarks = Convert.ToString(reader[11]);
                    if (!(reader[12] is DBNull)) beData.BillingType =(AcademicLib.BE.Fee.Creation.BILLINGTYPES)reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.ForMonthId = reader.GetInt32(13);
                    if (!(reader[14] is DBNull)) beData.ForMonthName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.IsCancel = reader.GetBoolean(15);
                    if (!(reader[16] is DBNull)) beData.CancelRemarks = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.IsCash = reader.GetBoolean(17);
                    if (!(reader[18] is DBNull)) beData.RefNo = reader.GetString(18);
                    if (!(reader[19] is DBNull)) beData.Waiver = Convert.ToDouble(reader[19]);
                    if (!(reader[20] is DBNull)) beData.PaidAmt = Convert.ToDouble(reader[20]);
                    ///if (!(reader[21] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[22] is DBNull)) beData.LedgerName = reader.GetString(22);

                    try
                    {
                        if (!(reader[23] is DBNull)) beData.CancelBy = reader.GetString(23);
                        if (!(reader[24] is DBNull)) beData.CancelRemarks = reader.GetString(24);
                        if (!(reader[25] is DBNull)) beData.CancelDateTime = reader.GetDateTime(25);
                        if (!(reader[26] is DBNull)) beData.CancelMiti = reader.GetString(26);
                        if (!(reader[27] is DBNull)) beData.InvoiceNo = Convert.ToString(reader[27]);
                        if (!(reader[28] is DBNull)) beData.Batch = reader.GetString(28);
                        if (!(reader[29] is DBNull)) beData.ClassYear = reader.GetString(29);
                        if (!(reader[30] is DBNull)) beData.Semester = reader.GetString(30);
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
        public AcademicLib.BE.Fee.Creation.ManualBilling getManualBillingById(int UserId, int EntityId, int TranId)
        {
            AcademicLib.BE.Fee.Creation.ManualBilling beData = new AcademicLib.BE.Fee.Creation.ManualBilling();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetManualBillingById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Fee.Creation.ManualBilling();

                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BillingDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.StudentId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.TotalAmount = reader.GetInt32(4);

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

        public AcademicLib.BE.Fee.Creation.ManualBilling getManualBillingDetailsById(int UserId, int TranId)
        {
            AcademicLib.BE.Fee.Creation.ManualBilling beData = new AcademicLib.BE.Fee.Creation.ManualBilling();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.Parameters.AddWithValue("@UserId", UserId);            
            cmd.CommandText = "usp_GetManualBillingDetailsById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Fee.Creation.ManualBilling();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.StudentName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.RollNo = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.RegdNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.ClassName = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.SectionName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.BillingDate = reader.GetDateTime(7);
                    if (!(reader[8] is DBNull)) beData.BillingDate_BS = reader.GetString(8);
                    if (!(reader[9] is DBNull)) beData.IsLeft = reader.GetBoolean(9);
                    if (!(reader[10] is DBNull)) beData.BillingTypeName = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.ForMonthName = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.Remarks = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.RefBillNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.TotalAmount = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.IsCash = Convert.ToBoolean(reader[15]);
                    if (!(reader[16] is DBNull)) beData.LedgerName = Convert.ToString(reader[16]);
                    if (!(reader[17] is DBNull)) beData.IsRateAsFeeMapping = Convert.ToBoolean(reader[17]);

                }
                beData.ManualBillingDetailsColl = new BE.Fee.Creation.ManualBillingDetailsCollections();
                reader.NextResult();
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Creation.ManualBillingDetails det = new BE.Fee.Creation.ManualBillingDetails();
                    if (!(reader[0] is DBNull)) det.SNo = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.FeeItemName = reader.GetString(1);
                    if (!(reader[2] is DBNull)) det.Qty = Convert.ToDouble(reader[2]);
                    if (!(reader[3] is DBNull)) det.Rate = Convert.ToDouble(reader[3]);
                    if (!(reader[4] is DBNull)) det.DiscountPer = Convert.ToDouble(reader[4]);
                    if (!(reader[5] is DBNull)) det.DiscountAmt = Convert.ToDouble(reader[5]);
                    if (!(reader[6] is DBNull)) det.PayableAmt = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) det.Remarks = Convert.ToString(reader[7]);
                    if (!(reader[8] is DBNull)) det.TaxRate = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) det.TaxAmt = Convert.ToDouble(reader[9]);
                    beData.ManualBillingDetailsColl.Add(det);

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

        public AcademicLib.RE.Fee.ManualBillingCollections getManualBillingDetails(int UserId,int AcademicYearId, DateTime? dateFrom,DateTime? dateTo,bool? IsCancel)
        {
            AcademicLib.RE.Fee.ManualBillingCollections dataColl = new RE.Fee.ManualBillingCollections();

            if (!IsCancel.HasValue)
                IsCancel = false;

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@IsCancel", IsCancel);
            cmd.CommandText = "usp_GetBillingDetails";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.ManualBilling beData = new RE.Fee.ManualBilling();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BillDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.BillMiti = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.MonthName = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.FeeItem = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.Qty = Convert.ToDouble(reader[6]);
                    if (!(reader[7] is DBNull)) beData.Rate = Convert.ToDouble(reader[7]);
                    if (!(reader[8] is DBNull)) beData.DiscountPer = Convert.ToDouble(reader[8]);
                    if (!(reader[9] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[9]);
                    if (!(reader[10] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[10]);
                    if (!(reader[11] is DBNull)) beData.Remarks = reader.GetString(11);

                    if (!(reader[12] is DBNull)) beData.StudentId = reader.GetInt32(12);
                    if (!(reader[13] is DBNull)) beData.RegdNo = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.ClassName = reader.GetString(14);
                    if (!(reader[15] is DBNull)) beData.SectionName = reader.GetString(15);
                    if (!(reader[16] is DBNull)) beData.RollNo = reader.GetInt32(16);
                    if (!(reader[17] is DBNull)) beData.Name = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.BillingType = reader.GetString(18);

                    if (!(reader[19] is DBNull)) beData.IsCancel = reader.GetBoolean(19);
                    if (!(reader[20] is DBNull)) beData.Batch = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.Semester = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.ClassYear = reader.GetString(22);

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

        public AcademicLib.RE.Fee.ManualBillingDetailsCollections getBillingDetails(int UserId, DateTime? dateFrom, DateTime? dateTo)
        {
            AcademicLib.RE.Fee.ManualBillingDetailsCollections dataColl = new RE.Fee.ManualBillingDetailsCollections();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
            cmd.Parameters.AddWithValue("@DateTo", dateTo);
            cmd.Parameters.AddWithValue("@UserId", UserId); 
            cmd.CommandText = "usp_GetManualBillingDetails";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RE.Fee.ManualBillingDetails beData = new RE.Fee.ManualBillingDetails();
                    if (!(reader[0] is DBNull)) beData.TranId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.AutoNumber = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BillingDate = reader.GetDateTime(2);
                    if (!(reader[3] is DBNull)) beData.BillingMiti = reader.GetString(3);
                    if (!(reader[4] is DBNull)) beData.RegNo = reader.GetString(4);
                    if (!(reader[5] is DBNull)) beData.Name = reader.GetString(5);
                    if (!(reader[6] is DBNull)) beData.ClassName = reader.GetString(6);
                    if (!(reader[7] is DBNull)) beData.SectionName = reader.GetString(7);
                    if (!(reader[8] is DBNull)) beData.RollNo = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.ContactNo = reader.GetString(9);
                    if (!(reader[10] is DBNull)) beData.Email = reader.GetString(10);
                    if (!(reader[11] is DBNull)) beData.FeeItem = reader.GetString(11);
                    if (!(reader[12] is DBNull)) beData.ProductName = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.ProductCode = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.Qty = Convert.ToDouble(reader[14]);
                    if (!(reader[15] is DBNull)) beData.Rate = Convert.ToDouble(reader[15]);
                    if (!(reader[16] is DBNull)) beData.DiscountAmt = Convert.ToDouble(reader[16]);
                    if (!(reader[17] is DBNull)) beData.PayableAmt = Convert.ToDouble(reader[17]);
                    if (!(reader[18] is DBNull)) beData.PaidAmt = Convert.ToDouble(reader[18]);
                    if (!(reader[19] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[19]);
                    if (!(reader[20] is DBNull)) beData.Remarks = reader.GetString(20);
                    if (!(reader[21] is DBNull)) beData.RefNo = reader.GetString(21);
                    if (!(reader[22] is DBNull)) beData.AcademicYear = reader.GetString(22);
                    if (!(reader[23] is DBNull)) beData.UserName = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.LogDateTime = reader.GetDateTime(24);
                    if (!(reader[25] is DBNull)) beData.LogMiti = reader.GetString(25);
                    if (!(reader[26] is DBNull)) beData.ForMonth = reader.GetString(26);
                    if (!(reader[27] is DBNull)) beData.BillingType = reader.GetString(27);

                    if (!(reader[28] is DBNull)) beData.IsCancel = reader.GetBoolean(28);
                    if (!(reader[29] is DBNull)) beData.Batch = reader.GetString(29);
                    if (!(reader[30] is DBNull)) beData.Semester = reader.GetString(30);
                    if (!(reader[31] is DBNull)) beData.ClassYear = reader.GetString(31);

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

        public ResponeValues getAutoNo(int UserId,int AcademicYearId,int? CostClassId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.Add("@AutoNumber", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.CommandText = "usp_GetAutoNumberOfManualBilling";
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@CostClassId", CostClassId);
            try
            {
                cmd.ExecuteNonQuery();
                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.RId = Convert.ToInt32(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[3].Value);

                if (!(cmd.Parameters[4].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[4].Value);

                if (!resVal.IsSuccess && resVal.ErrorNumber > 0)
                    resVal.ResponseMSG = resVal.ResponseMSG + " (" + resVal.ErrorNumber.ToString() + ")";
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
        public ResponeValues DeleteById(int UserId, int EntityId, int TranId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@TranId", TranId);
            cmd.CommandText = "usp_DelManualBillingById";
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
        public ResponeValues Cancel(BE.Fee.Creation.ManualBilling beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@Remarks", beData.CancelRemarks);
            cmd.Parameters.AddWithValue("@TranId", beData.TranId);
            cmd.CommandText = "usp_CancelManualBilling";
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
        public ResponeValues getFeeRate(int UserId, int AcademicYearId, int StudentId,int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@FeeItemId", FeeItemId);
            cmd.CommandText = "usp_GetFeeRateForManualBilling";
            cmd.Parameters.Add("@Rate", System.Data.SqlDbType.Float);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseId = Convert.ToString(cmd.Parameters[3].Value);

                resVal.IsSuccess = true;
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
        public AcademicLib.BE.Fee.Creation.RegAutoManualNoData getDataFromRegAutoManualNo(string RegNo, string AutoManualNo, int UserId, int EntityId)
        {
            AcademicLib.BE.Fee.Creation.RegAutoManualNoData beData = new AcademicLib.BE.Fee.Creation.RegAutoManualNoData();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegNo", RegNo);
            cmd.Parameters.AddWithValue("@AutoManualNo", AutoManualNo);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.CommandText = "usp_GetDataFromRegAutoManualNo";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Fee.Creation.RegAutoManualNoData();
                    if (!(reader[0] is DBNull)) beData.Name = reader.GetString(0);
                    if (!(reader[1] is DBNull)) beData.Address = reader.GetString(1);
                    if (!(reader[2] is DBNull)) beData.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) beData.ClassId = reader.GetInt32(3);
                    if (RegNo.Length > 0)
                    {
                        if (!(reader[4] is DBNull)) beData.RegistrationId = reader.GetInt32(4);
                    }
                    else if (AutoManualNo.Length > 0)
                    {
                        if (!(reader[4] is DBNull)) beData.AdmissionEnquiryId = reader.GetInt32(4);
                    }
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

    }
}
