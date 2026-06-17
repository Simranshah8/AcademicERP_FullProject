using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.DA.Fee.Setup
{
    internal class FeeConfigurationDB
    {
        DataAccessLayer1 dal = null;
        public FeeConfigurationDB(string hostName, string dbName)
        {
            dal = new DataAccessLayer1(hostName, dbName);
        }
        public ResponeValues SaveUpdate(BE.Fee.Setup.FeeConfiguration beData)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TransportFeeItemId", beData.TransportFeeItemId);
            cmd.Parameters.AddWithValue("@HostelFeeItemId", beData.HostelFeeItemId);
            cmd.Parameters.AddWithValue("@LibraryFeeItemId", beData.LibraryFeeItemId);
            cmd.Parameters.AddWithValue("@CanteenFeeItemId", beData.CanteenFeeItemId);
            cmd.Parameters.AddWithValue("@FineFeeItemId", beData.FineFeeItemId);
            cmd.Parameters.AddWithValue("@TaxFeeItemId", beData.TaxFeeItemId);
            cmd.Parameters.AddWithValue("@FixedStudentFeeItemId", beData.FixedStudentFeeItemId);
            cmd.Parameters.AddWithValue("@FeeReceiptLedgerId", beData.FeeReceiptLedgerId);
            cmd.Parameters.AddWithValue("@DiscountLedgerId", beData.DiscountLedgerId);
            cmd.Parameters.AddWithValue("@TaxLedgerId", beData.TaxLedgerId);
            cmd.Parameters.AddWithValue("@FineLedgerId", beData.FineLedgerId);
            cmd.Parameters.AddWithValue("@FixedStudentLedgerId", beData.FixedStudentLedgerId);
            cmd.Parameters.AddWithValue("@FineHeading", beData.FineHeading);
            cmd.Parameters.AddWithValue("@TaxHeading", beData.TaxHeading);
            cmd.Parameters.AddWithValue("@NumberingMethod", beData.NumberingMethod);
            cmd.Parameters.AddWithValue("@NumericalPartWidth", beData.NumericalPartWidth);
            cmd.Parameters.AddWithValue("@Prefix", beData.Prefix);
            cmd.Parameters.AddWithValue("@Suffix", beData.Suffix);
            cmd.Parameters.AddWithValue("@StartNumber", beData.StartNumber);
            cmd.Parameters.AddWithValue("@DateStyle", beData.DateStyle);
            cmd.Parameters.AddWithValue("@DateFormat", beData.DateFormat);
            cmd.Parameters.AddWithValue("@NoOfDecimal", beData.NoOfDecimal);
            cmd.Parameters.AddWithValue("@FeeMapping", beData.FeeMapping);
            cmd.Parameters.AddWithValue("@Notes", beData.Notes);
            cmd.Parameters.AddWithValue("@SendAutoSMS", beData.SendAutoSMS);            
            cmd.Parameters.AddWithValue("@UserId", beData.CUserId);
            cmd.Parameters.AddWithValue("@EntityId", beData.EntityId);            
            cmd.CommandText = "usp_AddFeeConfiguration";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[27].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[28].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[29].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@SalesPartyLedgerId", beData.SalesPartyLedgerId);
            cmd.Parameters.AddWithValue("@IRDEnabled", beData.IRDEnabled);
            cmd.Parameters.AddWithValue("@ReceiptMonthAs", beData.ReceiptMonthAs);
            cmd.Parameters.AddWithValue("@ReceiptDateValidateInBillPrint", beData.ReceiptDateValidateInBillPrint);
            cmd.Parameters.AddWithValue("@ActiveMemoBilling", beData.ActiveMemoBilling);
            cmd.Parameters.AddWithValue("@ShowRate", beData.ShowRate);
            cmd.Parameters.AddWithValue("@ShowOpeningHeadingWise", beData.ShowOpeningHeadingWise);
            cmd.Parameters.AddWithValue("@AllowDiscount", beData.AllowDiscount);
            cmd.Parameters.AddWithValue("@AllowFine", beData.AllowFine);
            cmd.Parameters.AddWithValue("@AdmitDateEffectInBillGenerate", beData.AdmitDateEffectInBillGenerate);

            cmd.Parameters.AddWithValue("@SiblingBillPrint", beData.SiblingBillPrint);
            cmd.Parameters.AddWithValue("@SiblingStudentLedger", beData.SiblingStudentLedger);
            cmd.Parameters.AddWithValue("@SiblingStudentVoucher", beData.SiblingStudentVoucher);
            cmd.Parameters.AddWithValue("@SiblingFeeReminder", beData.SiblingFeeReminder);

            cmd.Parameters.AddWithValue("@BillGenerate_VoucherId", beData.BillGenerate_VoucherId);
            cmd.Parameters.AddWithValue("@FeeReceipt_VoucherId", beData.FeeReceipt_VoucherId);
            cmd.Parameters.AddWithValue("@DebitNote_VoucherId", beData.DebitNote_VoucherId);
            cmd.Parameters.AddWithValue("@CreditNote_VoucherId", beData.CreditNote_VoucherId);
            cmd.Parameters.AddWithValue("@AdvanceFeeItemId", beData.AdvanceFeeItemId);
            cmd.Parameters.AddWithValue("@AllowDiscountInRegistration", beData.AllowDiscountInRegistration);
            cmd.Parameters.AddWithValue("@AllowDiscountInEnquiry", beData.AllowDiscountInEnquiry);
            cmd.Parameters.AddWithValue("@MonthWiseFeeHeading", beData.MonthWiseFeeHeading);
            cmd.Parameters.AddWithValue("@ShowLeftStudentInDiscountSetup", beData.ShowLeftStudentInDiscountSetup);
            cmd.Parameters.AddWithValue("@ShowOnlyGenerateBillInReceipt", beData.ShowOnlyGenerateBillInReceipt);
            cmd.Parameters.AddWithValue("@ShowDuesFeeHeadingInReceipt", beData.ShowDuesFeeHeadingInReceipt);
            cmd.Parameters.AddWithValue("@AllowMultiplePaymentmode", beData.AllowMultiplePaymentmode);
            cmd.Parameters.AddWithValue("@OpeningFeeMonth", beData.OpeningFeeMonth);
            cmd.Parameters.AddWithValue("@AllowMonthWiseOnlinePayment", beData.AllowMonthWiseOnlinePayment);
            cmd.Parameters.AddWithValue("@AllowValidateTotalDues", beData.AllowValidateTotalDues);
            cmd.Parameters.AddWithValue("@MonthNameAsBillDate_MB", beData.MonthNameAsBillDate_MB);
            //Added
            cmd.Parameters.AddWithValue("@InclBillDisSL", beData.InclBillDisSL);
            try
            {
                cmd.ExecuteNonQuery();

                SaveDefaulterDues(beData.CUserId, beData.DefaulterDuesColl);
                
                if (!(cmd.Parameters[27].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[27].Value);

                if (!(cmd.Parameters[28].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[28].Value);

                if (!(cmd.Parameters[29].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[29].Value);

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

        public void SaveDefaulterDues(int UserId, List<BE.Fee.Setup.FeeDefaulterMinDues> dataColl)
        {
            if (dataColl == null)
                return;

            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (var beData in dataColl)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ClassId", beData.ClassId);
                cmd.Parameters.AddWithValue("@DuesAmt", beData.DuesAmt);
                cmd.Parameters.AddWithValue("@ReceiptTemplateId", beData.ReceiptTemplateId);
                cmd.Parameters.AddWithValue("@BillTemplateId", beData.BillTemplateId);
                cmd.Parameters.AddWithValue("@CreditDays", beData.CreditDays);
                cmd.Parameters.AddWithValue("@BillGenerateOn", beData.BillGenerateOn);
                cmd.Parameters.AddWithValue("@BillGenerateDay", beData.BillGenerateDay);
                cmd.CommandText = "usp_AddFeeDefaulterMinDues";
                cmd.ExecuteNonQuery();
            }
        }

        public AcademicLib.BE.Fee.Setup.FeeDefaulterMinDues getRecTemplateTranId(int UserId, int EntityId,int? AcademicYearId)
        {
            AcademicLib.BE.Fee.Setup.FeeDefaulterMinDues beData = new AcademicLib.BE.Fee.Setup.FeeDefaulterMinDues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetReceiptTemplateForApp";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Fee.Setup.FeeDefaulterMinDues();
                    if (!(reader[0] is DBNull)) beData.DuesAmt = Convert.ToDouble(reader[0]);
                    if (!(reader[1] is DBNull)) beData.ReceiptTemplateId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.BillTemplateId = reader.GetInt32(2);
                     
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

        public AcademicLib.BE.Fee.Setup.FeeConfiguration getFeeConfigurationById(int UserId, int EntityId,int? AcademicYearId)
        {
            AcademicLib.BE.Fee.Setup.FeeConfiguration beData = new AcademicLib.BE.Fee.Setup.FeeConfiguration();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.CommandText = "usp_GetFeeConfigurationById";
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    beData = new AcademicLib.BE.Fee.Setup.FeeConfiguration();
                    if (!(reader[0] is DBNull)) beData.TransportFeeItemId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) beData.HostelFeeItemId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) beData.LibraryFeeItemId = reader.GetInt32(2);
                    if (!(reader[3] is DBNull)) beData.CanteenFeeItemId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) beData.FineFeeItemId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) beData.TaxFeeItemId = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) beData.FixedStudentFeeItemId = reader.GetInt32(6);
                    if (!(reader[7] is DBNull)) beData.FeeReceiptLedgerId = reader.GetInt32(7);
                    if (!(reader[8] is DBNull)) beData.DiscountLedgerId = reader.GetInt32(8);
                    if (!(reader[9] is DBNull)) beData.TaxLedgerId = reader.GetInt32(9);
                    if (!(reader[10] is DBNull)) beData.FineLedgerId = reader.GetInt32(10);
                    if (!(reader[11] is DBNull)) beData.FixedStudentLedgerId = reader.GetInt32(11);
                    if (!(reader[12] is DBNull)) beData.FineHeading = reader.GetString(12);
                    if (!(reader[13] is DBNull)) beData.TaxHeading = reader.GetString(13);
                    if (!(reader[14] is DBNull)) beData.NumberingMethod = reader.GetInt32(14);
                    if (!(reader[15] is DBNull)) beData.NumericalPartWidth = reader.GetInt32(15);
                    if (!(reader[16] is DBNull)) beData.Prefix = reader.GetString(16);
                    if (!(reader[17] is DBNull)) beData.Suffix = reader.GetString(17);
                    if (!(reader[18] is DBNull)) beData.StartNumber = reader.GetInt32(18);
                    if (!(reader[19] is DBNull)) beData.DateStyle = reader.GetInt32(19);
                    if (!(reader[20] is DBNull)) beData.DateFormat = reader.GetInt32(20);
                    if (!(reader[21] is DBNull)) beData.NoOfDecimal = reader.GetInt32(21);
                    if (!(reader[22] is DBNull)) beData.FeeMapping = reader.GetInt32(22);
                    if (!(reader[23] is DBNull)) beData.Notes = reader.GetString(23);
                    if (!(reader[24] is DBNull)) beData.SendAutoSMS = reader.GetBoolean(24);
                    if (!(reader[25] is DBNull)) beData.SalesPartyLedgerId = reader.GetInt32(25);
                    if (!(reader[26] is DBNull)) beData.IRDEnabled = reader.GetBoolean(26);
                    if (!(reader[27] is DBNull)) beData.ReceiptMonthAs = reader.GetInt32(27);
                    if (!(reader[28] is DBNull)) beData.ReceiptDateValidateInBillPrint = reader.GetBoolean(28);
                    if (!(reader[29] is DBNull)) beData.ActiveMemoBilling = reader.GetBoolean(29);
                    if (!(reader[30] is DBNull)) beData.ShowRate = reader.GetBoolean(30);
                    if (!(reader[31] is DBNull)) beData.ShowOpeningHeadingWise = reader.GetBoolean(31);
                    if (!(reader[32] is DBNull)) beData.AllowDiscount = reader.GetBoolean(32);
                    if (!(reader[33] is DBNull)) beData.AllowFine = reader.GetBoolean(33);
                    if (!(reader[34] is DBNull)) beData.AdmitDateEffectInBillGenerate = reader.GetBoolean(34);
                    if (!(reader[35] is DBNull)) beData.SiblingBillPrint = reader.GetBoolean(35);
                    if (!(reader[36] is DBNull)) beData.SiblingStudentLedger = reader.GetBoolean(36);
                    if (!(reader[37] is DBNull)) beData.SiblingStudentVoucher = reader.GetBoolean(37);
                    if (!(reader[38] is DBNull)) beData.SiblingFeeReminder = reader.GetBoolean(38);
                    if (!(reader[39] is DBNull)) beData.BillGenerate_VoucherId = reader.GetInt32(39);
                    if (!(reader[40] is DBNull)) beData.FeeReceipt_VoucherId = reader.GetInt32(40);
                    if (!(reader[41] is DBNull)) beData.DebitNote_VoucherId = reader.GetInt32(41);
                    if (!(reader[42] is DBNull)) beData.CreditNote_VoucherId = reader.GetInt32(42);
                    if (!(reader[43] is DBNull)) beData.AdvanceFeeItemId = reader.GetInt32(43);
                    if (!(reader[44] is DBNull)) beData.AllowDiscountInRegistration = reader.GetBoolean(44);
                    if (!(reader[45] is DBNull)) beData.AllowDiscountInEnquiry = reader.GetBoolean(45);
                    if (!(reader[46] is DBNull)) beData.MonthWiseFeeHeading = reader.GetBoolean(46);
                    if (!(reader[47] is DBNull)) beData.ShowLeftStudentInDiscountSetup = reader.GetBoolean(47);
                    if (!(reader[48] is DBNull)) beData.JVPending = Convert.ToInt32(reader[48]);
                    if (!(reader[49] is DBNull)) beData.SIPending = Convert.ToInt32(reader[49]);
                    if (!(reader[50] is DBNull)) beData.ShowOnlyGenerateBillInReceipt = reader.GetBoolean(50);
                    if (!(reader[51] is DBNull)) beData.ShowDuesFeeHeadingInReceipt = reader.GetBoolean(51);
                    if (!(reader[52] is DBNull)) beData.AllowMultiplePaymentmode = reader.GetBoolean(52);
                    if (!(reader[53] is DBNull)) beData.OpeningFeeMonth = reader.GetString(53);
                    if (!(reader[54] is DBNull)) beData.AllowMonthWiseOnlinePayment = reader.GetBoolean(54);
                    if (!(reader[55] is DBNull)) beData.AllowValidateTotalDues = reader.GetBoolean(55);
                    if (!(reader[56] is DBNull)) beData.MonthNameAsBillDate_MB = reader.GetBoolean(56);
                    //Added
                    if (!(reader[57] is DBNull)) beData.InclBillDisSL = reader.GetBoolean(57);
                }
                reader.NextResult();
                beData.DefaulterDuesColl = new List<BE.Fee.Setup.FeeDefaulterMinDues>();
                while (reader.Read())
                {
                    AcademicLib.BE.Fee.Setup.FeeDefaulterMinDues det = new BE.Fee.Setup.FeeDefaulterMinDues();
                    if (!(reader[0] is DBNull)) det.ClassId = reader.GetInt32(0);
                    if (!(reader[1] is DBNull)) det.DuesAmt = Convert.ToDouble(reader[1]);
                    if (!(reader[2] is DBNull)) det.ClassName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.ReceiptTemplateId = reader.GetInt32(3);
                    if (!(reader[4] is DBNull)) det.BillTemplateId = reader.GetInt32(4);
                    if (!(reader[5] is DBNull)) det.CreditDays = reader.GetInt32(5);
                    if (!(reader[6] is DBNull)) det.BillGenerateOn = reader.GetDateTime(6);
                    if (!(reader[7] is DBNull)) det.BillGenerateDay = reader.GetInt32(7);
                    beData.DefaulterDuesColl.Add(det);
                }

                beData.ReportTemplateList = new List<Dynamic.BusinessEntity.Global.ReportTempletes>();
                reader.NextResult();
                while (reader.Read())
                {
                    Dynamic.BusinessEntity.Global.ReportTempletes det = new Dynamic.BusinessEntity.Global.ReportTempletes();
                    det.RptTranId = reader.GetInt32(0);
                    det.EntityId = reader.GetInt32(1);
                    if (!(reader[2] is DBNull)) det.ReportName = reader.GetString(2);
                    if (!(reader[3] is DBNull)) det.Description = reader.GetString(3);
                    if (!(reader[4] is DBNull)) det.Path = reader.GetString(4);
                    if (!(reader[5] is DBNull)) det.IsTransaction = reader.GetBoolean(5);
                    beData.ReportTemplateList.Add(det);
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
            cmd.CommandText = "usp_DelFeeConfigurationById";
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

        public ResponeValues MergeStudentOpening(int UserId, int AcademicYearId, int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@FeeItemId", FeeItemId);
            cmd.CommandText = "usp_MergeStudentOpening";
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

        public ResponeValues UpdateMissingLeftStudent(int UserId, int FromAcademicYearId, int ToAcademicYearId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@FromAcademicYearId", FromAcademicYearId);
            cmd.Parameters.AddWithValue("@ToAcademicYearId", ToAcademicYearId);
            cmd.CommandText = "usp_UpdateLeftStudentMissing";
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

        public ResponeValues GenerateSalesInvoice(int UserId, int AcademicYearId, bool IsReGenerate,DateTime? DateFrom,DateTime? DateTo)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@AcademicYearId", AcademicYearId);
            cmd.Parameters.AddWithValue("@IsReGenerate", IsReGenerate);
            cmd.CommandText = "usp_ConvertAllBillToSalesInvoice";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[4].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[5].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
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


        public ResponeValues UpdateMissingSalesInvoice(int UserId)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandText = "usp_UpdateMissingSalesInvoice";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@IsSuccess", System.Data.SqlDbType.Bit);
            cmd.Parameters.Add("@ErrorNumber", System.Data.SqlDbType.Int);
            cmd.Parameters[1].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[2].Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[1].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[1].Value);

                if (!(cmd.Parameters[2].Value is DBNull))
                    resVal.IsSuccess = Convert.ToBoolean(cmd.Parameters[2].Value);

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ErrorNumber = Convert.ToInt32(cmd.Parameters[3].Value);

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
        public ResponeValues MissingFeeToAccount(int UserId, DateTime? DateFrom, DateTime? DateTo)
        {
            ResponeValues resVal = new ResponeValues();

            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);
            cmd.CommandText = "usp_MissingFeeToAccount";
            cmd.Parameters.Add("@ResponseMSG", System.Data.SqlDbType.NVarChar, 254);
            cmd.Parameters[3].Direction = System.Data.ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (!(cmd.Parameters[3].Value is DBNull))
                    resVal.ResponseMSG = Convert.ToString(cmd.Parameters[3].Value);

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
    }
}
